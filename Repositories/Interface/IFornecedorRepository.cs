using supra.Models;

namespace supra.Repositories.Interface
{
    public interface IFornecedorRepository
    {
         IEnumerable<Fornecedor> Fornecedores { get; }

    }
}