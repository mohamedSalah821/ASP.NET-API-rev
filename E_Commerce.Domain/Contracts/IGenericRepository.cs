using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Common;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity , Tkey> where TEntity:BaseEntity<Tkey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<TEntity?> GetByIdAsync(ISpecification<TEntity , Tkey> Spec , CancellationToken ct = default);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity , Tkey> Spec , CancellationToken ct = default);
        Task<IEnumerable<TEntity>> GetAllAsync( CancellationToken ct = default);


    }
}
