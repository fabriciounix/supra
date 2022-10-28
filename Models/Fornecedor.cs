using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace supra.Models
{
    [Table("Fornecedores")]
    public class Fornecedor
    {
        [Key]
        public int FornecedorId { get; set; }

        [Required(ErrorMessage = "Informe a data.")]
        [Display(Name = "Digite a data atual.")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime dt { get; set; }

        [Required(ErrorMessage = "Informe a razão social.")]
        [Display(Name = "Digite a razão social.")]
        [StringLength(100)]
        public string razao_social { get; set; }

        [Required(ErrorMessage = "Digite o CPF ou CNPJ.")]
        [Display(Name = "Digite o CPF ou CNPJ.")]
        [StringLength(14)]
        public string cnpj { get; set; }

        [Required(ErrorMessage = "Digite o telefone.")]
        [Display(Name = "Digite o telefone.")]
        [StringLength(20)]
        public string telefone { get; set; }

        [Required(ErrorMessage = "Selecione o Tipo.")]
        [Display(Name = "Selecione o Tipo.")]
        [StringLength(10)]
        public string tipo { get; set; }

        //public int ProdutoId { get; set; }

        public List<Produto> Produtos { get; set; }
    }
}