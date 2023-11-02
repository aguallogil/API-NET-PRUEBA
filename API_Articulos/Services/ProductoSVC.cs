using BLL;
using DAO;

namespace PruebaAPI.Services
{
    public interface IProductoSvc
    {
        Task<Response> Delete(int id);
        Producto Get(int id);
        List<Producto> GetAll();
        Task<Response> UpSert(Producto data);
    }

    public class ProductoSVC : IProductoSvc
    {
        public Task<Response> Delete(int id)
        {
            return ProductoBO.Delete(id);
        }

        public Producto Get(int id)
        {
            return ProductoBO.Get(id);
        }

        public List<Producto> GetAll()
        {
            return ProductoBO.GetAll();
        }

        public Task<Response> UpSert(Producto data)
        {
            return ProductoBO.UpSert(data);
        }
    }

}
