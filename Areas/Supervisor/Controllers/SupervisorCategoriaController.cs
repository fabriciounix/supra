using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using supra.Context;
using supra.Models;

namespace supra.Areas.Supervisor.Controllers
{
    [Area("Supervisor")]
    public class SupervisorCategoriaController : Controller
    {
        private readonly AppDbContext _context;

        public SupervisorCategoriaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminCategorias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        // GET: Admin/AdminCategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create([Bind("CategoriaId", "Descricao")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
    }
}