using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecefications<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        //signature for prop [Expression ==> Where]  
        Expression<Func<TEntity, bool>>? Criteria { get; }

        //List of includes
        List<Expression<Func<TEntity,object>>> IncludeExpressions { get; }

        Expression<Func<TEntity, object>> OrderBy { get; }

        Expression<Func<TEntity, object>> OrderByDescending { get; }

        int Skip { get; }
        int Take { get; }
        bool IsPaginated { get; }


        }
}
