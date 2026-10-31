public class Program
{
    public static void Main()
    {
        var service = new PropostaService();

        while (true)
        {
            try
            {
                MenuConsole.ExibirMenu();
                if (!int.TryParse(Console.ReadLine(), out int opc))
                {
                    opc = -1;
                }

                switch (opc)
                {
                    case 1:
                        service.ExibirLista(service.ListarPropostas());
                        break;
                    case 2:
                        Console.WriteLine("Insira o título da proposta (mín. 5 caracteres): ");
                        var titulo = Console.ReadLine() ?? string.Empty;
                        var areaCadastro = MenuConsole.LerAreaAplicacao();
                        Console.WriteLine("Insira a URL da fonte científica (iniciando com http:// ou https://): ");
                        var url = Console.ReadLine() ?? string.Empty;

                        service.CadastrarProposta(titulo, areaCadastro, url);
                        break;
                    case 3:
                        service.ExibirLista(service.ListarPropostasEmAnalise());
                        break;
                    case 4:
                        var areaInserida = MenuConsole.LerAreaAplicacao();
                        service.ExibirLista(service.ListarPropostasPorArea(areaInserida));
                        break;
                    case 5:
                        service.ExibirProposta(MenuConsole.EntradaIdProposta());
                        break;
                    case 6:
                        int idAprovar = MenuConsole.EntradaIdProposta();
                        Console.WriteLine("Insira a justificativa técnica para aprovação (mín. 10 caracteres): ");
                        string justificativaAprovacao = Console.ReadLine() ?? string.Empty;
                        service.AprovarProposta(idAprovar, justificativaAprovacao);
                        break;
                    case 7:
                        int idRejeitar = MenuConsole.EntradaIdProposta();
                        Console.WriteLine("Insira a justificativa técnica para rejeição (mín. 10 caracteres): ");
                        string justificativaRejeicao = Console.ReadLine() ?? string.Empty;
                        service.RejeitarProposta(idRejeitar, justificativaRejeicao);
                        break;
                    case 0:
                        Console.WriteLine("Encerrando o sistema de curadoria...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida! Por favor, escolha uma opção de 0 a 7.");
                        break;
                }

                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }
        }
    }
}
