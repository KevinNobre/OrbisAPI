using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbis.Domain.Entities
{
    [Table("tb_oferta_doacao")]
    public class OfertaDoacao
    {
        [Key]
        [Column("oferta_id")]
        public int OfertaId { get; set; }

        [Required]
        [Column("tipo_doacao")]
        public string TipoDoacao { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("quantidade")]
        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; }

        [Column("localidade")]
        public string Localidade { get; set; }

        [Column("data_oferta")]
        public DateTime DataOferta { get; set; } = DateTime.Now;

        [Column("status")]
        public string Status { get; set; } = "disponível";

        [Required]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
    }
}
