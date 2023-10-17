using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuloAdministracion.Data;
using ModuloAdministracion.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ModuloAdministracion.Controllers
{
    public class SiteController : Controller
    {
        private readonly DBFARMACIAContext _context;

        public SiteController(DBFARMACIAContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var dBFARMACIAContext = _context.Productos.Include(p => p.Categorias).Include(p => p.Proveedores).Where(e => e.Estado == 1).Take(7);
            var list = dBFARMACIAContext.ToList();
            return View(list);
        }
        public IActionResult InformacionInstitucional()
        {
            return View();
        }
        public IActionResult InicioDeSesion()
        {
            if (User.Identity.IsAuthenticated)
            { 
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Site");
        }
        [HttpPost]
        public async Task<IActionResult> InicioDeSesion(UsuarioLogin user)
        {
            if (await _context.Usuarios.AnyAsync(e => e.Correo.Equals(user.Correo) && e.Password.Equals(user.Password) && e.Estado == 1))
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Email, user.Correo),
                };
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Correo o contraseña incorrectos";
            return View();
        }
        public IActionResult ProductoIndividual()
        {
            return View();
        }
        public IActionResult Productos()
        {
            var dBFARMACIAContext = _context.Productos.Include(p => p.Categorias).Include(p => p.Proveedores).Where(e => e.Estado == 1);
            var list = dBFARMACIAContext.ToList();
            return View(list);
        }
    }
}
