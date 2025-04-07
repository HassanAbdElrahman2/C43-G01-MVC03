using LinkDev.IKEA.DAL.Common.Interfaces;
using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Common.Entities
{
    public class GenericRepositproy<TKey, TEntity> : IGenericRepositproy<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext dbContext;

        public GenericRepositproy(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public TEntity? GetById(TKey id)
        {
           var Entity= dbContext.Set<TEntity>().Find(id);
            return Entity;
        }
        public IEnumerable<TEntity> GetAll(bool WithTracking = false)
        {
            if (!WithTracking)
                return dbContext.Set<TEntity>().AsNoTracking();
            return dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }
        public void Delete(TKey id)
        {
            var Entity = dbContext.Set<TEntity>().Find(id);
            if (Entity is { })
                dbContext.Set<TEntity>().Remove(Entity);
        }
    }
}
