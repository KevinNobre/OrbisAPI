using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbis.Domain.Entities
{
    [Table("tb_match_ajuda")]
    public class MatchAjuda
    {
        [Key]
        [Column("match_id")]
        public int MatchId { get; set; }

        [Column("data_match")]
        public DateTime DataMatch { get; set; } = DateTime.Now;

        [Column("status")]
        public string Status { get; set; } = "pendente";

        [Required]
        [Column("pedido_id")]
        public int PedidoId { get; set; }

        [Required]
        [Column("oferta_id")]
        public int OfertaId { get; set; }

        [ForeignKey("PedidoId")]
        public PedidoAjuda Pedido { get; set; }

        [ForeignKey("OfertaId")]
        public OfertaDoacao Oferta { get; set; }
    }
}
