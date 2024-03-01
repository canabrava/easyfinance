using ErrorOr;
using FinanceSystem.Infrastructure.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Security.Claims;

namespace FinanceSystem.Tests.UnitTests.Interfaces
{
    public class UserProviderTests
    {
        [Fact]
        public void GetUserId_WithValidUserIdClaim_ReturnsUserId()
        {
            // Arrange
            var userIdClaim = new Claim(ClaimTypes.UserData, "12345678-1234-1234-1234-123456789012");
            var httpContext = Substitute.For<HttpContext>();

            httpContext.User.FindFirst(ClaimTypes.UserData).Returns(userIdClaim);

            var userProvider = new UserProvider();

            // Act
            var result = userProvider.GetUserId(httpContext);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(Guid.Parse("12345678-1234-1234-1234-123456789012"));
        }

        [Fact]
        public void GetUserId_WithInvalidUserIdClaim_ReturnsUnauthorizedError()
        {
           // Arrange
           var userIdClaim = new Claim(ClaimTypes.UserData, "invalid-uuid");
           var httpContext = Substitute.For<HttpContext>();
           httpContext.User.FindFirst(ClaimTypes.UserData).Returns(userIdClaim);

           var userProvider = new UserProvider();

           // Act
           var result = userProvider.GetUserId(httpContext);

           // Assert
           result.IsError.Should().BeTrue();
           result.Errors.First().Should().Be(Error.Unauthorized());
        }

        [Fact]
        public void GetUserId_WithInvalidUserId_ReturnsUnauthorizedError()
        {
            // Arrange
            var httpContext = Substitute.For<HttpContext>();
            httpContext.User.FindFirst(ClaimTypes.UserData).Returns((Claim)null);
            var userProvider = new UserProvider();

            // Act
            var result = userProvider.GetUserId(httpContext);

            // Arrange
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(Error.Unauthorized());

        }
    }
}
