using RegisterSystem.Infrastructure.Messages.Error.UserErrorMessages;
using System.ComponentModel.DataAnnotations;

namespace RegisterSystem.Contracts.User.Requests
{
    public record UserLoginRequest(
        [Required(ErrorMessage = UserErrorMessagesEn.InvalidEmail)]
        [EmailAddress]
        string email,

        [Required(ErrorMessage = UserErrorMessagesEn.InvalidPassword)]
        string password);
}
