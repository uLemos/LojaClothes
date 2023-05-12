using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crudProdutos.Models
{
    public class Pedido
    {

        public enum SituacaoPedido 
        {
            Cancelado,
            Realizado,
            //Verificado,
            //Entregue,
            //Atendido
        }

        [Key]
        [Display(Name = "Código")]
        public int IdPedido { get; set; }


        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [Display(Name = "Data/Hora")]
        public DateTime DataHoraPedido { get; set; }


        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Valor Total")]
        public Decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [DisplayName("Situação")]
        public SituacaoPedido Situacao { get; set; }

        [ForeignKey("IdUsuario")] //Chave estrangeira de outra tabela
        public Usuario Usuario { get; set; } //Referência
        public Endereco Endereco { get; set; }
        public ICollection<ItemPedido> ItensPedido { get; set; }

    }
}
