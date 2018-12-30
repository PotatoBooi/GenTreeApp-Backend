using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenTreeApp.API.Services.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetBy(Expression<Func<T,bool>> expression);
        Task<T> GetById(Guid id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();

    }
}
