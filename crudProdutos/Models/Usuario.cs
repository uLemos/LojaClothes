using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace crudProdutos.Models
{
    public class Usuario
    {
        public enum SituacaoUsuario
        {
            Cadastrado,
            Aprovado, 
            Bloqueado
        }

        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [DataType(DataType.Date, ErrorMessage = "O campo {0} deve conter uma data válida.")]
        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [RegularExpression(@"[0-9]{11}$", ErrorMessage = "O campo {0} deve ser preenchido com 11 dígitos numéricos.")] //REGEX
        [MaxLength(11, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [MinLength(11, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres.")]
        [UIHint("_CustomCPF")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchim,ento obrigatório.")]
        [RegularExpression(@"[0-9]{11}$", ErrorMessage = "O campo {0} deve ser preenchido com 11 dígitos numéricos.")] //REGEX
        [MaxLength(11, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [MinLength(11, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres.")]
        //[UIHint("_TelefoneTemplate")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "O campo {0} deve conter um endereço de e-mail válido.")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [DisplayName("Situação")]
        public SituacaoUsuario Situacao { get; set; }
        public Endereco Endereco { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }

    }
}
