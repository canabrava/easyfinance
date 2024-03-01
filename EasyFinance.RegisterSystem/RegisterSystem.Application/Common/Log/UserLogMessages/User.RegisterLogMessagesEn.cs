using static RegisterSystem.Application.Common.Log.LogMessages;

namespace RegisterSystem.Application.Messages.Log.UserLogMessages
{
    public static partial class User
    {
        public static class RegisterLogMessagesEn
        {
            public const string ActionName = "Restister";
            public const string RegisterSuccess = LogMessagesEn.Success + "{action} User: {id}";
            public const string RegisterError = LogMessagesEn.Error + "{action} User: {email} - {error}";
            public const string RegisterUnexpectedError = LogMessagesEn.UnexpectedError + "{action} User: {email} - message: {error} - stacktrace: {stacktrace}";
        }
    }
}
