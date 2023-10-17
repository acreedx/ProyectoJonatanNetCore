using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModuloAdministracion.Data;
using ModuloAdministracion.Models;
using Microsoft.AspNetCore.Authorization;

namespace ModuloAdministracion.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly DBFARMACIAContext _context;

        public ProductoesController(DBFARMACIAContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            var dBFARMACIAContext = _context.Productos.Include(p => p.Categorias).Include(p => p.Proveedores);
            var list = await dBFARMACIAContext.ToListAsync();
            return View(list);
        }


        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categorias)
                .Include(p => p.Proveedores)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoriasId"] = new SelectList(_context.Categorias, "Id", "Categoria1");
            ViewData["ProveedoresId"] = new SelectList(_context.Proveedores, "Id", "Proveedor");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Nombreproducto,Precio,Cantidad,Estado,CategoriasId,ProveedoresId")] Producto producto, IFormFile RutaImagen)
        {
            if (RutaImagen != null && RutaImagen.Length > 0)
            {
                if (_context.Productos.Any(e => e.Nombreproducto.Equals(producto.Nombreproducto)))
                {
                    ViewBag.ErrorNombreProducto = "Ya existe un producto con ese nombre";
                    ViewData["CategoriasId"] = new SelectList(_context.Categorias, "Id", "Categoria1", producto.CategoriasId);
                    ViewData["ProveedoresId"] = new SelectList(_context.Proveedores, "Id", "Proveedor", producto.ProveedoresId);
                    return View(producto);
                }
                if (!ModelState.IsValid)
                {
                    string fileName = Path.GetFileName(RutaImagen.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString()+fileName;
                    string absolutePath = Path.Combine(_environment.WebRootPath,"images", "ProductosImages", uniqueFileName);
                    using (var stream = new FileStream(absolutePath, FileMode.Create))
                    {
                        RutaImagen.CopyTo(stream);
                    }
                    producto.RutaImagen = Path.Combine("/", "images", "ProductosImages", uniqueFileName).Replace(@"\", "/");
                    _context.Add(producto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CategoriasId"] = new SelectList(_context.Categorias, "Id", "Categoria1", producto.CategoriasId);
                ViewData["ProveedoresId"] = new SelectList(_context.Proveedores, "Id", "Proveedor", producto.ProveedoresId);
                return View(producto);
            }
            else
            {
                ViewBag.ErrorImagen = "Debes seleccionar una imagen.";
                ViewData["CategoriasId"] = new SelectList(_context.Categorias, "Id", "Categoria1");
                ViewData["ProveedoresId"] = new SelectList(_context.Proveedores, "Id", "Proveedor");
                return View(producto);
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriasId"] = new SelectList(_context.Categorias, "Id", "Categoria1", producto.CategoriasId);
            ViewData["ProveedoresId"] = new SelectList(_context.Proveedores, "Id", "Proveedor", producto.ProveedoresId);
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombreproducto,Precio,Cantidad,RutaImagen,Estado,CategoriasId,ProveedoresId")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    if (_context.Productos.Any(e => e.Nombreproducto.Equals(producto.Nombreproducto) && e.Id != producto.Id))
                    {
                        ViewBag.ErrorNombreProducto = "Ya existe un producto con ese nombre";
                        ViewData["CategoriasId"] = new SelectList(_context.Categorias, "Id", "Categoria1", producto.CategoriasId);
                        ViewData["ProveedoresId"] = new SelectList(_context.Proveedores, "Id", "Proveedor", producto.ProveedoresId);
                        return View(producto);
                    }
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["CategoriasId"] = new SelectList(_context.Categorias, "Id", "Categoria1", producto.CategoriasId);
            ViewData["ProveedoresId"] = new SelectList(_context.Proveedores, "Id", "Proveedor", producto.ProveedoresId);
            return View(producto);
        }


        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categorias)
                .Include(p => p.Proveedores)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }


        [Authorize]
        public async Task<IActionResult> Enable(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categorias)
                .Include(p => p.Proveedores)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'DBFARMACIAContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {

                if (producto.Estado == 0)
                {
                    producto.Estado = 1;
                }
                else
                {
                    producto.Estado = 0;
                }
                _context.Update(producto);
                await _context.SaveChangesAsync();
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return (_context.Productos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
