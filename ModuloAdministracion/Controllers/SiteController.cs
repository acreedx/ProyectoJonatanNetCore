using Microsoft.AspNetCore.Mvc;

namespace ModuloAdministracion.Controllers
{
    public class SiteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult InformacionInstitucional()
        {
            return View();
        }
        public IActionResult InicioDeSesion()
        {
            return View();
        }
        public IActionResult ProductoIndividual()
        {
            return View();
        }
        public IActionResult Productos()
        {
            return View();
        }
    }
}
