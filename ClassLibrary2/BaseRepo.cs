using App.Commons;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace App.Repo
{
    public abstract class BaseRepo<T>: IRepository<T> where T : EntityBase
    {
        protected BaseContext Context = new BaseContext();
        private DbSet<T> _dbSet;

        public BaseRepo()
        {
            _dbSet = Context.Set<T>();
        }
        public void Dispose()
        {
            Context.Dispose();
        }

        public int GetCount()
        {
            return _dbSet.CountAsync().Result;
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefaultAsync(p => p.Id == id).Result;
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToListAsync().Result;
        }

        public void Save(T entity)
        {
            if (entity.Id == 0)
                _dbSet.Add(entity);
            else
                Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _dbSet.Remove(entity);
            Context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
