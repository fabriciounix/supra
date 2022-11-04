using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace supra.Models
{
    [Table("Produtos")]
    public class Produto
    {   
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Informe a data.")]
        [Display(Name = "Digite a data atual.")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dt { get; set; }

        [Required(ErrorMessage = "Informe a descrição.")]
        [Display(Name = "Digite a descrição.")]
        [StringLength(100)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe as observações.")]
        [Display(Name = "Digite as observações.")]
        [StringLength(100)]

        public string Observacao { get; set; }

        public int Quantidade { get; set; }

       // public int ProducaoId { get; set; }

        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }

        public int FornecedorId { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
    }
}