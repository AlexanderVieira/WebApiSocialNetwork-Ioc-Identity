using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebAPI.DataAccess.RegionContext;
using WebAPI.Domain.Interfaces.Repositories;

namespace WebAPI.DataAccess.Repositories
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        protected CtxRegion _ctx; 
        private bool disposed = false;

        public BaseRepository()
        {
            _ctx = new CtxRegion();
        }

        public void Delete(Guid id)
        {
            var obj = _ctx.Set<T>().Find(id);
            _ctx.Set<T>().Remove(obj);
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<T> GetAll()
        {
            return _ctx.Set<T>().ToList();
        }

        public T GetById(Guid id)
        {
            return _ctx.Set<T>().Find(id);
        }

        public void Save(T obj)
        {
            _ctx.Set<T>().Add(obj);
            _ctx.SaveChanges();
        }

        public void Update(T obj)
        {
            _ctx.Entry<T>(obj).State = EntityState.Modified;
            _ctx.SaveChanges();
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
        ~BaseRepository()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }
    }
}
