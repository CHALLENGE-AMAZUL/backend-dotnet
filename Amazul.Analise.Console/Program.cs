public class Program
{
    public static void Main()
    {
        while(true)
        {
            try
            {
                var service = new PropostaService();

                int opc = 0;
                int idProposta;
                MenuConsole.ExibirMenu();
                int.TryParse(Console.ReadLine(), out opc);
                
                switch (opc)
                {
                    case 1:
                        service.ExibirLista(service.ListarPropostas());
                        break;
                    case 2:
                        Console.WriteLine("Insira o título da proposta: ");
                        var titulo = Console.ReadLine();
                        Console.WriteLine("Insira a URL da proposta: ");
                        var url = Console.ReadLine();
                        
                        service.CadastrarProposta(titulo, MenuConsole.LerAreaAplicacao(), url);
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
                        Console.WriteLine("Insira a justificativa para aprovação ou não: ");
                        string justificativa = Console.ReadLine()!;
                        service.AprovarProposta(MenuConsole.EntradaIdProposta(), justificativa);
                        break;
                    case 0:
                        return;
                }
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}
