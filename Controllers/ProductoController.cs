using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Utilies;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class ProductoController : Controller
    {

        // Dependencia para comunicarse con la API.
        private readonly IApiService _apiService;

        // Constructor que inyecta el servicio de la API.
        public ProductoController(IApiService apiService)
        {
            _apiService = apiService;
        }


        // GET: ProductoController  
        public async Task<IActionResult> Index()
        {
            try
            {
                var productos = await _apiService.getProductos();
                return View(productos);
            } catch (Exception e)
            {
                return View(new List<Producto>());
            }
        }

        // GET: ProductoController/Details/5
        public async Task<ActionResult> Details(int IdProducto)
        {
            var producto = await _apiService.getProducto(IdProducto);
            if (producto != null)
            {
                return View(producto);
            }
            return RedirectToAction("Index");
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            var result = await _apiService.addProducto(producto);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: ProductoController/Edit/5
        public async Task<IActionResult> Edit(int IdProducto)
        {
            var producto = await _apiService.getProducto(IdProducto);
            if (producto != null)
            {
                return View(producto);
            }
            return RedirectToAction("Index");
        }

        // POST: ProductoController/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Producto producto)
        {
            var pAEditar = await _apiService.updateProducto(producto.IdProducto, producto);
            if (pAEditar != null)
            {
                return RedirectToAction(nameof(Index));
            }   
            return View(pAEditar);  
        }


        // GET: ProductoController/Delete/5
        public ActionResult Delete(int IdProducto)
        {
            var pEliminar = _apiService.deleteProducto(IdProducto);
            return RedirectToAction(nameof(Index));

        }

    }
}
