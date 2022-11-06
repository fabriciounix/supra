using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using supra.Context;
using supra.Models;
using supra.ViewModels;

namespace supra.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
       {
            var usuario = _context.Producaos.Select(u => u.FuncionarioId);
            return View(await _context.Producaos.ToListAsync());
       }
    }
}