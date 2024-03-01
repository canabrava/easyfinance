using ErrorOr;

namespace RegisterSystem.Domain.Common.Errors
{
    public static class Errors
    {
        public static class UserErrors
        {
            public static Error DuplicateEmail = Error.Conflict(
                code: "User.DuplicateEmail", 
                description: "Email is already in use.");

            public static Error LoginFail = Error.Unauthorized(
                code: "User.LoginFail",
                description: "Invalid login attempt. Please try again.");
        }
    }
}
