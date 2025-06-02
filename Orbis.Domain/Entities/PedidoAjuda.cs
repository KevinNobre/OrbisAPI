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
        [Column("pedido_id")]
        public int PedidoId { get; set; }

        [Required]
        [Column("tipo_ajuda")]
        public string TipoAjuda { get; set; }

        [Required]
        [Column("urgencia")]
        public string Urgencia { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("localidade")]
        public string Localidade { get; set; }

        [Column("data_pedido")]
        public DateTime DataPedido { get; set; } = DateTime.Now;

        [Column("status")]
        public string Status { get; set; } = "pendente";

        [Required]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
    }
}
