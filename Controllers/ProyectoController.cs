    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using GestorProyectos.Models;

    namespace GestorProyectos.Controllers
    {
        public class ProyectoController : Controller
        {
            private readonly DbConnection _context;

            public ProyectoController(DbConnection context)
            {
                _context = context;
            }

            // GET: Proyecto
            public async Task<IActionResult> Index()
            {
                var dbConnection = _context.Proyectos.Include(p => p.Usuario);
                return View(await dbConnection.ToListAsync());
            }

            // GET: Proyecto/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var proyecto = await _context.Proyectos
                    .Include(p => p.Usuario)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (proyecto == null)
                {
                    return NotFound();
                }

                return View(proyecto);
            }

            // GET: Proyecto/Create
            public IActionResult Create()
            {
                ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
                return View();
            }

            // POST: Proyecto/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,FechaInicio,FechaFin,UsuarioId")] Proyecto proyecto)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(proyecto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.UsuarioId);
                return View(proyecto);
            }

            // GET: Proyecto/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var proyecto = await _context.Proyectos.FindAsync(id);
                if (proyecto == null)
                {
                    return NotFound();
                }
                ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.UsuarioId);
                return View(proyecto);
            }

            // POST: Proyecto/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,FechaInicio,FechaFin,UsuarioId")] Proyecto proyecto)
            {
                if (id != proyecto.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(proyecto);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProyectoExists(proyecto.Id))
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
                ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.UsuarioId);
                return View(proyecto);
            }

            // GET: Proyecto/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var proyecto = await _context.Proyectos
                    .Include(p => p.Usuario)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (proyecto == null)
                {
                    return NotFound();
                }

                return View(proyecto);
            }

            // POST: Proyecto/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var proyecto = await _context.Proyectos.FindAsync(id);
                if (proyecto != null)
                {
                    _context.Proyectos.Remove(proyecto);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool ProyectoExists(int id)
            {
                return _context.Proyectos.Any(e => e.Id == id);
            }
        }
    }
