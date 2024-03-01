using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RegisterSystem.Contracts.User.Requests;
using RegisterSystem.Contracts.User.Resonses;
using RegisterSystem.Tests.IntegrationTests.TestUtils.Extensions;
using RegisterSystem.Tests.IntegrationTests.UserController.Generators;
using System.Net.Http.Json;
using Xunit;
using static RegisterSystem.Tests.IntegrationTests.TestUtils.Constants.Constants;

namespace RegisterSystem.Tests.IntegrationTests.UserController
{
    public class RegisterUserControllerTests : IClassFixture<RegisterSystemAPIFactory>
    {
        private readonly HttpClient _client;

        public RegisterUserControllerTests(RegisterSystemAPIFactory apiFactory)
        {
            _client = apiFactory.CreateClient();
        }

        [Fact]
        public async Task Register_RegisteredUser_WhenDataIsValid()
        {
            //Arrange
            var user = UserRegisterRequestGenerator.Generate();
            var loginRequest = new UserLoginRequest(user.email, user.password);

            //Act
            var response = await _client.PostAsJsonAsync("api/user/register", user);
            var loginResponse = await _client.PostAsJsonAsync("api/user/login", loginRequest);

            //Aseert
            var userResponse = await response.Content.ReadFromJsonAsync<UserRegisterResponse>();
            userResponse!.name.Should().Be(user.name);
            userResponse!.email.Should().Be(user.email);

            loginResponse.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task Register_ReturnsValidationError_When_nameIsEmpty()
        {
            //Arrange
            var user = UserRegisterRequestGenerator.GenerateWithEmptyUserName();

            //Act
            var response = await _client.PostAsJsonAsync("api/user/register", user);

            //Aseert
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            problemDetails!.GetErrors("name").Should().Contain(UserRegisterRequestErrors.InvalidUsername);
        }

        [Theory]
        [InlineData("")]
        [InlineData("emailInvalido")]
        public async Task Register_ReturnsValidationError_When_EmailIsInvalid(string email)
        {
            //Arrange
            var user = UserRegisterRequestGenerator.GenerateWithInvalidEmail(email);

            //Act
            var response = await _client.PostAsJsonAsync("api/user/register", user);

            //Aseert
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            problemDetails!.GetErrors("email").Should().Contain(UserRegisterRequestErrors.InvalidEmail);
        }

        [Theory]
        [InlineData("senha")]
        [InlineData("senhatestesenhatestesenhatestesenhatestesenhatestesenhatestesenhatestesenhatestesenhateste")]
        [InlineData("senha123")]
        public async Task Register_ReturnsValidationError_When_PasswordIsInvalid(string password)
        {
            //Arrange
            var user = UserRegisterRequestGenerator.GenerateWithInvalidPassword(password);

            //Act
            var response = await _client.PostAsJsonAsync("api/user/register", user);

            //Aseert
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            problemDetails!.GetErrors("password").Should().Contain(UserRegisterRequestErrors.InvalidPasword);
        }

        [Fact]
        public async Task Register_ReturnError_WhenEmailIsAlreadyRegistred()
        {
            //Arrange
            var user = UserRegisterRequestGenerator.Generate();
            await _client.PostAsJsonAsync("api/user/register", user);


            //Act
            var response = await _client.PostAsJsonAsync("api/user/register", user);

            //Aseert
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Conflict);
            problemDetails!.Title.Should().Contain(UserRegisterRequestErrors.EmailConflict);
        }
    }
}
