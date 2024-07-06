using Application.DTOs;
using Core.Interfaces;

namespace Application.UseCases;

public class GetCurrency
{
    private readonly ICurrencyRepository _currencyRepository;

    public GetCurrency(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<ShowCurrenciesData> GetCurrencyById(Guid id)
    {
        var currency = await _currencyRepository.GetCurrency(id);
        return new ShowCurrenciesData
        {
            Id = currency.Id,
            Code = currency.Code,
            Creator = new ShowUserData
            {
                Id = currency.Creator!.Id,
                Username = currency.Creator.Username,
                CreatedAt = currency.Creator.CreatedAt,
                UpdatedAt = currency.Creator.UpdatedAt,
                Email = currency.Creator.Email
            },
            CreatorId = currency.CreatorId,
            Name = currency.Name
        };
    }
}