using Microsoft.ML.Data;

namespace Orbis.MLModels
{
    // Usada para o TREINAMENTO
    public class PedidoAjudaInput
    {
        [LoadColumn(0)]
        public string TipoAjuda { get; set; }

        [LoadColumn(1)]
        public string Descricao { get; set; }

        [LoadColumn(2)]
        public string Urgencia { get; set; } 
    }

    // Usada como entrada de PREDIÇÃO
    public class PedidoAjudaEntrada
    {
        public string TipoAjuda { get; set; }

        public string Descricao { get; set; }
    }

    // Resultado da predição
    public class PedidoAjudaPredicao
    {
        [ColumnName("PredictedLabel")]
        public string Urgencia { get; set; }
    }
}
