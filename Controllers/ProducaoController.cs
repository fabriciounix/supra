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
  //  [Authorize(Roles = "Admin")]

    public class ProducaoController : Controller
    {
        private readonly AppDbContext _context;

        public ProducaoController(AppDbContext context)
        {
            _context = context;
        }

        // public async Task<IActionResult> Index()
        // {
        //  ViewBag.FuncionarioId = new SelectList(_context.Funcionarios, "FuncionarioId", "Nome");
        //   return View(await _context.Producaos.ToListAsync());
        //   }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
        
            var resultado = _context.Producaos.AsNoTracking().AsQueryable();

            //aqui faz a pesquisa
            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.Nome.Contains(filter));
            }

            
            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
   
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);


        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producao = await _context.Producaos
                .FirstOrDefaultAsync(m => m.ProducaoId == id);
            if (producao == null)
            {
                return NotFound();
            }

            return View(producao);
        }

        public IActionResult Create()
        {
           // ViewBag.FuncionarioId = new SelectList(_context.Funcionarios, "FuncionarioId", "Nome");
            //ViewBag.ProdutoId = new SelectList(_context.Produtos, "ProdutoId", "Descricao");
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create([Bind("Nome,Descricao,Quantidade,Observacoes,Total_Notas,Status,FuncionarioId,ProdutoId,Dt")] Producao producao)
        {
            if (ModelState.IsValid)
            {
                var produtoEstoque = _context.Produtos.Where(p => p.Descricao.Equals(producao.Descricao) && producao.Quantidade <= p.Quantidade).FirstOrDefault();
              
                if (produtoEstoque != null)
                {
                    AtualizaProduto(producao.Quantidade,producao.ProdutoId);
                    _context.Add(producao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
               
            }
           
            return View(producao);
        }

        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producao = await _context.Producaos.FindAsync(id);
            if (producao == null)
            {
                return NotFound();
            }
            return View(producao);
        }

        // POST: Admin/AdminCategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProducaoId,Nome,Descricao,Quantidade,Observacoes,Total_Notas,Status,FuncionarioId,ProdutoId,Dt")] Producao producao)
        {
            if (id != producao.ProducaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducaoExists(producao.ProducaoId))
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
            return View(producao);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producao = await _context.Producaos
                .FirstOrDefaultAsync(m => m.ProducaoId == id);
            if (producao == null)
            {
                return NotFound();
            }

            return View(producao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producao = await _context.Producaos.FindAsync(id);
            _context.Producaos.Remove(producao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducaoExists(int id)
        {
            return _context.Producaos.Any(e => e.ProducaoId == id);
        }

        private void AtualizaProduto(int Quantidade, int Id)
        {
            Produto prodt = _context.Produtos.Where(p => p.ProdutoId == Id).FirstOrDefault();
            prodt.Categoria = prodt.Categoria;
            prodt.Descricao = prodt.Descricao;
            prodt.dt = prodt.dt;
            prodt.Observacao = prodt.Observacao;
            prodt.Quantidade = prodt.Quantidade - Quantidade;
            prodt.CategoriaId = prodt.CategoriaId;
            prodt.FornecedorId = prodt.FornecedorId;
            _context.Update(prodt);
            _context.SaveChanges();

        }

    }
}