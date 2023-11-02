using DAO;

namespace PruebaAPI.Services
{
    public interface ICrud<T>
    {
        Task<Response> UpSert(T data);
        List<T> GetAll();
        T Get(int id);
        Task<Response> Delete(int id);
    }
}
