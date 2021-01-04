namespace API.Core
{
    public class ServiceResponse
    {
       public ServiceResponse(bool success = true)
        {
            Success = success;
        }

        public bool Success { get; set; } = true;

        public string Message { get; set; } = null;
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public ServiceResponse(bool success = true)
        {
            Success = success;
        }

        public T Data { get; set; }
    }
}