using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IApiService
    {
        Task<List<Producto>> getProductos();
        Task<Producto> getProducto(int id);
        Task<bool> addProducto(Producto producto);
        Task<bool> updateProducto(int id, Producto producto);
        Task<bool> deleteProducto(int id);  
    }
}
