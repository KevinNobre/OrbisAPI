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
        [Column("ONG_ID")]
        public int OngId { get; set; }

        [Required]
        [Column("NOME")]
        public string Nome { get; set; }

        [Column("LOCALIDADE")]
        public string Localidade { get; set; }

        [Column("TIPO_ONG")]
        public string TipoOng { get; set; }

        [Column("TELEFONE")]
        public string Telefone { get; set; }
    }
}
