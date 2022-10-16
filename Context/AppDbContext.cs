using Microsoft.EntityFrameworkCore;
using supra.Models;

namespace supra.Context
{
    public class AppDbContext : DbContext
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