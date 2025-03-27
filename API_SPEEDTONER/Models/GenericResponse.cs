namespace API_SPEEDTONER.Models
{
    public class GenericResponse<T>
    {
        public int StatusCode { get; set; }
        public required GenericResponseStruct<T> Result { get; set; }
    }

    public class GenericResponseStruct<T>
    {
        public bool Success { get; set; } //Se hizo bien

        public T? Content { get; set; }
            
        public string? InnerException { get; set; }
    }
}
