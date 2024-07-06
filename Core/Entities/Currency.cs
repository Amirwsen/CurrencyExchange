
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("Currency",Schema = "Finance")]
public class Currency : BaseEntity
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public User? Creator { get; set; }
    public Guid CreatorId { get; set; }
}