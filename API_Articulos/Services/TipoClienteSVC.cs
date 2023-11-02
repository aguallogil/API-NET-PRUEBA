using BLL;
using DAO;

namespace PruebaAPI.Services
{
    public interface ITipoClienteSvc
    {
        Task<Response> Delete(int id);
        TipoCliente Get(int id);
        List<TipoCliente> GetAll();
        Task<Response> UpSert(TipoCliente data);
    }
    public class TipoClienteSVC : ITipoClienteSvc
    {
        public Task<Response> Delete(int id)
        {
            return TipoClienteBO.Delete(id);
        }

        public TipoCliente Get(int id)
        {
            return TipoClienteBO.Get(id);
        }

        public List<TipoCliente> GetAll()
        {
            return TipoClienteBO.GetAll();
        }

        public Task<Response> UpSert(TipoCliente data)
        {
            return TipoClienteBO.UpSert(data);
        }
    }
}
