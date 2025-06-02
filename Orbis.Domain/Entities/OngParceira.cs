using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbis.Domain.Entities
{
    [Table("tb_ong_parceiras")]
    public class OngParceira
    {
        [Key]
        [Column("ong_id")]
        public int OngId { get; set; }

        [Required]
        [Column("nome")]
        public string Nome { get; set; }

        [Column("localidade")]
        public string Localidade { get; set; }

        [Column("tipo_ong")]
        public string TipoOng { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }
    }
}
