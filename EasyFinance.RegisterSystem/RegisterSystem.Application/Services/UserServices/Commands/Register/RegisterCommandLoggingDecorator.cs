using ErrorOr;
using Microsoft.Extensions.Logging;
using RegisterSystem.Application.Common.Interfaces.Services;
using RegisterSystem.Application.Common.Utils;
using RegisterSystem.Application.Services.UserServices.Commands.Register.Interfaces;
using static RegisterSystem.Application.Common.Log.LogMessages;
using static RegisterSystem.Application.Messages.Log.UserLogMessages.User;

namespace RegisterSystem.Application.Services.UserServices.Commands.Register
{
    public class RegisterCommandLoggingDecorator 
        : IRegisterCommandHandler
    {
        private readonly IRegisterCommandHandler _innerHandler;
        private readonly ILogger<IRegisterCommandHandler> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public RegisterCommandLoggingDecorator(
            IRegisterCommandHandler innerHandler,
            ILogger<IRegisterCommandHandler> logger,
            IDateTimeProvider dateTimeProvider)
        {
            _innerHandler = innerHandler;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<RegisterResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
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
                        _logger.LogError(RegisterLogMessagesEn.RegisterError, 
                            LogMessagesEn.RestisterSystem,
                            requesteDateTime,
                            RegisterLogMessagesEn.ActionName,
                            request.email.MaskSensitiveData(), 
                            error);
                    }
                }
                else
                {
                    _logger.LogInformation(RegisterLogMessagesEn.RegisterSuccess,
                        LogMessagesEn.RestisterSystem,
                        requesteDateTime,
                        RegisterLogMessagesEn.ActionName,
                        result.Value.id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(RegisterLogMessagesEn.RegisterUnexpectedError, 
                    LogMessagesEn.RestisterSystem,
                    requesteDateTime,
                    RegisterLogMessagesEn.ActionName,
                    request.email.MaskSensitiveData(), 
                    ex.Message,
                    ex.StackTrace);

                throw new Exception(ex.StackTrace);
            }
        }
    }
}
