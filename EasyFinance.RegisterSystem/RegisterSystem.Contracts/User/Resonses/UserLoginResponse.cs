using System.ComponentModel.DataAnnotations;


namespace RegisterSystem.Contracts.User.Resonses
{
    public record UserLoginResponse(
        [Required] Guid id,
        [Required] string name,
        [Required] string email,
        [Required] string token);
}
