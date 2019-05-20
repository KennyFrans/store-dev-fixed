using System;
using System.Collections.Generic;
using System.Text;

namespace App.Commons
{
    public abstract class DomainServiceBase<T> where T : EntityBase
    {
        protected IRepository<T> entityRepo;

        public DomainServiceBase(IRepository<T> entityRepo)
        {
            this.entityRepo = entityRepo;
        }

        public abstract T CreateNew();

        public void Save(T entity)
        {
            entityRepo.Save(entity);
        }

        public void Delete(int id)
        {
            entityRepo.Delete(id);
        }

        public T GetById(int id)
        {
            return entityRepo.GetById(id);
        }

        public List<T> GetAll()
        {
            return entityRepo.GetAll();
        }
    }
}
