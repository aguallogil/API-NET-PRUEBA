using BLL;
using DAO;

namespace PruebaAPI.Services
{
    public interface IFacturaSvc
    {
        Task<Response> Delete(int id);
        Factura Get(int id);
        List<Factura> GetAll();
        List<Factura> GetAll(int id,int numero);
        Task<Response> UpSert(Factura data);
    }

    public class FacturaSVC : IFacturaSvc
    {
        public Task<Response> Delete(int id)
        {
            return FacturaBO.Delete(id);
        }

        public Factura Get(int id)
        {
            return FacturaBO.Get(id);
        }

        public List<Factura> GetAll()
        {
            return FacturaBO.GetAll();
        }

        public List<Factura> GetAll(int id, int numero)
        {
            return FacturaBO.GetAll(id, numero);
        }

        public Task<Response> UpSert(Factura data)
        {
            return FacturaBO.UpSert(data);
        }
    }

}
