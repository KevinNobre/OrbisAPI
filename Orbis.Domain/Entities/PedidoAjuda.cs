using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbis.Domain.Entities
{
    [Table("tb_pedido_ajuda")]
    public class PedidoAjuda
    {
        [Key]
        [Column("PEDIDO_ID")]
        public int PedidoId { get; set; }

        [Required]
        [Column("TIPO_AJUDA")]
        public string TipoAjuda { get; set; }

        [Required]
        [Column("URGENCIA")]
        public string Urgencia { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Column("LOCALIDADE")]
        public string Localidade { get; set; }

        [Column("DATA_PEDIDO")]
        public DateTime DataPedido { get; set; } = DateTime.Now;

        [Column("STATUS")]
        public string Status { get; set; } = "pendente";

        [Required]
        [Column("USUARIO_ID")]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }
    }
}
