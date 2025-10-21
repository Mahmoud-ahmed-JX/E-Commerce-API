using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal abstract class BaseSpecification<TEntity, Tkey> : ISpecefications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {


        #region Criteria
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

        #endregion
       
        #region Add Include
        public BaseSpecification(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }

        public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
        #endregion

        #region sorting [orderby-orderbydescending]

        public Expression<Func<TEntity, object>> OrderBy { get;private set; } 

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

      

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        =>    OrderBy = orderByExpression;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
            => OrderByDescending = orderByDescExpression;

        #endregion

        #region Pagination
        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }=false;

        protected void ApplyPagination(int PageSize,int PageIndex)
        {
            IsPaginated = true;
            Take=PageSize;
            Skip=(PageIndex-1)*PageSize;
        }
        #endregion
    }
}
