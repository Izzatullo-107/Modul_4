using Telegram.Bot.Exceptions;

namespace _4_7_TG_bot_lar.Extensions
{
    public static class ExceptionExtensions
    {
       
        public static string GetFriendlyMessage(this Exception ex)
        {
            // Switch mantiqi endi metod ichida, shuning uchun ishlaydi
            return ex switch
            {
                ApiRequestException apiEx => $"Telegram API xatosi: {apiEx.Message}",
                _ => $"Kutilmagan xatolik: {ex.Message}"
            };
        }

    }
}
