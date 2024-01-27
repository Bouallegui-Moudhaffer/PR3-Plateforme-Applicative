using PA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA.ApplicationCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Type _repisitoryType;
        private bool disposedValue;
        private ApplicationDbContext dbContext;

        public UnitOfWork(ApplicationDbContext context, Type repoType)
        {
            _context = context;
            _repisitoryType = repoType;
        }

        public UnitOfWork(ApplicationDbContext dbContext) : this(dbContext, typeof(GenericRepository<>))
        {
            this.dbContext = dbContext;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            //return new GenericRepository<T>(_context);
            return (IGenericRepository<T>)
                Activator.
                CreateInstance(_repisitoryType.MakeGenericType(typeof(T)), _context);
        }

        public async Task<int> SaveAsync()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~UnitOfWork()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
