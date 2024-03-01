using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RegisterSystem.Api;
using RegisterSystem.Infrastructure.Persistence;
using System.Data.Common;
using Xunit;

namespace RegisterSystem.Tests.IntegrationTests
{
    public class RegisterSystemAPIFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
    {
        private readonly DbConnection _connection;

        public RegisterSystemAPIFactory()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            });

            builder.ConfigureTestServices(services =>
            {
                services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<RegisterSystemDbContext>) == service.ServiceType));

                services.AddDbContext<RegisterSystemDbContext>(options =>
                    options.UseSqlite(_connection));
            });
        }

        public async Task InitializeAsync()
        {
            using (var scope = Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<RegisterSystemDbContext>();
                dbContext.Database.EnsureCreated();
            }
        }

        public new async Task DisposeAsync()
        {
            using (var scope = Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<RegisterSystemDbContext>();
                dbContext.Database.EnsureDeleted();
            }

            await _connection.DisposeAsync();
        }
    }


}
