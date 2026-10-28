using System.Diagnostics.CodeAnalysis;

public class PropostaConteudo {
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public AreaAplicacao Area { get; set; }
    public string Url { get; set; } = string.Empty;
    public Status StatusAnalise { get; set; }
    public string JustificativaAnalise { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
}

public enum Status
{
    EmAnalise,
    Aprovada,
    Rejeitada
}
public enum AreaAplicacao
{
    Energia = 1,
    Saude = 2,
    Agricultura = 3,
    Industria = 4,
    Pesquisa = 5
}