using static RegisterSystem.Application.Common.Log.LogMessages;

namespace RegisterSystem.Application.Common.Log.UserLogMessages
{
    public static partial class User
    {
        public static class LoginLogMessagesEn
        {
            public const string ActionName = "Login";
            public const string LoginSuccess = LogMessagesEn.Success + "{action} User: {id}";
            public const string LoginError = LogMessagesEn.Error + "{action} User: {email} - {error}";
            public const string LoginUnexpectedError = LogMessagesEn.UnexpectedError + "{action} User: {email} - message: {error} - stacktrace: {stacktrace}";
        }
    }
}
