namespace API_SPEEDTONER.Models
{
    public class DapperServiceResponse
    {
        public bool HasError { get; set; } = false;
        public string Message { get; set; }
        public dynamic? Results { get; set; }
    }
}
