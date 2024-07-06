using Core.Entities;

namespace Application.DTOs;

public class ShowCurrenciesData
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required ShowUserData Creator { get; set; }
    public Guid CreatorId { get; set; }
}