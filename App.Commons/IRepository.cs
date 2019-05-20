using System;
using System.Collections.Generic;
using System.Text;

namespace App.Commons
{
    public interface IRepository<T> : IDisposable where T : EntityBase
    {
        int GetCount();
        T GetById(int id);
        List<T> GetAll();
        void Save(T employee);
        void Delete(int id);
    }
}
