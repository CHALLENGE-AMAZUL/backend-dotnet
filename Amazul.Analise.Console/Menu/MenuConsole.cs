public static class MenuConsole
{
    public static void ExibirMenu()
    {
        if (!Console.IsOutputRedirected)
        {
            Console.Clear();
        }
        Console.WriteLine("Menu de Propostas de Conteúdo - Lumar (AMAZUL)");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("1.   Listar todas as propostas");
        Console.WriteLine("2.   Cadastrar nova proposta");
        Console.WriteLine("3.   Listar as propostas em análise");
        Console.WriteLine("4.   Listar as propostas por área de aplicação");
        Console.WriteLine("5.   Buscar proposta por ID");
        Console.WriteLine("6.   Aprovar proposta");
        Console.WriteLine("7.   Rejeitar proposta");
        Console.WriteLine("0.   Sair");
        Console.WriteLine("\nSelecione uma opção: ");
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
            default:
                throw new ArgumentException("Área inválida! Escolha uma opção de 1 a 5.");
        }
    }

    public static AreaAplicacao LerAreaAplicacao()
    {
        Console.WriteLine("\nSelecione a Área de Aplicação:");
        Console.WriteLine("1.   Energia");
        Console.WriteLine("2.   Saúde");
        Console.WriteLine("3.   Agricultura");
        Console.WriteLine("4.   Indústria");
        Console.WriteLine("5.   Pesquisa");

        if (!int.TryParse(Console.ReadLine(), out int opcArea))
        {
            throw new ArgumentException("Entrada inválida! Digite um número correspondente à área (1 a 5).");
        }

        return MapeamentoAreaAplicacao(opcArea);
    }

    public static int EntradaIdProposta()
    {
        Console.WriteLine("Insira o ID da proposta: ");
        if (!int.TryParse(Console.ReadLine(), out int idProposta) || idProposta <= 0)
        {
            throw new ArgumentException("ID inválido! Digite um número inteiro positivo.");
        }

        return idProposta;
    }

    public static void AguardarTecla()
    {
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        if (!Console.IsInputRedirected)
        {
            Console.ReadKey();
        }
        else
        {
            Console.ReadLine();
        }
    }
}
