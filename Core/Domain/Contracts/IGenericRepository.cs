using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity: BaseEntity<Tkey>
    {
        //get all entities
        public Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking=false);
        //get by Id
        public Task<TEntity?> GetByIdAsync(Tkey id);
        //add entity
        public Task AddAsync(TEntity entity);
        //update entity
        public void Update(TEntity entity);
        //delete entity
        public void Delete(TEntity entity);

        #region Specification
        public Task<IEnumerable<TEntity>> GetAllAsync(ISpecefications<TEntity,Tkey> specefications);
        //get by Id
        public Task<TEntity?> GetByIdAsync(ISpecefications<TEntity, Tkey> specefications);
        
        Task<int> CountAsync(ISpecefications<TEntity, Tkey> specefications);
        #endregion
    }
}
