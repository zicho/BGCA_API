namespace API.Core
{
    public class ServiceResponse<T>
    {
        public ServiceResponse(bool success = true)
        {
            Success = success;
        }

        public T Data { get; set; }

        public bool Success { get; set; } = true;

        public string Message { get; set; } = null;
        
    }
}