using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crudProdutos.Models
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MaxLength(1000)]
        [Display(Name = "Descrição")] //DataAnnotation sendo aplicada para todas as classes.
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [DataType(DataType.Currency)] //Inserção de valor no padrão correto.
        [Display(Name = "Preço")] //DataAnnotation sendo aplicada para todas as classes.
        public double? Preco { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public int? Estoque { get; set; }
    }
}
