using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbis.Domain.Entities
{
    [Table("tb_check_seguranca")]
    public class CheckSeguranca
    {
        [Key]
        [Column("check_id")]
        public int CheckId { get; set; }

        [Required]
        [Column("status")]
        public string Status { get; set; }

        [Column("data_hora")]
        public DateTime DataHora { get; set; } = DateTime.Now;

        [Column("mensagem")]
        public string Mensagem { get; set; }

        [Column("localidade")]
        public string Localidade { get; set; }

        [Required]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
    }
}