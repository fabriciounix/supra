using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using supra.Context;
using supra.Models;

namespace supra.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logar(Funcionario usuarioModel)
        {
            var LoginValidate = ModelState["Login"];
            var SenhaValidate = ModelState["Senha"];

            if ((LoginValidate != null && !LoginValidate.Errors.Any()) ||
                (SenhaValidate != null && !SenhaValidate.Errors.Any()))
            { //Validações estão OK
                string usuario = usuarioModel.Login;
                string senha = usuarioModel.Senha;
                //Busca objeto no banco de dados

                var usuarioObj = _context.Funcionarios.Where(linha =>
                        linha.Login.Equals(usuario) &&
                        linha.Senha.Equals(senha)).FirstOrDefault();

                if (usuarioObj != null)
                {
                    //Usuario existente no banco de dados
                    ViewBag.usuarioLogado = usuarioModel.Nome;

                    HttpContext.Session.
                        SetString("IdUsuarioLogado", usuarioObj.FuncionarioId.ToString());

                    HttpContext.Session.
                        SetString("NomeUsuarioLogado", usuarioObj.Login.ToString());

                    return RedirectToAction("Index", "Supervisor");
                }
                else
                {
                    ViewBag.ErrorLogin = "Credenciais inválidas";
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }
        
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return View("Index");

        }

    }
}