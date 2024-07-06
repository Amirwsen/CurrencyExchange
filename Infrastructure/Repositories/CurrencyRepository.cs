using Core.Entities;
using Core.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly DatabaseContext _databaseContext;

    public CurrencyRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<Currency>> GetAllCurrencies()
    {
        return await _databaseContext.Currencies.Include(x => x.Creator).ToListAsync();
    }

    public async Task<Currency> GetCurrency(Guid id)
    {
        return await _databaseContext.Currencies.Include(x => x.Creator).FirstAsync(x => x.Id == id);
    }

    public async Task<Currency> AddCurrency(Currency currency)
    {
        await _databaseContext.Currencies.AddAsync(currency);
        await _databaseContext.SaveChangesAsync();
        return currency;
    }
}