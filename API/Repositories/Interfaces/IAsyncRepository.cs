using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> GetById(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task Add(T entity);
        Task Update(T entity);
        Task Remove(int id);
        Task SaveChangesAsync();

        Task<List<T>> GetAll();
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);
    }
}