using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using supra.Context;
using supra.Models;

namespace supra.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class FornecedorController : Controller
    {
        private readonly AppDbContext _context;

        public FornecedorController(AppDbContext context)
        {
            _context = context;
        }


        //  public async Task<IActionResult> Index()
        //   {
        //      return View(await _context.Fornecedors.ToListAsync());
        //  }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "razao_social")
        {
            // aqui o comando AsNoTracking faz a lista do todos os itens, e AsQuerible converte os dados para transfomar em uma consulta
            var resultado = _context.Fornecedors.AsNoTracking().AsQueryable();

            //aqui faz a pesquisa
            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.razao_social.Contains(filter));
            }

            //Aqui define o que eu quero pesquisar, a quantidade da paginação, o pageindex que é 1 e ordena pela Descrição
            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "razao_social");
            //aqui adiciona uma rota 
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            //aqui retorna o model na view
            return View(model);


        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedors
                .FirstOrDefaultAsync(m => m.FornecedorId == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]

        public async Task<IActionResult> Create([Bind("razao_social,cnpj,telefone,tipo,dt")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedors.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, [Bind("FornecedorId,razao_social,cnpj,telefone,tipo,dt")] Fornecedor fornecedor)
        {
            if (id != fornecedor.FornecedorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fornecedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorExists(fornecedor.FornecedorId))
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
            return View(fornecedor);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedors.FirstOrDefaultAsync(m => m.FornecedorId == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

      
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _context.Fornecedors.FindAsync(id);
            _context.Fornecedors.Remove(fornecedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(int id)
        {
            return _context.Fornecedors.Any(e => e.FornecedorId == id);
        }

    }
}

