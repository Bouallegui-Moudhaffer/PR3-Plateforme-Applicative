using PA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PA.ApplicationCore.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> _repository;
        public Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await CommitAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);
            await CommitAsync();
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            await _repository.DeleteAsync(where);
            await CommitAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            return await _repository.GetAsync(where);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(params object[] keyValues)
        {
            return await _repository.GetByIdAsync(keyValues);
        }

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            return await _repository.GetManyAsync(where);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
            await CommitAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
