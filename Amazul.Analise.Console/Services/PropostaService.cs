public class PropostaService
{

	public void ListarPropostas()
	{
		try
		{
			string nomeArquivo = "arquivo.json";

			if (!File.Exists(nomeArquivo))
			{
				Console.WriteLine("Arquivo não encontrado!");
				return;
			}

			string jsonString = File.ReadAllText(nomeArquivo);

			PropostaConteudo proposta = JsonSerializer.Deserialize<PropostaConteudo>(jsonString);


		}
		catch (JsonException ex)
		{
			Console.WriteLine("Erro no formato do json!");

		}

		catch (Exception ex)
		{
			Console.WriteLine($"Erro: {ex.Message}");
		}


	}

	public void ListarPropostasNaoAprovadas()
	{
	}

	public PropostaConteudo CadastrarProposta() {]
	}

	public PropostaConteudo AnalisarProposta(int id) {
	}

	public void AprovarProposta(int id) {
		//Setar BOOL -> TRUE para aprovar Proposta
		
	}
}