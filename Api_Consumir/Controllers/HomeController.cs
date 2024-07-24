using Api_Consumir.Models;
using Api_Consumir.NewFolder;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api_Consumir.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiciosApi _serviciosApi;

        public HomeController(IServiciosApi servicioApi)
        {
            _serviciosApi = servicioApi;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Producto> lista = await _serviciosApi.Lista();
            return View(lista);
        }

        public async Task<IActionResult> ProductoBuscar(int idProducto)
        {
            Producto producto = await _serviciosApi.Obtener(idProducto);
            return View(producto);
        }

        public async Task<IActionResult> Eliminar(int idProducto)
        {
            var resultado =await _serviciosApi.Borrar(idProducto);
            if (resultado)
                return RedirectToAction("Index");

            else
                return NoContent();

            
        }
        public ActionResult Registrar()
        {
            Producto producto = new Producto();
            
            return View(producto);
        }


        public async Task<IActionResult> GuardarCambios(Producto producto)
        {
            bool respuesta;
            if (producto.iDpRODUCTO==0) {
                respuesta = await _serviciosApi.Guardar(producto);
               
            }
            else {
                respuesta = await _serviciosApi.Editar(producto);
            }
            if (respuesta) {
                TempData["AlertMessage"] = "Producto creado exitosamente...";
                return RedirectToAction("Index");
            }
            else
            {
                return NoContent();
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}
