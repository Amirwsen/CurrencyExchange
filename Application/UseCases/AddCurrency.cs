using Application.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Application.UseCases;

public class AddCurrency
{
    private readonly ICurrencyRepository _currencyRepository;

    public AddCurrency(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<Currency> AddToCurrency(AddToCurrencyTableDTO addToCurrencyTableDto, Guid userId)
    {
        var creatingCurrency = new Currency
        {
            Name = addToCurrencyTableDto.Name,
            Code = addToCurrencyTableDto.Code,
            CreatorId = userId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        await _currencyRepository.AddCurrency(creatingCurrency);
        return creatingCurrency;
    }
}