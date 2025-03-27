using API_SPEEDTONER.Models;
using API_SPEEDTONER.Models.Customer;

namespace API_SPEEDTONER.Repositorys
{
    public interface ICustomerRepository
    {   
        Task<GenericResponse<List<Customer>>> GetClients();
        Task<GenericResponse<List<Email>>> GetEmails(int IdCliente);

        Task<GenericResponse<int>> GetIdClient(string Cliente);

        Task<GenericResponse<bool>> AddClient(Customer newCustomer);
        Task<GenericResponse<bool>> AddEmail(AddEmail newEmail);
    }
}
