using System.ComponentModel.DataAnnotations;

namespace Core.Request;

public sealed record LoginRequest(

    [Required(AllowEmptyStrings = false)] string Username,
    [Required(AllowEmptyStrings = false)] string Password
);