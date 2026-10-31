using System.Text.Encodings.Web;
using System.Text.Json;

public class PropostaService
{
	private static readonly string _nomeArquivo = ResolverCaminhoArquivo();
	private static int _idAtual = 1;

	private static string ResolverCaminhoArquivo()
	{
		// 1. Procura a pasta do projeto (onde está o .csproj) subindo a partir de AppContext.BaseDirectory (ex: bin/Debug/net10.0)
		var diretorio = new DirectoryInfo(AppContext.BaseDirectory);
		while (diretorio != null)
		{
			string csproj = Path.Combine(diretorio.FullName, "Amazul.Console.csproj");
			string candidatoProjeto = Path.Combine(diretorio.FullName, "propostas.json");
			if (File.Exists(csproj))
			{
				return candidatoProjeto;
			}

			string candidatoSubpasta = Path.Combine(diretorio.FullName, "Amazul.Analise.Console", "propostas.json");
			if (File.Exists(candidatoSubpasta))
			{
				return candidatoSubpasta;
			}

			diretorio = diretorio.Parent;
		}

		// 2. Fallback para o diretório de trabalho atual
		string subpastaAtual = Path.Combine("Amazul.Analise.Console", "propostas.json");
		if (File.Exists(subpastaAtual))
		{
			return Path.GetFullPath(subpastaAtual);
		}

		return Path.GetFullPath("propostas.json");
	}
	
	public void ExibirLista(List<PropostaConteudo> lista)
	{
		if (!lista.Any())
		{
			Console.WriteLine("Nenhuma proposta foi encontrada na lista!");
			return;
		}
			
		foreach (var p in lista)
		{
			ImprimirDetalhesProposta(p);
		}
	}

	private static void ImprimirDetalhesProposta(PropostaConteudo p)
	{
		Console.WriteLine("ID: " + p.Id);
		Console.WriteLine("Título: " + p.Titulo);
		Console.WriteLine("Área: " + p.Area);
		Console.WriteLine("Url: " + p.Url);
		Console.WriteLine("Status da Análise de proposta: " + p.StatusAnalise);
		Console.WriteLine("Justificativa de aprovação/recusa: " + (string.IsNullOrWhiteSpace(p.JustificativaAnalise) ? "Pendente de análise" : p.JustificativaAnalise));
		Console.WriteLine("Data da criação da proposta: " + p.DataCriacao.ToString("dd/MM/yyyy HH:mm"));
		Console.WriteLine("---------------------------------------");
	}

	public List<PropostaConteudo> ListarPropostas()
	{
		try
		{
			if (!File.Exists(_nomeArquivo))
			{
				return new List<PropostaConteudo>();
			}
			
			string jsonRetorno = File.ReadAllText(_nomeArquivo);
			if (string.IsNullOrWhiteSpace(jsonRetorno))
			{
				return new List<PropostaConteudo>();
			}

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
				WriteIndented = true,
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			};
			string json = JsonSerializer.Serialize(propostas, complemento);
			File.WriteAllText(_nomeArquivo, json);
		}
		catch(Exception ex)
		{
			Console.WriteLine($"Erro ao persistir registro de propostas na lista no arquivo JSON: {_nomeArquivo} - {ex.Message}");
		}
	}
	
	public PropostaConteudo? CadastrarProposta(string titulo, AreaAplicacao areaAplicacao, string url)
	{
		var listaPropostas = ListarPropostas();
		
		PropostaConteudo proposta = new PropostaConteudo
		{
			Id = listaPropostas.Any() ? listaPropostas.Max(p => p.Id) + 1 : _idAtual,
			Titulo = titulo.Trim(),
			Area = areaAplicacao,
			Url = url.Trim(),
			StatusAnalise = Status.EmAnalise,
			DataCriacao = DateTime.Now
		};

		if (!proposta.ValidarCadastro(out string mensagemErro))
		{
			Console.WriteLine(mensagemErro);
			return null;
		}
		
		listaPropostas.Add(proposta);
		Persistir(listaPropostas);
		Console.WriteLine("Proposta cadastrada com sucesso!");
		_idAtual++;
		return proposta;
	}

	public PropostaConteudo? ExibirProposta(int id)
	{
		var p = BuscarPorId(id);

		if (p == null)
		{
			Console.WriteLine("Proposta não encontrada!");
			return null;
		}
			
		ImprimirDetalhesProposta(p);
		return p;
	}

	public void AprovarProposta(int id, string justificativa)
	{
		var listaPropostas = ListarPropostas();
		var proposta = listaPropostas.Find(p => p.Id == id);
		
		if (proposta == null)
		{
			Console.WriteLine("Proposta não encontrada!");
			return;
		}

		if (proposta.StatusAnalise == Status.Rejeitada)
		{
			Console.WriteLine("Operação negada: esta proposta já foi rejeitada anteriormente!");
			return;
		}
		else if (proposta.StatusAnalise == Status.Aprovada)
		{
			Console.WriteLine("Operação negada: esta proposta já foi aprovada anteriormente!");
			return;
		}

		if (!PropostaConteudo.ValidarJustificativa(justificativa, out string mensagemErro))
		{
			Console.WriteLine(mensagemErro);
			return;
		}
		
		proposta.JustificativaAnalise = justificativa.Trim();
		proposta.StatusAnalise = Status.Aprovada;
		Persistir(listaPropostas);
		Console.WriteLine("Proposta de conteúdo aprovada com sucesso!");
	}

	public void RejeitarProposta(int id, string justificativa)
	{
		var listaPropostas = ListarPropostas();
		var proposta = listaPropostas.Find(p => p.Id == id);
		
		if (proposta == null)
		{
			Console.WriteLine("Proposta não encontrada!");
			return;
		}

		if (proposta.StatusAnalise == Status.Aprovada)
		{
			Console.WriteLine("Operação negada: esta proposta já foi aprovada anteriormente!");
			return;
		}
		else if (proposta.StatusAnalise == Status.Rejeitada)
		{
			Console.WriteLine("Operação negada: esta proposta já foi rejeitada anteriormente!");
			return;
		}

		if (!PropostaConteudo.ValidarJustificativa(justificativa, out string mensagemErro))
		{
			Console.WriteLine(mensagemErro);
			return;
		}
		
		proposta.JustificativaAnalise = justificativa.Trim();
		proposta.StatusAnalise = Status.Rejeitada;
		Persistir(listaPropostas);
		Console.WriteLine("Proposta de conteúdo rejeitada com sucesso!");
	}
}
