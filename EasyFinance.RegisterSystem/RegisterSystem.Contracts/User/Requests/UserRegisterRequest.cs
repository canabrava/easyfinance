using RegisterSystem.Infrastructure.Messages.Error.UserErrorMessages;
using System.ComponentModel.DataAnnotations;

namespace RegisterSystem.Contracts.User.Requests
{
  


    public record UserRegisterRequest(
        [Required(ErrorMessage = UserErrorMessagesEn.UsernameRequired)] 
        string name,
        
        [Required(ErrorMessage = UserErrorMessagesEn.InvalidEmail)]
        [EmailAddress]
        string email,

        [Required(ErrorMessage = UserErrorMessagesEn.InvalidPassword)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = UserErrorMessagesEn.InvalidPassword)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = UserErrorMessagesEn.InvalidPassword)]
        string password);
}
