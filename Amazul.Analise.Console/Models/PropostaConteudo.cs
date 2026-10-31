public class PropostaConteudo
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public AreaAplicacao Area { get; set; }
    public string Url { get; set; } = string.Empty;
    public Status StatusAnalise { get; set; }
    public string JustificativaAnalise { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }

    public bool ValidarCadastro(out string mensagemErro)
    {
        if (string.IsNullOrWhiteSpace(Titulo) || Titulo.Trim().Length < 5)
        {
            mensagemErro = "Título inválido! Deve conter pelo menos 5 caracteres.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Url) ||
            (!Url.StartsWith("http", StringComparison.OrdinalIgnoreCase)))
        {
            mensagemErro = "URL inválida! Deve iniciar com http:// ou https://.";
            return false;
        }

        if (!Enum.IsDefined(typeof(AreaAplicacao), Area))
        {
            mensagemErro = "Área de aplicação inválida!";
            return false;
        }

        mensagemErro = string.Empty;
        return true;
    }

    public static bool ValidarJustificativa(string justificativa, out string mensagemErro)
    {
        if (string.IsNullOrWhiteSpace(justificativa) || justificativa.Trim().Length < 10)
        {
            mensagemErro = "Justificativa insuficiente, deve ter no mínimo 10 caracteres válidos!";
            return false;
        }

        mensagemErro = string.Empty;
        return true;
    }
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
