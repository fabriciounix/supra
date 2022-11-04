using supra.Context;
using supra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;

namespace supra.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FuncionarioController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FuncionarioController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

      //  public async Task<IActionResult> Index()
     //   {
     //     return View(await _context.Funcionarios.ToListAsync());
     //    }

         public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {

            var resultado = _context.Funcionarios.AsNoTracking().AsQueryable();

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

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.FuncionarioId== id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        public IActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuncionarioId,Dt,Nome,Sobrenome,Telefone,Cargo,Login,Senha,Telefone")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();

                var user = new IdentityUser { UserName = funcionario.Login};
                var result = _userManager.CreateAsync(user, funcionario.Senha);
                await _userManager.AddToRoleAsync(user, "Member");
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        [HttpPut]

        public async Task<IActionResult> Edit(int id, [Bind("FuncionarioId,Nome,Sobrenome,Cargo,Login,Senha,Telefone")] Funcionario funcionario)
        {
            if (id != funcionario.FuncionarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.FuncionarioId))
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
            return View(funcionario);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.FuncionarioId == id);
        }
        
    }
}