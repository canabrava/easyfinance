using ErrorOr;
using Microsoft.Extensions.Logging;
using RegisterSystem.Application.Common.Interfaces.Services;
using RegisterSystem.Application.Common.Utils;
using RegisterSystem.Application.Services.UserServices.Queries.Login.Interfaces;
using static RegisterSystem.Application.Common.Log.LogMessages;
using static RegisterSystem.Application.Common.Log.UserLogMessages.User;

namespace RegisterSystem.Application.Services.UserServices.Queries.Login
{
    public class LoginQueryLoggingDecorator
        : ILoginQueryHandler
    {
        private readonly ILoginQueryHandler _innerHandler;
        private readonly ILogger<ILoginQueryHandler> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public LoginQueryLoggingDecorator(ILoginQueryHandler innerHandler, ILogger<ILoginQueryHandler> logger, IDateTimeProvider dateTimeProvider)
        {
            _innerHandler = innerHandler;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<LoginResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var requesteDateTime = _dateTimeProvider.GetUtcNow();

            try
            {
                var result = await _innerHandler.Handle(request, cancellationToken);

                if (result.IsError)
                {
                    var errors = result.Errors.Select(e => e.Description);

                    foreach (var error in errors)
                    {
                        _logger.LogError(LoginLogMessagesEn.LoginError,
                            LogMessagesEn.RestisterSystem,
                            requesteDateTime,
                            LoginLogMessagesEn.ActionName,
                            request.email.MaskSensitiveData(), 
                            error);
                    }
                }
                else
                {
                    _logger.LogInformation(LoginLogMessagesEn.LoginSuccess,
                        LogMessagesEn.RestisterSystem,
                        requesteDateTime,
                        LoginLogMessagesEn.ActionName,
                        result.Value.id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoginLogMessagesEn.LoginUnexpectedError,
                    LogMessagesEn.RestisterSystem,
                    requesteDateTime,
                    LoginLogMessagesEn.ActionName,
                    request.email.MaskSensitiveData(), 
                    ex.Message, 
                    ex.StackTrace);

                throw new Exception(ex.StackTrace);
            }
        }
    }
}
