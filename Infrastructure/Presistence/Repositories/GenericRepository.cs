using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class GenericRepository<TEntity, Tkey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity)
        =>await _dbContext.Set<TEntity>().AddAsync(entity);  



        public void Delete(TEntity entity)
        => _dbContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking=false)
            => asNoTracking? await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync():
            await _dbContext.Set<TEntity>().ToListAsync();

     

        public async Task<TEntity?> GetByIdAsync(Tkey id)
        => await _dbContext.Set<TEntity>().FindAsync(id);


        public void Update(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecefications<TEntity, Tkey> specefications)
        {
            List<TEntity> entities = await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specefications).ToListAsync();
            return entities;
        }


        public async Task<TEntity?> GetByIdAsync(ISpecefications<TEntity, Tkey> specefications)
        => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(),specefications).FirstOrDefaultAsync();

        public Task<int> CountAsync(ISpecefications<TEntity, Tkey> specefications)
        => SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specefications).CountAsync();

    }
}
