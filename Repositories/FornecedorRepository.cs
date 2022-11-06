using supra.Models;
using supra.Context;
using supra.Repositories.Interface;

namespace supra.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly AppDbContext _context;

        public FornecedorRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Fornecedor> Fornecedores => _context.Fornecedors;
    }
}