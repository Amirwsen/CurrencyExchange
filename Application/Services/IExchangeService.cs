namespace Application.Services;

public interface IExchangeService
{
    Task<double> GetPairRate(string fromCurrency, string toCurrency);
}