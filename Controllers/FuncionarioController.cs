using System.Security.Cryptography;
using supra.Context;
using supra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;
using System.Text;
using supra.ViewModels;

namespace supra.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FuncionarioController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public FuncionarioController(AppDbContext context, UserManager<IdentityUser> userManager,
         RoleManager<IdentityRole> roleManager) //
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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
        public async Task<IActionResult> Create([Bind("FuncionarioId,Dt,Nome,Sobrenome,Telefone,Cargo,Login,Senha")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                CadastraRole(funcionario.Login,funcionario.Senha);
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

        [HttpPost]

        public async Task<IActionResult> Edit(int id, [Bind("FuncionarioId,Dt,Nome,Sobrenome,Telefone,Cargo,Login,Senha")] Funcionario funcionario)
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
                    //AtualizaRole(nomeAtual.ToString(), funcionario.Nome, funcionario.Senha);
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
            DeletaRole(funcionario.Login);
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.FuncionarioId == id);
        }

        private void CadastraRole(string Usuario, string Senha)
        {
            /*  IdentityUser usuario = new IdentityUser();
               usuario.UserName = Usuario;
               usuario.Email = Usuario;
               usuario.NormalizedUserName = Usuario.ToUpper();
               usuario.NormalizedEmail = Usuario.ToUpper();
               usuario.EmailConfirmed = true;
               usuario.LockoutEnabled = false;

               usuario.SecurityStamp = Guid.NewGuid().ToString();
               byte[] textoAsBytes = Encoding.ASCII.GetBytes(Senha);
               var encodedValue = Encoding.UTF8.GetBytes(Senha);
               usuario.PasswordHash = System.Convert.ToBase64String(textoAsBytes);

               _context.Add(usuario);
               _context.SaveChanges(); */

            IdentityUser user = new IdentityUser();
            user.UserName = Usuario;
            user.Email = Usuario;
            user.NormalizedUserName = Usuario.ToUpper();
            user.NormalizedEmail = Usuario.ToUpper();
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = _userManager.CreateAsync(user, Senha).Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Member").Wait();
            }
            

        }

        private void DeletaRole(string Nome)
        {
            IdentityUser user = _context.Users.Where(u => u.UserName.Equals(Nome)).FirstOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();

        }

        private void AtualizaRole(string UsuarioAtual, string NovoUsuario, string Senha)
        {
            IdentityUser user = _context.Users.Where(u => u.UserName.Equals(UsuarioAtual)).FirstOrDefault();
            user.UserName = NovoUsuario;
            user.Email = NovoUsuario;
            user.NormalizedUserName = NovoUsuario.ToUpper();
            user.NormalizedEmail = NovoUsuario.ToUpper();
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            string SenhaAtual = user.PasswordHash;
            user.SecurityStamp = Guid.NewGuid().ToString();
            _userManager.ChangePasswordAsync(user, SenhaAtual, Senha);
            _userManager.UpdateAsync(user);
               
            
        }

    }
}