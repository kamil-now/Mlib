using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data
{
    public abstract class GenericRepository<C, T> : IGenericRepository<T> where T : class, IDataEntity where C : DbContext
    {
        public abstract T Get(string id);
        public C Context { get; set; }

        public virtual IQueryable<T> GetAll() => Context.Set<T>();

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) => Context.Set<T>().Where(predicate);

        public virtual void Add(T entity) => Context.Set<T>().Add(entity);

        public virtual void Delete(T entity) => Context.Set<T>().Remove(entity);

        public virtual void Edit(T entity) => Context.Entry(entity).State = EntityState.Modified;

        public virtual void Save() => Context.SaveChanges();
        public virtual void Clear() => Context.Set<T>().ForEachAsync(n => Delete(n));

        IDataEntity IRepository.Get(string id) => Get(id);

        IQueryable<IDataEntity> IRepository.GetAll() => GetAll();

        public void Add(IDataEntity entity) => Add((T)entity);

        public void Delete(IDataEntity entity) => Delete((T)entity);

        public void Edit(IDataEntity entity) => Edit((T)entity);
    }
}
