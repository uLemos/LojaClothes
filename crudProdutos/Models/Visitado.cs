using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace crudProdutos.Models
{
    public class Visitado
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public int IdProduto { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        [ForeignKey("IdProduto")]
        public Produto Produto { get; set; }
    }
}
