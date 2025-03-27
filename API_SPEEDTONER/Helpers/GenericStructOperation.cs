using API_SPEEDTONER.Models;

namespace API_SPEEDTONER.Helpers
{
    public class GenericStructOperation<T>
    {
        public static GenericResponseStruct<T> GetGenericResponseStruct(bool success, T content, string innerException)
        {
            return new GenericResponseStruct<T>
            {
                Success = success,
                Content = content,
                InnerException = innerException
            };
        }

    }
}
