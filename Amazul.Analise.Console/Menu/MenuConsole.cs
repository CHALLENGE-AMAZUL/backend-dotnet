public static class MenuConsole
{
    public static void ExibirMenu()
    {
        Console.Clear();
        Console.WriteLine("Menu de Propostas de Conteúdo.");
        Console.WriteLine("---------------------------");
        Console.WriteLine("1.   Listar todas as propostas");
        Console.WriteLine("2.   Cadastrar nova proposta");
        Console.WriteLine("3.   Listar as propostas em análise");
        Console.WriteLine("4.   Listar as propostas por área de aplicação");
        Console.WriteLine("5.   Analisar proposta");
        Console.WriteLine("6.   Aprovar proposta");
        Console.WriteLine("0.   Sair");
        Console.WriteLine("\nSelecione uma opção: ");
    }

    public static void InsercaoDadosProposta(string titulo, AreaAplicacao area, string url)
    {
        //Ideia de como seriam os campos de proposta - Justificativa viria apenas na aprovação ou nao.
        Console.Clear();
        Console.WriteLine("Criação de proposta.");
        Console.WriteLine("---------------------------");
    }
    private static AreaAplicacao MapeamentoAreaAplicacao(int opcArea)
    {
        switch (opcArea)
        {
            case 1:
                return AreaAplicacao.Energia;
            case 2:
                return AreaAplicacao.Saude;
            case 3:
                return AreaAplicacao.Agricultura;
            case 4:
                return AreaAplicacao.Industria;
            case 5:
                return AreaAplicacao.Pesquisa;
        }
        throw new ArgumentException("Área inválida! Escolha de 1 a 5!");
    }

    public static AreaAplicacao LerAreaAplicacao()
    {
        Console.Clear();
        Console.WriteLine("1.   Energia");
        Console.WriteLine("2.   Saúde");
        Console.WriteLine("3.   Agricultura");
        Console.WriteLine("4.   Indústria");
        Console.WriteLine("5.   Pesquisa");
        
        var opcArea = int.Parse(Console.ReadLine()!);
        return MapeamentoAreaAplicacao(opcArea);
    }

    public static int EntradaIdProposta()
    {
        Console.WriteLine("Insira o ID da proposta: ");
        var idProposta = int.Parse(Console.ReadLine()!);
        return idProposta;
    }
}