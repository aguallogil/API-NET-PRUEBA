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
    public class TipoClienteBO
    {
        public static async Task<Response> UpSert(TipoCliente data)
        {
            string _message = string.Empty;
            int _statusCode = 0;
            DataAccess.TransactionWrapper(() =>
            {
                var result = TipoClienteDA.UpSert(data);
                if (result)
                {
                    _message = "Se guardo correctamente";
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
        public static List<TipoCliente> GetAll()
        {
            return TipoClienteDA.GetAll();
        }
        public static TipoCliente Get(int id)
        {
            return TipoClienteDA.Get(id);
        }
        public static async Task<Response> Delete(int id)
        {
            string _message = string.Empty;
            int _statusCode = 0;
            DataAccess.TransactionWrapper(() =>
            {
                var result = TipoClienteDA.Delete(id);
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
