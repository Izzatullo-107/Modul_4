using _4_6_TelegramBot.Reposditories;
using _4_6_TelegramBot.Entiti;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Globalization;

// ==========================================
// 1. GLOBAL O'ZGARUVCHILAR (Top-level)
// ==========================================

var botClient = new TelegramBotClient("8783681570:AAFe2R-kYlfZ1YQas-C9goLdalBXJIAs-PI"); // Tokenni kiriting
var userRepo = new Repository<UserProfile>();
List<UserProfile> users = await userRepo.GetAllAsync();
Dictionary<long, string> userState = new Dictionary<long, string>();

using var cts = new CancellationTokenSource();

// Botni ishga tushirish
botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    errorHandler: HandlePollingErrorAsync,
    receiverOptions: new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() },
    cancellationToken: cts.Token
);

Console.WriteLine("Bot ishga tushdi...");
await Task.Delay(-1);

// ==========================================
// 2. ASOSIY UPDATE HANDLER
// ==========================================

async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct)
{
    if (update.Message is not { } message || message.Text is not { } messageText) return;

    long chatId = message.Chat.Id;

    // Registratsiya tekshiruvi
    bool isNewUser = !users.Any(u => u.Id == chatId);
    await CheckAndRegisterAsync(message);

    if (isNewUser)
    {
        await bot.SendMessage(chatId, "Xush kelibsiz! Siz muvaffaqiyatli ro'yxatdan o'tdingiz. ✅");
    }

    await IncrementUsageAsync(chatId);

    // "Orqaga" tugmasi har doim ishlashi kerak
    if (messageText == "⬅️ Orqaga")
    {
        userState.Remove(chatId);
        await bot.SendMessage(chatId, "Asosiy menyuga qaytdingiz.", replyMarkup: GetMainMenu());
        return;
    }

    // Konvertatsiya holati
    if (userState.ContainsKey(chatId) && userState[chatId].StartsWith("CONVERT_"))
    {
        await HandleConversion(bot, chatId, messageText);
        return;
    }

    // Menyu buyruqlari
    switch (messageText)
    {
        case "/start":
            await bot.SendMessage(chatId, "Assalomu alaykum! Kerakli bo'limni tanlang:", replyMarkup: GetMainMenu());
            break;

        case "📈 Valyuta kurslari":
            await ShowRates(bot, chatId);
            break;

        case "🔄 Konvertatsiya":
            await bot.SendMessage(chatId, "Valyutani tanlang:", replyMarkup: GetCurrencySelection());
            break;

        case "USD ➡️ UZS":
            userState[chatId] = "CONVERT_USD";
            await bot.SendMessage(chatId, "Qancha dollar (USD) miqdorini hisoblamoqchisiz? Faqat son kiriting:", replyMarkup: GetCurrencySelection());
            break;

        case "EUR ➡️ UZS":
            userState[chatId] = "CONVERT_EUR";
            await bot.SendMessage(chatId, "Qancha yevro (EUR) miqdorini hisoblamoqchisiz? Faqat son kiriting:", replyMarkup: GetCurrencySelection());
            break;

        case "👤 Statistika":
            var user = users.FirstOrDefault(u => u.Id == chatId);
            //await bot.SendMessage(chatId, $"📊 *Statistika:*\n\n📅 Ro'yxatdan o'tgan sana: {user?.RegistrationDate:dd.MM.yyyy}\n🔄 Bugungi foydalanish: {user?.DailyUsageCount} marta", parseMode: ParseMode.Markdown);
            //break;
            await bot.SendMessage(chatId, $"📊 *Statistika:*\n\n🔄 Bugungi foydalanish: {user?.DailyUsageCount} marta", parseMode: ParseMode.Markdown);
            break;
    }
}

// ==========================================
// 3. YORDAMCHI METODLAR (LOGIKA)
// ==========================================

async Task CheckAndRegisterAsync(Message msg)
{
    if (!users.Any(u => u.Id == msg.Chat.Id))
    {
        users.Add(new UserProfile
        {
            Id = msg.Chat.Id,
            Username = msg.Chat.Username ?? "Noma'lum",
            FirstName = msg.Chat.FirstName ?? "User",
            RegistrationDate = DateTime.Now,
            DailyUsageCount = 0,
            LastInteractionDate = DateTime.Now
        });
        await userRepo.SaveAllAsync(users);
    }
}

async Task IncrementUsageAsync(long chatId)
{
    var user = users.FirstOrDefault(u => u.Id == chatId);
    if (user != null)
    {
        if (user.LastInteractionDate.Date != DateTime.Now.Date)
            user.DailyUsageCount = 1;
        else
            user.DailyUsageCount++;

        user.LastInteractionDate = DateTime.Now;
        await userRepo.SaveAllAsync(users);
    }
}

async Task ShowRates(ITelegramBotClient bot, long chatId)
{
    var rates = await GetRatesFromCBU();
    string text = "📅 *Bugungi kurslar:*\n\n";
    foreach (var r in rates.Where(x => x.Code == "USD" || x.Code == "EUR" || x.Code == "RUB"))
    {
        text += $"1 {r.Code} = *{r.Rate}* UZS\n";
    }
    await bot.SendMessage(chatId, text, parseMode: ParseMode.Markdown);
}

async Task HandleConversion(ITelegramBotClient bot, long chatId, string text)
{
    string cleanText = text.Replace(",", ".");
    if (double.TryParse(cleanText, NumberStyles.Any, CultureInfo.InvariantCulture, out double amount))
    {
        string code = userState[chatId].Split('_')[1];
        var rates = await GetRatesFromCBU();
        var rateObj = rates.FirstOrDefault(x => x.Code == code);

        if (rateObj != null)
        {
            double rateValue = double.Parse(rateObj.Rate.Replace(",", "."), CultureInfo.InvariantCulture);
            double res = amount * rateValue;

            await bot.SendMessage(chatId, $"✅ *{amount:N0} {code}* = *{res:N2}* UZS", parseMode: ParseMode.Markdown, replyMarkup: GetMainMenu());
            userState.Remove(chatId);
        }
    }
    else
    {
        await bot.SendMessage(chatId, "⚠️ Iltimos, faqat son kiriting!");
    }
}

async Task<List<CurrencyModel>> GetRatesFromCBU()
{
    using HttpClient client = new HttpClient();
    var response = await client.GetStringAsync("https://cbu.uz/uz/arkhiv-kursov-valyut/json/");
    return JsonConvert.DeserializeObject<List<CurrencyModel>>(response);
}

// ==========================================
// 4. TUGMALAR VA XATOLIKLAR
// ==========================================

ReplyKeyboardMarkup GetMainMenu() => new ReplyKeyboardMarkup(new[]
{
    new KeyboardButton[] { "📈 Valyuta kurslari", "🔄 Konvertatsiya" },
    new KeyboardButton[] { "👤 Statistika" }
})
{ ResizeKeyboard = true };

ReplyKeyboardMarkup GetCurrencySelection() => new ReplyKeyboardMarkup(new[]
{
    new KeyboardButton[] { "USD ➡️ UZS", "EUR ➡️ UZS" },
    new KeyboardButton[] { "⬅️ Orqaga" }
})
{ ResizeKeyboard = true };

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken ct)
{
    Console.WriteLine("Xato: " + exception.Message);
    return Task.CompletedTask;
}

// ==========================================
// 5. MODELLAR
// ==========================================

public class CurrencyModel
{
    [JsonProperty("ccy")] public string Code { get; set; }
    [JsonProperty("rate")] public string Rate { get; set; }
}