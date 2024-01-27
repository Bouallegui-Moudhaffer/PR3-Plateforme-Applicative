using Microsoft.EntityFrameworkCore;
using PA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PA.ApplicationCore
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbset;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            _dbset.Add(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbset.Remove(entity);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            _dbset.RemoveRange(_dbset.Where(where));
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            //return _dbset.FirstOrDefault(where);
            return _dbset.Where(where).FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return _dbset.AsEnumerable();
        }

        public async Task<T> GetByIdAsync(params object[] keyValues)
        {
            return _dbset.Find(keyValues);
        }

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).AsEnumerable();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbset.Update(entity);
        }
    }
}
