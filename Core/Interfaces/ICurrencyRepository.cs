using Core.Entities;

namespace Core.Interfaces;

public interface ICurrencyRepository
{
    Task<List<Currency>> GetAllCurrencies();
    Task<Currency> GetCurrency(Guid id);
    Task<Currency> AddCurrency(Currency currency);

}