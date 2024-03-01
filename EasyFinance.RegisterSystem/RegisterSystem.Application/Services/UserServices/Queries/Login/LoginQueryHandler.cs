using ErrorOr;
using MediatR;
using RegisterSystem.Application.Common.Interfaces.Persistence;
using RegisterSystem.Application.Common.Interfaces.Services;
using RegisterSystem.Application.Services.UserServices.Queries.Login.Interfaces;
using static RegisterSystem.Domain.Common.Errors.Errors;

namespace RegisterSystem.Application.Services.UserServices.Queries.Login
{
    public class LoginQueryHandler
        : ILoginQueryHandler
    {
        private ICryptographyProvider _cryptoProvider;
        private IUserRepository _userRepository;
        private ITokenGenerator _tokenGenerator;

        public LoginQueryHandler(
            ICryptographyProvider cryptoProvider,
            IUserRepository userRepository,
            ITokenGenerator tokenGenerator)
        {
            _cryptoProvider = cryptoProvider;
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }


        public async Task<ErrorOr<LoginResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.email);

            if (user is null)
            {
                return UserErrors.LoginFail;
            }

            var validPassword = _cryptoProvider.ValidatePassword(request.password, user.PasswordHash, user.Salt);

            if(!validPassword)
            {
                return UserErrors.LoginFail;
            }

            var token = _tokenGenerator.GenerateToken(user);

            return new LoginResult(user.Id, user.Username, user.Email, token);
        }
    }
}
