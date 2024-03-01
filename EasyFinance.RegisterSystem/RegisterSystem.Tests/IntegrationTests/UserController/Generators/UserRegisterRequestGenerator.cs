using Bogus;
using RegisterSystem.Contracts.User.Requests;

namespace RegisterSystem.Tests.IntegrationTests.UserController.Generators
{
    public static class UserRegisterRequestGenerator
    {

        public static UserRegisterRequest Generate()
        {

            var userGenerator = new Faker<UserRegisterRequest>().CustomInstantiator(f =>
            {
                return new UserRegisterRequest(
                    f.Person.FullName,
                    f.Internet.Email(),
                    "!TesteSenha123456");
            });

            return userGenerator.Generate();
        }

        public static UserRegisterRequest GenerateWithEmptyUserName()
        {
            var userGenerator = new Faker<UserRegisterRequest>().CustomInstantiator(f =>
            {
                return new UserRegisterRequest(
                    String.Empty,
                    f.Internet.Email(),
                    "!TesteSenha123456");
            });

            return userGenerator.Generate();
        }

        public static UserRegisterRequest GenerateWithInvalidEmail(string email)
        {
            var userGenerator = new Faker<UserRegisterRequest>().CustomInstantiator(f =>
            {
                return new UserRegisterRequest(
                    f.Person.FullName,
                    email,
                    "!TesteSenha123456");
            });

            return userGenerator.Generate();
        }

        public static UserRegisterRequest GenerateWithInvalidPassword(string password)
        {
            var userGenerator = new Faker<UserRegisterRequest>().CustomInstantiator(f =>
            {
                return new UserRegisterRequest(
                    f.Person.FullName,
                    f.Internet.Email(),
                    password);
            });

            return userGenerator.Generate();
        }
    }
}
