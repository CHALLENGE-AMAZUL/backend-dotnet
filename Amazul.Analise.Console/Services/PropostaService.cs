using System.Text.Json;
public class PropostaService
{
	private static readonly string _nomeArquivo = "propostas.json";
	private static int _idAtual = 1;
	
	public void ExibirLista(List<PropostaConteudo> lista)
	{
		if (!lista.Any())
		{
			Console.WriteLine("Nenhuma proposta foi encontrada na lista!");
		}
			
		foreach (var p in lista)
		{
			Console.WriteLine("ID: "  + p.Id);
			Console.WriteLine("Titulo: " + p.Titulo);
			Console.WriteLine("Área: " + p.Area);
			Console.WriteLine("Url: " + p.Url);
			Console.WriteLine("Status da Análise de proposta: " + p.StatusAnalise);
			Console.WriteLine("Justificativa de aprovação ou não: " + p.JustificativaAnalise);
			Console.WriteLine("Data da criação da proposta: " + p.DataCriacao);
		}
	}
	public List<PropostaConteudo> ListarPropostas()
	{
		try
		{
			if (!File.Exists(_nomeArquivo))
			{
				Console.WriteLine("Arquivo não encontrado!");
				return new List<PropostaConteudo>();
			}
			
			string jsonRetorno = File.ReadAllText(_nomeArquivo);
			return JsonSerializer.Deserialize<List<PropostaConteudo>>(jsonRetorno) ?? new List<PropostaConteudo>();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Erro ao ler arquivo JSON: {ex.Message}");
			return new List<PropostaConteudo>();
		}
	}

	public List<PropostaConteudo> ListarPropostasEmAnalise()
	{
		var listaTodasPropostas = ListarPropostas();
		return listaTodasPropostas.Where(p => p.StatusAnalise == Status.EmAnalise).ToList();
	}
	
	public List<PropostaConteudo> ListarPropostasPorArea(AreaAplicacao areaAplicacao)
	{
		var listaTodasPropostas = ListarPropostas();
		return listaTodasPropostas.Where(p => p.Area == areaAplicacao).ToList();
	}

	public PropostaConteudo? BuscarPorId(int id)
	{
		var listaPropostas = ListarPropostas();
		return listaPropostas.FirstOrDefault(p => p.Id == id);
	}

	public void Persistir(List<PropostaConteudo> propostas)
	{
		try
		{
			var complemento = new JsonSerializerOptions
			{
				WriteIndented = true
			};
			string json = JsonSerializer.Serialize(propostas, complemento);
			File.WriteAllText(_nomeArquivo, json);
		}
		catch(Exception ex)
		{
			Console.WriteLine($"Erro ao persistir registro de propostas na lista no arquivo JSON:  {_nomeArquivo} - {ex.Message}");
		}
	}
	
	public PropostaConteudo? CadastrarProposta(string titulo, AreaAplicacao areaAplicacao, string url)
	{
		var listaPropostas = ListarPropostas();
		
		PropostaConteudo proposta = new PropostaConteudo();
		proposta.Id = listaPropostas.Any() ? listaPropostas.Max(p => p.Id) + 1 : _idAtual;

		if (string.IsNullOrEmpty(titulo) || titulo.Length < 5)
		{
			Console.WriteLine("Título inválido!");
			return null;
		}
		proposta.Titulo = titulo;
		
		if (string.IsNullOrWhiteSpace(url) || !url.StartsWith("http") )
		{
			Console.WriteLine("Url inválida!");
			return null;
		}
		proposta.Url = url;
		
		proposta.Area = areaAplicacao;
		proposta.StatusAnalise = Status.EmAnalise;
		proposta.DataCriacao = DateTime.Now;
		
		listaPropostas.Add(proposta);
		Persistir(listaPropostas);
		Console.WriteLine("Proposta cadastrada com sucesso!");
		_idAtual++;
		return proposta;
	}

	public PropostaConteudo? ExibirProposta(int id)
	{
		var proposta = BuscarPorId(id);

		if (proposta == null)
		{
			Console.WriteLine("Proposta não encontrada!");
			return null;
		}
			
		
		return proposta;
	}

	public void AprovarProposta(int id, string justificativa) {
		var listaPropostas = ListarPropostas();
		var proposta = listaPropostas.Find(p => p.Id == id);
		
		if (proposta == null)
		{
			Console.WriteLine("Proposta não encontrada!");
			return;
		}

		if (proposta.StatusAnalise == Status.Rejeitada)
		{
			return;
		}

		if (justificativa.Length < 10)
		{
			Console.WriteLine("Justificativa insufuciente, deve ter no mínimo 10 caracteres!");
			return;
		}
		
		proposta.JustificativaAnalise = justificativa;
		proposta.StatusAnalise = Status.Aprovada;
		Persistir(listaPropostas);
		Console.WriteLine("Proposta de conteúdo aprovada com sucesso!");
	}
}