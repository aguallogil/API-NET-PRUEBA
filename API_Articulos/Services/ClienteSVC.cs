using BLL;
using DAO;

namespace PruebaAPI.Services
{
    public interface IClienteSvc
    {
        Task<Response> Delete(int id);
        Cliente Get(int id);
        List<Cliente> GetAll();
        Task<Response> UpSert(Cliente data);
    }

    public class ClienteSVC : IClienteSvc
    {
        public Task<Response> Delete(int id)
        {
            return ClienteBO.Delete(id);
        }

        public Cliente Get(int id)
        {
            return ClienteBO.Get(id);
        }

        public List<Cliente> GetAll()
        {
            return ClienteBO.GetAll();
        }

        public Task<Response> UpSert(Cliente data)
        {
            return ClienteBO.UpSert(data);
        }
    }

}
