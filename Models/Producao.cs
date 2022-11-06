using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace supra.Models
{
    [Table("Producaos")]
    public class Producao
    {
        [Key]
        public int ProducaoId { get; set; }

        [Required(ErrorMessage = "Informe a data.")]
        [Display(Name = "Digite a data atual.")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
       public DateTime Dt { get; set; }

        [Required(ErrorMessage = "Informe o nome.")]
        [Display(Name = "Digite o nome.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a descrição.")]
        [Display(Name = "Digite a descrição.")]
        [StringLength(100)]
        public string Descricao { get; set; }

        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Informe a descrição.")]
        [Display(Name = "Digite a descrição.")]
        [StringLength(100)]

        public string Observacoes { get; set; }

        public int Total_Notas { get; set; }

        public string Status { get; set; }

        public int FuncionarioId { get; set; }

        public virtual Funcionario Funcionario { get; set; }

        public int ProdutoId { get; set; }

        public virtual Produto Produtos { get; set; }


    }
}