using API_SPEEDTONER.Helpers;
using API_SPEEDTONER.Models.Customer;
using API_SPEEDTONER.Repositorys;
using API_SPEEDTONER.Services;  
using Microsoft.AspNetCore.Mvc;

namespace API_SPEEDTONER.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController:ControllerBase
    {
        //Inyeccion de dependecias
        private readonly IDapperService _dapperService;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerRepository _customerRepository2;

        public CustomerController(IDapperService dapperService, ICustomerRepository customerRepository)
        {
            _dapperService = dapperService;
            _customerRepository = customerRepository;
        }

        [HttpGet("ObtenerClientes")]
        public async Task<IActionResult> ObtenerClientes()
        {
            try
            {
                var response = await _customerRepository.GetClients();
                return StatusCode(response.StatusCode, response.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, GenericStructOperation<object>.GetGenericResponseStruct(false, null, ex.Message));
            }
        }

        [HttpGet("ObtenerContactos")]
        public async Task<IActionResult> ObtenerContactos(int IdCliente)
        {
            try
            {
                var response = await _customerRepository.GetEmails(IdCliente);
                return StatusCode(response.StatusCode, response.Result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, GenericStructOperation<object>.GetGenericResponseStruct(false, null, ex.Message));
            }
        }

        [HttpGet("ObtenerIdCliente")]
        public async Task<IActionResult> ObtenerIdCliente(string Cliente)
        {
            try
            {
                var response = await _customerRepository.GetIdClient(Cliente);
                return StatusCode(response.StatusCode, response.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, GenericStructOperation<object>.GetGenericResponseStruct(false, null, ex.Message));
            }
        }

        [HttpPost("GuardarCliente")]
        public async Task<IActionResult> GuardarCliente(int IdCliente, string Nombre, int DiasCredito)
        {
            try
            {
                Customer newCustomer = new Customer()
                {
                    IdCliente = IdCliente,
                    Nombre = Nombre,
                    DiasCredito = DiasCredito
                };
                var response = await _customerRepository.AddClient(newCustomer);

                return StatusCode(response.StatusCode, response.Result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, GenericStructOperation<object>.GetGenericResponseStruct(false, null, ex.Message));
            }
        }

        [HttpPost("GuardarCorreo")]
        public async Task<IActionResult> GuardarCorreo(int IdCliente, int IdCorreo, string Correo)
        {
            try
            {
                AddEmail newEmail = new AddEmail()
                {
                    IdCliente = IdCliente,
                    IdCorreo = IdCorreo,
                    Correo = Correo
                };
                var response = await _customerRepository.AddEmail(newEmail);

                return StatusCode(response.StatusCode, response.Result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, GenericStructOperation<object>.GetGenericResponseStruct(false, null, ex.Message));
            }
        }

    }
}
