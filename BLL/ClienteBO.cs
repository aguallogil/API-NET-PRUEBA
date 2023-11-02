using DAL.Helpers;
using DAL;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ClienteBO
    {
        public static async Task<Response> UpSert(Cliente data)
        {
            string _message = string.Empty;
            int _statusCode = 0;
            DataAccess.TransactionWrapper(() =>
            {
                var result = ClienteDA.UpSert(data);
                if (result)
                {
                    _message = "Se guardó correctamente";
                    _statusCode = 200;
                }
                else
                {
                    _message = "Hubo un problema al guardar";
                    _statusCode = 409;
                }
            });

            return new Response()
            {
                StatusCode = _statusCode,
                Message = _message
            };
        }

        public static List<Cliente> GetAll()
        {
            return ClienteDA.GetAll();
        }

        public static Cliente Get(int id)
        {
            return ClienteDA.Get(id);
        }

        public static async Task<Response> Delete(int id)
        {
            string _message = string.Empty;
            int _statusCode = 0;
            DataAccess.TransactionWrapper(() =>
            {
                var result = ClienteDA.Delete(id);
                if (result)
                {
                    _message = "Se eliminó correctamente";
                    _statusCode = 200;
                }
                else
                {
                    _message = "Hubo un problema al eliminar";
                    _statusCode = 409;
                }
            });

            return new Response()
            {
                StatusCode = _statusCode,
                Message = _message
            };
        }
    }

}
