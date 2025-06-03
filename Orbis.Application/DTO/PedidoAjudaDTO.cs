using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbis.Application.DTO
{
    public class PedidoAjudaDto
    {
        public int PedidoId { get; set; }
        public string TipoAjuda { get; set; }
        public string Urgencia { get; set; }
        public string Descricao { get; set; }
        public string Localidade { get; set; }
        public string Status { get; set; }

        public Dictionary<string, LinkDto> _links { get; set; }
    }

    public class LinkDto
    {
        public string Href { get; set; }
        public string Method { get; set; }

        public LinkDto(string href, string method)
        {
            Href = href;
            Method = method;
        }
    }

}
