using System.ComponentModel.DataAnnotations;

namespace RegisterSystem.Contracts.User.Resonses
{
    public record UserRegisterResponse(
        [Required] Guid id,
        [Required] string name,
        [Required] string email,
        [Required] DateTime registerDate);
}