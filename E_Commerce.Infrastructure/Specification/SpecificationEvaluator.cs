using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Specification
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity , Tkey>(IQueryable<TEntity> inputQuery , ISpecification<TEntity , Tkey> spec) where TEntity :BaseEntity<Tkey>
        {
            var query = inputQuery;

            if(spec.Criteria !=null)
            {
                query = query.Where(spec.Criteria);
            }

            if(spec.IncludeExpressions.Any())
            {
                query = spec.IncludeExpressions.Aggregate(query, (current, nextExp) => current.Include(nextExp));
            }

            if(spec.OrderBy !=null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if(spec.OrderByDescending !=null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if(spec.IsPaginated)
            {
                query= query.Skip(spec.Skip).Take(spec.Take);
            }

            return query;
        }
            
    }
}
