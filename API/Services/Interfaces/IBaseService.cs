using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core;

namespace API.Services.Interfaces
{
    public interface IBaseService<T>
    {
        Task<ServiceResponse<List<T>>> GetAll();
        Task<ServiceResponse<T>> GetById(int id);
        Task<ServiceResponse<T>> Add(T entity);
        Task<ServiceResponse<T>> Update(T entity);
        Task<ServiceResponse<T>> Remove(int id);
    }
}