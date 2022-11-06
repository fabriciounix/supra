using supra.Models;
using supra.Context;
using supra.Repositories.Interface;

namespace supra.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly AppDbContext _context;

        public FuncionarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Funcionario> Funcionarios => _context.Funcionarios;
    }
}