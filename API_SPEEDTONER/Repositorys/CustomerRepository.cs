using API_SPEEDTONER.Helpers;
using API_SPEEDTONER.Models;
using API_SPEEDTONER.Models.Customer;
using API_SPEEDTONER.Services;
using Dapper;
using System.Reflection.Metadata.Ecma335;

namespace API_SPEEDTONER.Repositorys
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDapperService _dapperService;

        public CustomerRepository(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        public async Task<GenericResponse<List<Customer>>> GetClients()
        {
            StoredProcedureData NewStoredProcedure = new StoredProcedureData()
            {
                SchemaName = "[dbo]",
                Name = "MostrarClientes",
                IdConnectionString = "SpeedToner"
            };
            DynamicParameters parameters = new DynamicParameters();

            var dapperResponse = await _dapperService.ExecuteStoredProcedureAsync<Customer>(NewStoredProcedure, parameters, true);

            if (dapperResponse.HasError)
            {
                return new GenericResponse<List<Customer>>
                {
                    StatusCode = 500,
                    Result = GenericStructOperation<List<Customer>>.GetGenericResponseStruct(false, null, dapperResponse.Message)
                };
            }
            
            return new GenericResponse<List<Customer>>
            {
                StatusCode = 200,
                Result = GenericStructOperation<List<Customer>>.GetGenericResponseStruct(true,dapperResponse.Results, null)
            };
        }

        public async Task<GenericResponse<List<Email>>> GetEmails(int IdCliente)
        {
            StoredProcedureData NewStoredProcedure = new StoredProcedureData()
            {
                SchemaName = "[dbo]",
                Name = "MostrarContactos",
                IdConnectionString = "SpeedToner"
            };
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("IdCliente", IdCliente);

            var dapperResponse = await _dapperService.ExecuteStoredProcedureAsync<Email>(NewStoredProcedure, parameters, true);

            if (dapperResponse.HasError)
            {
                return new GenericResponse<List<Email>>
                {
                    StatusCode = 500,
                    Result = GenericStructOperation<List<Email>>.GetGenericResponseStruct(false, null, dapperResponse.Message)
                };
            }
            return new GenericResponse<List<Email>>
            {
                StatusCode = 200,
                Result = GenericStructOperation<List<Email>>.GetGenericResponseStruct(true, dapperResponse.Results, null)
            };
        }

        public async Task<GenericResponse<int>> GetIdClient(string Cliente)
        {
            StoredProcedureData NewStoredProcedure = new StoredProcedureData()
            {
                SchemaName = "[dbo]",
                Name = "BuscarIdCliente",
                IdConnectionString = "SpeedToner"
            };
            DynamicParameters parameters = new DynamicParameters();

            var dapperResponse = await _dapperService.ExecuteStoredProcedureAsync<int>(NewStoredProcedure, parameters, true);

            if (dapperResponse.HasError)
            {
                return new GenericResponse<int>
                {
                    StatusCode = 500,
                    Result = GenericStructOperation<int>.GetGenericResponseStruct(false, 0, dapperResponse.Message)
                };
            }
            Console.WriteLine("Hola");
            
            return new GenericResponse<int>
            {
                StatusCode = 200,
                Result = GenericStructOperation<int>.GetGenericResponseStruct(true, dapperResponse.Results[0], null)
            };
        }

        public async Task<GenericResponse<bool>> AddClient(Customer newCustomer)
        {
            StoredProcedureData NewStoredProcedure = new StoredProcedureData()
            {
                SchemaName = "[dbo]",
                Name = "GuardarCliente",
                IdConnectionString = "SpeedToner"
            };

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("IdCliente", newCustomer.IdCliente);
            parameters.Add("Cliente", newCustomer.Nombre);
            parameters.Add("DiasCredito", newCustomer.DiasCredito);

            var dapperResponse = await _dapperService.ExecuteStoredProcedureAsync<bool>(NewStoredProcedure, parameters, true);

            if (dapperResponse.HasError)
            {
                return new GenericResponse<bool>
                {
                    StatusCode = 500,
                    Result = GenericStructOperation<bool>.GetGenericResponseStruct(false, false, dapperResponse.Message)
                };
            }

            return new GenericResponse<bool>
            {
                StatusCode = 200,
                //Result = GenericStructOperation<List<bool>>.GetGenericResponseStruct(true, true, null)
                Result = GenericStructOperation<bool>.GetGenericResponseStruct(false,true, dapperResponse.Message)
            };
        }

        public async Task<GenericResponse<bool>> AddEmail(AddEmail newEmail)
        {
            StoredProcedureData NewStoredProcedure = new StoredProcedureData()
            {
                SchemaName = "[dbo]",
                Name = "GuardarCorreo",
                IdConnectionString = "SpeedToner"
            };

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("IdCorreo", newEmail.IdCorreo);
            parameters.Add("IdCliente", newEmail.IdCliente);
            parameters.Add("Correo", newEmail.Correo);

            var dapperResponse = await _dapperService.ExecuteStoredProcedureAsync<bool>(NewStoredProcedure, parameters, true);

            if(dapperResponse.HasError) 
            {
                return new GenericResponse<bool>
                {
                    StatusCode = 500,
                    Result = GenericStructOperation<bool>.GetGenericResponseStruct(false, false, dapperResponse.Message)
                };
            }
            return new GenericResponse<bool>
            {
                StatusCode = 200,
                Result = GenericStructOperation<bool>.GetGenericResponseStruct(true, true, null)
            };
        }
    }
}
    