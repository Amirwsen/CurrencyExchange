using Application.DTOs;
using Application.Services;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class ExchangeService : IExchangeService
{
    public async Task<double> GetPairRate(string fromCurrency, string toCurrency)
    {
        using var client = new HttpClient();
        try
        {
            var responseMessage =
                await client.GetAsync(
                    $"https://v6.exchangerate-api.com/v6/e1f31352b09e1a6b74bf6136/pair/{fromCurrency}/{toCurrency}/1");
            responseMessage.EnsureSuccessStatusCode();
            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<KeyToExchange>(responseBody);
            return response!.ConversionRate;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message : {0}", e.Message);
            return 0;
        }
    }
}