using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IApiService
    {
        Task<List<Producto>> getProductos();

    }
}
