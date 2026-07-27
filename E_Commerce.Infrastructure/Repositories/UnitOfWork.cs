using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;

namespace E_Commerce.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly Dictionary<string, object> repositories = [];

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var typeName = typeof(TEntity).Name;
            if (repositories.TryGetValue(typeName, out object? value)) 
                return (IGenericRepository<TEntity , Tkey>)value;

            var Repo = new GenericRepository<TEntity ,Tkey>(_dbContext);
            repositories[typeName] = Repo;
            return Repo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct)
            => await _dbContext.SaveChangesAsync(ct);
    }
}
