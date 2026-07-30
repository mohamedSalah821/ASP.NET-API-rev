using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories
{
    internal class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

      
      
        public void Add(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);
        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> Spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), Spec);

            return await query.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, Tkey> Spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), Spec);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = default)
        =>await _dbContext.Set<TEntity>().ToListAsync(ct);
    }
}
