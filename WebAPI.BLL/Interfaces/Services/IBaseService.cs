using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.BLL.Interfaces.Services
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Save(T obj);
        void Update(T obj);
        void Delete(Guid id);
        void Dispose();
    }
}
