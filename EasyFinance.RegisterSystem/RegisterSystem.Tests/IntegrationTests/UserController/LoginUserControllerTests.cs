using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RegisterSystem.Contracts.User.Requests;
using RegisterSystem.Tests.IntegrationTests.UserController.Generators;
using System.Net.Http.Json;
using Xunit;
using static RegisterSystem.Tests.IntegrationTests.TestUtils.Constants.Constants;

namespace RegisterSystem.Tests.IntegrationTests.UserController
{
    public class LoginUserControllerTests : IClassFixture<RegisterSystemAPIFactory>
    {
        private readonly HttpClient _client;

        public LoginUserControllerTests(RegisterSystemAPIFactory apiFactory)
        {
            _client = apiFactory.CreateClient();
        }

        [Fact]
        public async Task Login_RegisteredUser_WithValidPassword()
        {
            //Arrange
            var user = UserRegisterRequestGenerator.Generate();
            var loginRequest = new UserLoginRequest(user.email, user.password);
            await _client.PostAsJsonAsync("api/user/register", user);

            //Act
            var loginResponse = await _client.PostAsJsonAsync("api/user/login", loginRequest);

            //Aseert
            loginResponse.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task Login_RegisteredUser_WithInvalidPassword()
        {
            //Arrange
            var user = UserRegisterRequestGenerator.Generate();
            var loginRequest = new UserLoginRequest(user.email, $"{user.password}_wrong");
            await _client.PostAsJsonAsync("api/user/register", user);

            //Act
            var loginResponse = await _client.PostAsJsonAsync("api/user/login", loginRequest);

            //Aseert
            var problemDetails = await loginResponse.Content.ReadFromJsonAsync<ProblemDetails>();

            loginResponse.IsSuccessStatusCode.Should().BeFalse();
            loginResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
            problemDetails!.Title.Should().Contain(UserLoginRequestErrors.Unauthorized);
        }

        [Fact]
        public async Task Login_UnregisteredUser()
        {
            //Arrange
            var user = UserRegisterRequestGenerator.Generate();
            var loginRequest = new UserLoginRequest(user.email, user.password);

            //Act
            var loginResponse = await _client.PostAsJsonAsync("api/user/login", loginRequest);

            //Aseert
            var problemDetails = await loginResponse.Content.ReadFromJsonAsync<ProblemDetails>();

            loginResponse.IsSuccessStatusCode.Should().BeFalse();
            loginResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
            problemDetails!.Title.Should().Contain(UserLoginRequestErrors.Unauthorized);
        }
    }
}
