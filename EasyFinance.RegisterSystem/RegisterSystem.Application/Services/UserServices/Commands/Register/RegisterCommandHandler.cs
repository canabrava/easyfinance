using ErrorOr;
using MediatR;
using RegisterSystem.Application.Common.Interfaces.Persistence;
using RegisterSystem.Application.Common.Interfaces.Services;
using RegisterSystem.Application.Services.UserServices.Commands.Register.Interfaces;
using RegisterSystem.Domain.Entities;
using static RegisterSystem.Domain.Common.Errors.Errors;

namespace RegisterSystem.Application.Services.UserServices.Commands.Register
{
    public class RegisterCommandHandler 
        : IRegisterCommandHandler
    {
        private IDateTimeProvider _dateTimeProvider;
        private ICryptographyProvider _cryptoProvider;
        private IUserRepository _userRepository;

        public RegisterCommandHandler(
            IDateTimeProvider dateTimeProvider,
            ICryptographyProvider cryptoProvider, 
            IUserRepository userRepository)
        {
            _dateTimeProvider = dateTimeProvider;
            _cryptoProvider = cryptoProvider;
            _userRepository = userRepository;
        }


        public async Task<ErrorOr<RegisterResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByEmailAsync(request.email) is not null)
            {
                return UserErrors.DuplicateEmail;
            }

            var salt = _cryptoProvider.GenerateSalt();

            var hashPassword = _cryptoProvider.HashPassword(request.password, salt);

            var registerDate = _dateTimeProvider.GetUtcNow();

            var user = new User
            {
                Username = request.name,
                Email = request.email,
                Salt = salt,
                PasswordHash = hashPassword,
                RegisterDate = registerDate
            };

            await _userRepository.CreateAsync(user);

            return new RegisterResult(user.Id, user.Username, user.Email, user.RegisterDate);
        }
    }
}
