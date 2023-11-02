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
    public class ProductoBO
    {
        public static async Task<Response> UpSert(Producto data)
        {
            string _message = string.Empty;
            int _statusCode = 0;
            DataAccess.TransactionWrapper(() =>
            {
                var result = ProductoDA.UpSert(data);
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

        public static List<Producto> GetAll()
        {
            return ProductoDA.GetAll();
        }

        public static Producto Get(int id)
        {
            return ProductoDA.Get(id);
        }

        public static async Task<Response> Delete(int id)
        {
            string _message = string.Empty;
            int _statusCode = 0;
            DataAccess.TransactionWrapper(() =>
            {
                var result = ProductoDA.Delete(id);
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
