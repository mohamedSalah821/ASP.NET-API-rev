using E_Commerce.Domain.Contracts;

namespace E_Commerce.API.Extentions
{
    public static class WebApplicationExtentions
    {

        public static async Task<WebApplication> SeedAndMigrationDataAsync(this WebApplication app )
        {
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");

            await seeder.SeedDataAsync();

            return app;
        }
    }
}
