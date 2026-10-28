public class PropostaConteudo {
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public enum AreaAplicacao Area { get; set; }
    //public enum Status StatusConteudo;
    public bool PropostaAprovada = false;
    public DateTime DataCriacao; 
}

//public enum Status
//{
//    RASCUNHO,
//    PUBLICADO,
//    ARQUIVADO
//}

public enum AreaAplicacao
{
    ENERGIA,
    SAUDE,
    AGRICULTURA,
    INDUSTRIA,
    PESQUISA
}