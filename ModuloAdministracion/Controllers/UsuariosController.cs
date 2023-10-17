using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModuloAdministracion.Data;
using ModuloAdministracion.Models;

namespace ModuloAdministracion.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly DBFARMACIAContext _context;

        public UsuariosController(DBFARMACIAContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var dBFARMACIAContext = _context.Usuarios.Include(u => u.Roles);
            return View(await dBFARMACIAContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Rol");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Correo,Password,Nombre,Apellido,RolesId,Estado")] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                if (_context.Usuarios.Any(e => e.Correo.Equals(usuario.Correo)))
                {
                    ViewBag.ErrorCorreo = "Ya existe un usuario registrado con ese correo electronico";
                    ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Rol", usuario.RolesId);
                    return View(usuario);
                }
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Rol", usuario.RolesId);
            return View(usuario);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Rol", usuario.RolesId);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Correo,Password,Nombre,Apellido,RolesId,Estado")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    if (_context.Usuarios.Any(e => e.Correo.Equals(usuario.Correo) && e.Id != usuario.Id))
                    {
                        ViewBag.ErrorCorreo = "Ya existe un usuario registrado con ese correo electronico";
                        ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Rol", usuario.RolesId);
                        return View(usuario);
                    }
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Rol", usuario.RolesId);
            return View(usuario);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        [Authorize]
        public async Task<IActionResult> Enable(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'DBFARMACIAContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                if (usuario.Estado == 0)
                {
                    usuario.Estado = 1;
                }
                else
                {
                    usuario.Estado = 0;
                }
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
