using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace supra.Models
{
    [Table("Categorias")]
    public class Categoria
    {
     
        [Key]
      public  int CategoriaId { get; set; }

      [Required(ErrorMessage = "Digite a descrição.")]
      [Display(Name = "Digite a descrição.")]
      [StringLength(100)]
      public string Descricao { get; set; }

      public List<Produto> Produtos { get; set; }
    }
}