using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Utilies;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class ProductoController : Controller
    {

        private readonly IApiService _apiService;

        public ProductoController(IApiService apiService)
        {
            _apiService = apiService;
        }


        // GET: ProductoController  
        public async Task<IActionResult> Index()
        {
            var productos = await _apiService.getProductos();
            return View(productos);
        }

        // GET: ProductoController/Details/5
        public ActionResult Details(int IdProducto)
        {
            Producto producto = Utils.ListaProductos.Find(x => x.IdProducto == IdProducto);
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
        public IActionResult Create(Producto producto)
        {
            int nProductos = Utils.ListaProductos.Count() + 1;
            producto.IdProducto = nProductos;
            Utils.ListaProductos.Add(producto);
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductoController/Edit/5
        public IActionResult Edit(int IdProducto)
        {
            Producto producto = Utils.ListaProductos.Find(x=>x.IdProducto==IdProducto);
            if(producto != null)
            {
                return View(producto);
            }
            return RedirectToAction("Index");
        }

        // POST: ProductoController/Edit
        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            Producto productoEncontrado = Utils.ListaProductos.Find(x => x.IdProducto == producto.IdProducto);

            if (productoEncontrado != null)
            {
                productoEncontrado.Nombre = producto.Nombre;
                productoEncontrado.Descripcion = producto.Descripcion;
                productoEncontrado.Cantidad = producto.Cantidad;
                return RedirectToAction("Index");
            }

            return View();
        }


        // GET: ProductoController/Delete/5
        public ActionResult Delete(int IdProducto)
        {
            Producto productoEncontrado = Utils.ListaProductos.Find(x => x.IdProducto == IdProducto);
            if (productoEncontrado != null)
            {
                Utils.ListaProductos.Remove(productoEncontrado);
            }
            return RedirectToAction("Index");
        }

    }
}
