namespace Api.Models
{
    public class ApiJsonResponseModel<T>
    {
        public int ResponseCode { get; set; } = 200;
        public string ErrorMessage { get; set; } = null;
        public T Data { get; set; }
    }
}
