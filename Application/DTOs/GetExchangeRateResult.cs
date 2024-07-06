namespace Application.DTOs;

public class GetExchangeRateResult
{
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }
    public double ExchangeRate { get; set; }
}