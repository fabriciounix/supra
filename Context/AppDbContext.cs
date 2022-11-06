using System.Reflection;
using Microsoft.IdentityModel;
using Microsoft.EntityFrameworkCore;
using supra.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace supra.Context
{
    public class AppDbContext :  IdentityDbContext<IdentityUser> //DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Fornecedor> Fornecedors { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Funcionario> Funcionarios { get; set; }

        public DbSet<Producao> Producaos { get; set; }
    }
}