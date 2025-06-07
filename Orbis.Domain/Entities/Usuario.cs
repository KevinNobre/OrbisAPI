using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbis.Domain.Entities
{
    [Table("tb_usuario")]
    public class Usuario
    {
        [Key]
        [Column("USUARIO_ID")]
        public int UsuarioId { get; set; }

        [Required]
        [Column("NOME")]
        [MaxLength(55)]
        public string Nome { get; set; }

        [Required]
        [Column("SOBRENOME")]
        [MaxLength(55)]
        public string Sobrenome { get; set; }

        [Required]
        [Column("SENHA")]
        [MaxLength(20)]
        public string Senha { get; set; }

        [Required]
        [Column("TELEFONE")]
        [MaxLength(20)]
        public string Telefone { get; set; }

        [Column("CEP")]
        [MaxLength(12)]
        public string Cep { get; set; }
    }
}