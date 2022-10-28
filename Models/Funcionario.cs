using System.ComponentModel.DataAnnotations;

namespace supra.Models
{
    public class Funcionario
    {
        [Key]
        public int FuncionarioId { get; set; }

        [Required(ErrorMessage = "Informe a data.")]
        [Display(Name = "Digite a data atual.")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dt { get; set; }

        [Required(ErrorMessage = "Informe o nome.")]
        [Display(Name = "Digite o nome. ")]
        [StringLength(100)]
        public string Nome { get;  set; }

        [Required(ErrorMessage = "Informe o sobrenome.")]
        [Display(Name = "Digite o sobrenome. ")]
        [StringLength(100)]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Informe o telefone.")]
        [Display(Name = "Digite o telefone. ")]
        [StringLength(100)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe o cargo.")]
        [Display(Name = "Digite o cargo. ")]
        [StringLength(100)]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Informe o login.")]
        [Display(Name = "Digite o login. ")]
        [StringLength(100)]

        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a senha.")]
        [Display(Name = "Digite a senha.")]
        [StringLength(100)]
        public string Senha { get; set; }

        //public int ProducaoId { get; set; }

        public List<Producao> Producoes { get; set; }

     }
}