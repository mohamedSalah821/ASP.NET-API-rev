using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Infrastructure.DataSeeding
{
    internal class CatalogDataSeeder : IDataSeeder
    {
        private readonly StoreDbContext _dbContext;
        private readonly ILogger<CatalogDataSeeder> _logger;

        public CatalogDataSeeder(StoreDbContext dbContext , ILogger<CatalogDataSeeder> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var PendinMigration = await _dbContext.Database.GetPendingMigrationsAsync(ct);
                if(PendinMigration.Any())
                    await _dbContext.Database.MigrateAsync(ct);

                //Seeding
                var seedRoot = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                await SeedDataIfEmptyAsync<ProductBrand,int>(seedRoot, "brands.json", ct);
                await SeedDataIfEmptyAsync<ProductType, int>(seedRoot, "types.json", ct);
                await SeedDataIfEmptyAsync<Product, int>(seedRoot, "products.json", ct);


                int result = await _dbContext.SaveChangesAsync(ct);
                if (result > 0)
                    _logger.LogInformation($"{result} rows added");
                else
                    _logger.LogInformation("Data Already  seeded");

            }
            catch
            {

            }


        }
    
    
        private async Task SeedDataIfEmptyAsync<T,Tkey>(string rootPath , string fileName ,CancellationToken ct) where T:BaseEntity<Tkey>
        {
            if(await _dbContext.Set<T>().AnyAsync())
            {
                _logger.LogInformation("Table Already has data");
                return;
            }

            var filePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filePath))
            {
                _logger.LogWarning($"File {fileName} not found."); return;
            }

            using var FileStream =  File.OpenRead(filePath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var items = await JsonSerializer.DeserializeAsync<List<T>>(FileStream, options, ct);
            if(items?.Any() ??  false)
                   _dbContext.Set<T>().AddRange(items);
        }    
    }
}
