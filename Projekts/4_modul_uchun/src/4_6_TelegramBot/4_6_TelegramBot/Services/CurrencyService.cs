using _4_6_TelegramBot.DTOs;
using System.Text.Json;

namespace _4_6_TelegramBot.Services;

public class CurrencyService
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://cbu.uz/uz/arkhiv-kursov-valyut/json/";

    public CurrencyService()
    {
        _httpClient = new HttpClient();
    }

    // Onlayn kurslarni olish
    public async Task<List<ValutaDto>> GetOnlineRatesAsync()
    {
        try
        {
            var response = await _httpClient.GetStringAsync(ApiUrl);
            var rates = JsonSerializer.Deserialize<List<ValutaDto>>(response);
            return rates ?? new List<ValutaDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xatolik yuz berdi: {ex.Message}");
            return new List<ValutaDto>();
        }
    }

    // Konvertatsiya qilish funksiyasi
    public double Convert(string direction, double amount, ValutaDto selectedCurrency)
    {
        double rate = double.Parse(selectedCurrency.Kurs.Replace(",", ".")); // Kursni songa o'tkazish

        if (direction == "USD_TO_UZS")
        {
            return amount * rate;
        }
        else // UZS_TO_USD
        {
            return amount / rate;
        }
    }
}