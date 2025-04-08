using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Common.Interfaces
{
    public interface IGenericRepositproy <TKey,TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : BaseEntity<TKey>
        
    {
        IEnumerable<TEntity> GetAll(bool WithTracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity,TResult>> selector);
        TEntity? GetById(TKey id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);
    }
}
