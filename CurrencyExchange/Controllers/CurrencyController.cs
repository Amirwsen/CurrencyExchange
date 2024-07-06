using System.Security.Claims;
using Application.DTOs;
using Application.Services;
using Application.UseCases;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CurrencyController : ControllerBase
{
    private readonly GetCurrencies _getCurrencies;
    private readonly IExchangeService _exchangeService;
    private readonly GetCurrency _getCurrency;
    private readonly AddCurrency _addCurrency;

    public CurrencyController(GetCurrencies getCurrencies,
        IExchangeService exchangeService, GetCurrency getCurrency, AddCurrency addCurrency)
    {
        _getCurrencies = getCurrencies;
        _exchangeService = exchangeService;
        _getCurrency = getCurrency;
        _addCurrency = addCurrency;
    }

    [HttpGet("GetCurrencies")]
    public async Task<ActionResult<List<ShowCurrenciesData>>> GetAllCurrency()
    {
        return Ok(await _getCurrencies.GetAllCurrencies());
    }

    [HttpGet("GetExchangeRate/{fromCurrencyId}/{toCurrencyId}")]
    public async Task<ActionResult<GetExchangeRateResult>> GetExchangeRate(Guid fromCurrencyId, Guid toCurrencyId)
    {
        var fromCurrency = await _getCurrency.GetCurrencyById(fromCurrencyId);
        var toCurrency = await _getCurrency.GetCurrencyById(toCurrencyId);
        var rate = await _exchangeService.GetPairRate(fromCurrency.Code, toCurrency.Code);

        return Ok(new GetExchangeRateResult
        {
            ExchangeRate = rate,
            FromCurrency = fromCurrency.Code,
            ToCurrency = toCurrency.Code
        });
    }

    [HttpPost("CreateCurrency")]
    public async Task<ActionResult<Currency>> AddToCurrency(AddToCurrencyTableDTO addToCurrencyTableDto)
    {
        var userId = Guid.Empty;
        if (HttpContext.User.Identity is ClaimsIdentity identity)
        {
            var claims = identity.Claims;
            var id = identity.FindFirst("userId")?.Value;
            if (id != null) userId = Guid.Parse(id);
        }

        var currency = await _addCurrency.AddToCurrency(addToCurrencyTableDto, userId);
        return Ok(currency);
    }
}