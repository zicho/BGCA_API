using API.Core;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<ServiceResponse<List<T>>> GetAll()
        {
            try
            {
                return new ServiceResponse<List<T>>
                {
                    Data = await _repository.GetAll()
                };
            }
            catch
            {
                return new ServiceResponse<List<T>>
                {
                    Success = false,
                    Message = $"Method '{nameof(GetAll)}' in '{GetType().Name}' failed when retrieving {typeof(T)}'"
                };
            }
        }

        public virtual async Task<ServiceResponse<T>> GetById(int id)
        {
            try
            {
                return new ServiceResponse<T>
                {
                    Data = await _repository.GetById(id)
                };
            }
            catch
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = $"Method '{nameof(GetById)}' in '{GetType().Name}' failed when retrieving {typeof(T)}"
                };
            }
        }

        public virtual async Task<ServiceResponse<T>> Add(T entity)
        {
            try
            {
                await _repository.Add(entity);
                return new ServiceResponse<T> { Message = $"Entity of type '{entity}' was successfully added." };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public virtual async Task<ServiceResponse<T>> Update(T entity)
        {
            try
            {
                await _repository.Update(entity);
                return new ServiceResponse<T> { Message = $"Entity '{entity}' was successfully updated." }; ;
            }
            catch
            {
                return new ServiceResponse<T>(false);
            }
        }

        public virtual async Task<ServiceResponse<T>> Remove(int id)
        {
            try
            {
                await _repository.Remove(id);
                return new ServiceResponse<T> { Message = $"Entity of type '{nameof(T)} (ID: {id}' was successfully removed." }; ;
            }
            catch (Exception ex)
            {
                return new ServiceResponse<T>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}