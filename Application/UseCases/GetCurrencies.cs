using Application.DTOs;
using Core.Interfaces;

namespace Application.UseCases;

public class GetCurrencies
{
    private readonly ICurrencyRepository _currencyRepository;

    public GetCurrencies(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }
    public async Task<List<ShowCurrenciesData>> GetAllCurrencies()
    {
        return (await _currencyRepository.GetAllCurrencies()).Select(x => new ShowCurrenciesData
        {
            Id = x.Id,
            Name = x.Name,
            Code = x.Code,
            Creator = new ShowUserData
            {
                Username = x.Creator!.Username,
                CreatedAt = x.Creator.CreatedAt,
                UpdatedAt = x.Creator.UpdatedAt,
                Email = x.Creator.Email,
                Id = x.Creator.Id
            },
            CreatorId = x.CreatorId
        })
        .ToList();
    }
}