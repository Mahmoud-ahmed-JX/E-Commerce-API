using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> inputQuery, ISpecefications<TEntity, TKey> specefications) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            if(specefications.Criteria is not null)
                query= query.Where(specefications.Criteria);

            if(specefications.OrderBy is not null)
                query=query.OrderBy(specefications.OrderBy);
            if (specefications.OrderByDescending is not null)
                query = query.OrderByDescending(specefications.OrderByDescending);

                if (specefications.IncludeExpressions is not null && specefications.IncludeExpressions.Count>0)
                query=specefications.IncludeExpressions.Aggregate(query,(current,expression)=>current.Include(expression));

            if (specefications.IsPaginated)
            {
                query = query.Skip(specefications.Skip).Take(specefications.Take);
            }


            return query.AsQueryable();
        }
    }
}
