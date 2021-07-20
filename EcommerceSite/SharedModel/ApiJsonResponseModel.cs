namespace SharedModel
{
    public class ApiJsonResponseModel<T>
    {
        public int ResponseCode { get; set; } = HttpResponseCode.OK;
        public string ErrorMessage { get; set; } = null;
        public T Data { get; set; }
    }
}
