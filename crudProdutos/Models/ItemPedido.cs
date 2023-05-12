using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crudProdutos.Models
{
    public class ItemPedido
    {
        [Required]
        public int IdPedido { get; set; }

        [Required] //Chave Composta, não há atributo KEY
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Valor Unitário")]  
        public decimal ValorVoluntario { get; set; }

        [ForeignKey("IdPedido")]
        public Pedido Pedido { get; set; } 
        
        [ForeignKey("IdProduto")]
        public Produto Produto { get; set; }


    }
}
