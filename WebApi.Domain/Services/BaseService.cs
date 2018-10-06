using System;
using System.Collections.Generic;
using WebAPI.Domain.Interfaces.Repositories;
using WebAPI.Domain.Interfaces.Services;

namespace WebAPI.Domain.Services
{
    public class BaseService<T> : IDisposable, IBaseService<T> where T : class
    {
        protected readonly IBaseRepository<T> _baseRepository;
        private bool disposed = false;        

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }       

        public void Delete(Guid id)
        {
            _baseRepository.Delete(id);            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<T> GetAll()
        {
            return _baseRepository.GetAll();
        }

        public T GetById(Guid id)
        {
            return _baseRepository.GetById(id);
        }

        public void Save(T obj)
        {
            _baseRepository.Save(obj);
        }

        public void Update(T obj)
        {
            _baseRepository.Update(obj);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                disposed = true;
            }
        }
        ~BaseService()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }
    }
}
