using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AmazingCo.Api.Data
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly CompanyStructureContext _dbContext;

        public RepositoryBase(CompanyStructureContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return   _dbContext.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return  _dbContext.Set<TEntity>().FirstOrDefault(e => e.Id == id);
        }

        public async Task Update(int id, TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
