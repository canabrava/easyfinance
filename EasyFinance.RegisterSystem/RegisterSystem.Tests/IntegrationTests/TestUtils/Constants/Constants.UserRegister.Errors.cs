namespace RegisterSystem.Tests.IntegrationTests.TestUtils.Constants
{
    public static partial class Constants
    {
        public static class UserRegisterRequestErrors
        {
            public const string InvalidUsername = "Username is required.";
            public const string InvalidEmail = "The email field is not a valid e-mail address.";
            public const string InvalidPasword = "Password must be between 6 and 50 characters long.";
            public const string EmailConflict = "Email is already in use.";
        }       
    }
}
