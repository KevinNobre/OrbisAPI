using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
using Orbis.MLModels; 

[Route("api/[controller]")]
[ApiController]
public class PedidoAjudaMLController : ControllerBase
{
    private readonly string caminhoModelo = Path.Combine(Environment.CurrentDirectory, "wwwroot", "MLModels", "PedidoAjudaModel.zip");
    private readonly string caminhoTreinamento = Path.Combine(Environment.CurrentDirectory, "Data", "pedido_ajuda_urgencia_treino.csv");
    private readonly MLContext mlContext;

    public PedidoAjudaMLController()
    {
        mlContext = new MLContext();

        if (!System.IO.File.Exists(caminhoModelo))
        {
            Console.WriteLine("Modelo não encontrado. Iniciando treinamento...");
            TreinarModelo();
        }
    }

    private void TreinarModelo()
    {
        var pastaModelo = Path.GetDirectoryName(caminhoModelo);
        if (!Directory.Exists(pastaModelo))
        {
            Directory.CreateDirectory(pastaModelo);
            Console.WriteLine($"Diretório criado: {pastaModelo}");
        }

        // Carrega os dados do CSV (com urgencia para treino)
        IDataView dadosTreinamento = mlContext.Data.LoadFromTextFile<PedidoAjudaInput>(
            path: caminhoTreinamento, hasHeader: true, separatorChar: ',');

        // Define as transformações das features (input)
        var pipelineFeatures = mlContext.Transforms.Text.FeaturizeText("DescricaoFeaturizada", nameof(PedidoAjudaInput.Descricao))
            .Append(mlContext.Transforms.Text.FeaturizeText("TipoAjudaFeaturizado", nameof(PedidoAjudaInput.TipoAjuda)))
            .Append(mlContext.Transforms.Concatenate("Features", "TipoAjudaFeaturizado", "DescricaoFeaturizada"));

        // Transforma a coluna label (Urgencia) para chave para o treino
        var pipelineTreino = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(PedidoAjudaInput.Urgencia))
            .Append(pipelineFeatures)
            .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "Label", featureColumnName: "Features"))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        // Treina o modelo completo
        var modeloCompleto = pipelineTreino.Fit(dadosTreinamento);

        var modeloPredicao = modeloCompleto;

        // Salva o modelo preditivo para ser usado na predição (sem necessidade de Urgencia no input)
        mlContext.Model.Save(modeloPredicao, dadosTreinamento.Schema, caminhoModelo);

        Console.WriteLine($"Modelo de urgência treinado e salvo em: {caminhoModelo}");
    }

    public class PedidoAjudaPredicaoInput
    {
        public string TipoAjuda { get; set; }
        public string Descricao { get; set; }
        public string Urgencia { get; set; } // pode ser null ou string vazia
    }

    /// <summary>
    /// Realiza a predição da urgência com base no tipo e descrição do pedido de ajuda.
    /// </summary>
    /// <param name="entrada">Dados de entrada sem urgência</param>
    /// <returns>Urgência prevista</returns>
    [HttpPost("prever")]
    public ActionResult<PedidoAjudaPredicao> PreverUrgencia([FromBody] PedidoAjudaEntrada entrada)
    {
        if (!System.IO.File.Exists(caminhoModelo))
        {
            return BadRequest("O modelo ainda não foi treinado.");
        }

        ITransformer modelo;
        using (var stream = new FileStream(caminhoModelo, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            modelo = mlContext.Model.Load(stream, out var schema);
        }

        var engine = mlContext.Model.CreatePredictionEngine<PedidoAjudaPredicaoInput, PedidoAjudaPredicao>(modelo);

        var inputParaPredicao = new PedidoAjudaPredicaoInput
        {
            TipoAjuda = entrada.TipoAjuda,
            Descricao = entrada.Descricao,
            Urgencia = null // ou string.Empty
        };

        var resultado = engine.Predict(inputParaPredicao);

        return Ok(resultado);
    }
}
