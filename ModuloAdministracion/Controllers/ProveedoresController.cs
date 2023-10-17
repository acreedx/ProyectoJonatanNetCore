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
    public class ProveedoresController : Controller
    {
        private readonly DBFARMACIAContext _context;

        public ProveedoresController(DBFARMACIAContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
              return _context.Proveedores != null ? 
                          View(await _context.Proveedores.ToListAsync()) :
                          Problem("Entity set 'DBFARMACIAContext.Proveedores'  is null.");
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedore == null)
            {
                return NotFound();
            }

            return View(proveedore);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Proveedor,Telefono,Estado")] Proveedore proveedore)
        {
            if (ModelState.IsValid)
            {
                if (_context.Proveedores.Any(e => e.Proveedor.Equals(proveedore.Proveedor)))
                {
                    ViewBag.ErrorProveedor = "Ya existe un proveedor con ese nombre";
                    return View(proveedore);
                }
                _context.Add(proveedore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedore);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores.FindAsync(id);
            if (proveedore == null)
            {
                return NotFound();
            }
            return View(proveedore);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Proveedor,Telefono,Estado")] Proveedore proveedore)
        {
            if (id != proveedore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_context.Proveedores.Any(e => e.Proveedor.Equals(proveedore.Proveedor) && e.Id != proveedore.Id))
                    {
                        ViewBag.ErrorProveedor = "Ya existe un proveedor con ese nombre";
                        return View(proveedore);
                    }
                    _context.Update(proveedore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedoreExists(proveedore.Id))
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
            return View(proveedore);
        }


        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedore == null)
            {
                return NotFound();
            }

            return View(proveedore);
        }

        [Authorize]
        public async Task<IActionResult> Enable(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedore == null)
            {
                return NotFound();
            }

            return View(proveedore);
        }
        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proveedores == null)
            {
                return Problem("Entity set 'DBFARMACIAContext.Proveedores'  is null.");
            }
            var proveedore = await _context.Proveedores.FindAsync(id);
            if (proveedore != null)
            {
                if (proveedore.Estado == 0)
                {
                    proveedore.Estado = 1;
                }
                else
                {
                    proveedore.Estado = 0;
                }
                _context.Update(proveedore);
                await _context.SaveChangesAsync();
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedoreExists(int id)
        {
          return (_context.Proveedores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
