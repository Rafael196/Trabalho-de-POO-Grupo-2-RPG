# CLASS_CONTRACTS — Campus Quest
> Assinaturas públicas de todas as classes do sistema.
> Use para pedir à IA que implemente uma classe específica já com o contrato definido.
> Nunca altere um contrato sem atualizar este arquivo.

---

## Core

### `Personagem` (abstract) — `src/Core/Personagem.cs`
```csharp
public abstract class Personagem
{
    protected int vida;
    protected int vidaMaxima;
    protected int conhecimento;

    public int Vida          { get; protected set; }
    public int VidaMaxima    { get; protected set; }
    public int Conhecimento  { get; protected set; }

    public abstract void Atacar(Personagem alvo);
    public void ReceberDano(int dano);          // reduz vida, mínimo 0
    public bool EstaVivo();                     // retorna vida > 0
    public void RestaurarVida(int quantidade);  // não ultrapassa VidaMaxima
}
```

### `Aluno` — `src/Core/Aluno.cs`
```csharp
public class Aluno : Personagem
{
    public string Nome                     { get; private set; }
    public Inventario Inventario           { get; private set; }
    public List<Habilidade> Habilidades    { get; private set; }
    public int SemestreAtual               { get; private set; }  // 1, 2 ou 3
    public int[] Aproveitamentos           { get; private set; }  // [0..2], escala 0-100
    public bool BonusCoragemAplicado       { get; private set; }
    public bool ColaAtiva                  { get; private set; }
    public int CafesRecebidosSemestre      { get; private set; }
    public int CadernosRecebidosSemestre   { get; private set; }
    public int LivrosRecebidosSemestre     { get; private set; }

    public void DefinirNome(string nome);
    public override void Atacar(Personagem alvo);
    public void AdicionarHabilidade(Habilidade h);
    public bool PossuiHabilidade(string nome);
    public void RegistrarAproveitamento(int semestre, int valor); // semestre 1-3
    public float GetMediaFinal();                                 // média dos 3 aproveitamentos
    public void AumentarConhecimento(int quantidade);
    public void AvancarSemestre();
    public bool SemestresCompletos();                             // retorna SemestreAtual > 3
    public void AtivarCola();
    public void DesativarCola();
    public bool PodeReceberItem(Item item);
    public void RegistrarItemRecebido(Item item);
    public void DefinirContagemItensSemestre(int cafes, int cadernos, int livros);
}
```

### `Habilidade` — `src/Core/Habilidade.cs`
```csharp
public class Habilidade
{
    public string Nome        { get; }
    public string Descricao   { get; }
    public TipoEfeito Efeito  { get; }   // enum: AumentaDano, ReducDano, DuploAtaque

    public Habilidade(string nome, string descricao, TipoEfeito efeito);
    public void Aplicar(Aluno aluno, ContextoExame contexto);
}

public enum TipoEfeito { AumentaDano, ReducaoDano, DuploAtaque }
```

### `Inventario` — `src/Core/Inventario.cs`
```csharp
public class Inventario
{
    public const int CapacidadeMaxima = 6;
    public int Quantidade { get; }

    public bool Adicionar(Item item);       // retorna false se cheio
    public bool Remover(Item item);         // retorna false se não encontrado
    public bool EstaCheia();
    public List<Item> ListarItens();
    public Item BuscarPorTipo<T>() where T : Item;
}
```

---

## Matérias (Chefes)

### `Materia` (abstract) — `src/Materias/Materia.cs`
```csharp
public abstract class Materia
{
    protected int vidaAtual;
    protected int vidaMaxima;
    public string Nome { get; protected set; }

    public abstract void Atacar(Aluno aluno);
    public abstract void AtaqueEspecial(Aluno aluno);   // ataque temático único
    public abstract List<Pergunta> GetPerguntasExame(); // banco exclusivo, mín. 8 perguntas
    public void ReceberDano(int dano);
    public bool EstaVencido();
    public int GetVidaAtual();
    public int GetVidaMaxima();
    public string GetDescricaoAtaque();                 // descrição narrativa do ataque especial
}
```

### `IC` — `src/Materias/IC.cs`
```csharp
public class IC : Materia
{
    // AtaqueEspecial: Loop Infinito — dano contínuo por 2 perguntas consecutivas
    public override void Atacar(Aluno aluno);
    public override void AtaqueEspecial(Aluno aluno);
    public override List<Pergunta> GetPerguntasExame();
}
```

### `AED` — `src/Materias/AED.cs`
```csharp
public class AED : Materia
{
    // AtaqueEspecial: Stack Overflow — dano único muito alto em uma pergunta
    public override void Atacar(Aluno aluno);
    public override void AtaqueEspecial(Aluno aluno);
    public override List<Pergunta> GetPerguntasExame();
}
```

### `POO` — `src/Materias/POO.cs`
```csharp
public class POO : Materia
{
    // AtaqueEspecial: NullPointerException — anula multiplicador de Conhecimento por 1 turno
    public override void Atacar(Aluno aluno);
    public override void AtaqueEspecial(Aluno aluno);
    public override List<Pergunta> GetPerguntasExame();
}
```

### `TCC` — `src/Materias/TCC.cs`
```csharp
public class TCC : Materia
{
    // Recebe a média final do aluno no construtor para escalar a dificuldade
    public TCC(float mediaFinalAluno);

    // AtaqueEspecial: Síntese Total — combina os 3 ataques anteriores escalados pela média
    public override void Atacar(Aluno aluno);
    public override void AtaqueEspecial(Aluno aluno);
    public override List<Pergunta> GetPerguntasExame();
}
```

---

## NPCs

### `NPC` (abstract) — `src/NPCs/NPC.cs`
```csharp
public abstract class NPC
{
    public string Nome { get; protected set; }

    public abstract void Interagir(Aluno aluno);
    public abstract string GetDialogo();   // retorna linha de diálogo atual
    public virtual string GetMensagemAbertura(Aluno aluno);
    public virtual string ObterDica(Aluno aluno);
    public virtual Item SolicitarItem(Aluno aluno);
    public virtual string MensagemItemIndisponivel(Aluno aluno);
}
```

### `Professor` — `src/NPCs/Professor.cs`
```csharp
public class Professor : NPC
{
    public override void Interagir(Aluno aluno);
    public override string GetMensagemAbertura(Aluno aluno);
    public bool SugerirQuiz();                              // retorna true se aluno aceitar
    public int EscolherSemestreQuiz(int semestreAtual);     // retorna semestre escolhido (1-3)
    public ResultadoQuiz AplicarQuiz(Aluno aluno, int semestre);
    public override string GetDialogo();
    public override string ObterDica(Aluno aluno);
    public override Item SolicitarItem(Aluno aluno);
    public override string MensagemItemIndisponivel(Aluno aluno);
}
```

### `Veterano` — `src/NPCs/Veterano.cs`
```csharp
public class Veterano : NPC
{
    public override void Interagir(Aluno aluno);
    public override string GetMensagemAbertura(Aluno aluno);
    public string DarDica(int semestreAtual);   // dica temática sobre o chefe atual
    public Item OfereceItem();                  // retorna item disponível para o aluno
    public override string GetDialogo();
    public override string ObterDica(Aluno aluno);
    public override Item SolicitarItem(Aluno aluno);
}
```

### `Coordenador` — `src/NPCs/Coordenador.cs`
```csharp
public class Coordenador : NPC
{
    public override void Interagir(Aluno aluno);
    public override string GetMensagemAbertura(Aluno aluno);
    public bool TrancarSemestre(Aluno aluno, IRepositorio repo); // salva e retorna ao menu
    public override string GetDialogo();
    public override string ObterDica(Aluno aluno);
    public override Item SolicitarItem(Aluno aluno);
    public override string MensagemItemIndisponivel(Aluno aluno);
}
```

---

## Itens

### `Item` (abstract) — `src/Itens/Item.cs`
```csharp
public abstract class Item
{
    public string Nome       { get; protected set; }
    public string Descricao  { get; protected set; }

    public abstract void Usar(Aluno aluno);
    public abstract string GetDescricaoEfeito();
}
```

### `Cafe` — `src/Itens/Cafe.cs`
```csharp
public class Cafe : Item
{
    public const int RestauracaoVida = 25;
    public override void Usar(Aluno aluno);      // restaura 25 de vida
    public override string GetDescricaoEfeito();
}
```

### `Caderno` — `src/Itens/Caderno.cs`
```csharp
public class Caderno : Item
{
    public const int BonusConhecimento = 15;
    public override void Usar(Aluno aluno);      // +15 Conhecimento temporário (próximo exame)
    public override string GetDescricaoEfeito();
}
```

### `LivroTecnico` — `src/Itens/LivroTecnico.cs`
```csharp
public class LivroTecnico : Item
{
    public string MateriaAlvo { get; }           // nome da matéria que recebe o bônus
    public const int BonusConhecimento = 20;

    public LivroTecnico(string materiaAlvo);
    public override void Usar(Aluno aluno);      // +20 Conhecimento permanente vs matéria alvo
    public override string GetDescricaoEfeito();
}
```

### `Cola` — `src/Itens/Cola.cs`
```csharp
public class Cola : Item
{
    public override void Usar(Aluno aluno);      // ativa acerto garantido na próxima pergunta do exame
    public override string GetDescricaoEfeito();
}
```

---

## Quiz

### `Pergunta` — `src/Quiz/Pergunta.cs`
```csharp
public class Pergunta
{
    public string Enunciado           { get; }
    public string[] Alternativas      { get; }  // sempre 4 alternativas
    public int IndiceCorreto          { get; }  // 0-3
    public string Explicacao          { get; }  // exibida após resposta errada

    public Pergunta(string enunciado, string[] alternativas, int indiceCorreto, string explicacao);
    public bool VerificarResposta(int indiceEscolhido);
}
```

### `SistemaQuiz` — `src/Quiz/SistemaQuiz.cs`
```csharp
public class SistemaQuiz
{
    public ResultadoQuiz Executar(Aluno aluno, int semestre);
    // Retorna ResultadoQuiz com: aproveitamento (0-100), ganhoConhecimento, itemDropado
}

public class ResultadoQuiz
{
    public int Aproveitamento    { get; }   // 0-100
    public int GanhoConhecimento { get; }
    public Item ItemDropado      { get; }   // Cafe / Caderno / LivroTecnico
}
```

---

## Exame

### `ContextoExame` — `src/Exame/SistemaExame.cs`
```csharp
public class ContextoExame
{
    public bool MultiplicadorAnulado { get; set; }  // usado pelo ataque NullPointer da POO
    public bool DanoContínuo         { get; set; }  // usado pelo Loop Infinito da IC
    public int TurnosComDano         { get; set; }
}
```

### `SistemaExame` — `src/Exame/SistemaExame.cs`
```csharp
public class SistemaExame
{
    public ResultadoExame Executar(Aluno aluno, Materia chefe);
    // Gerencia turnos, aplica fórmulas de dano, verifica vitória/derrota
    // Aplica habilidades passivas do aluno no contexto
    // Registra aproveitamento via aluno.RegistrarAproveitamento()
}

public class ResultadoExame
{
    public bool Vitoria              { get; }
    public int Aproveitamento        { get; }   // 0-100, apenas se vitória
    public bool BonusCoragemAplicado { get; }
    public int AcertosTotal          { get; }
    public int ErrosTotal            { get; }
}
```

### `CalculadoraMedia` — `src/Exame/CalculadoraMedia.cs`
```csharp
public static class CalculadoraMedia
{
    public static float Calcular(int[] aproveitamentos);  // média dos 3 semestres
    public static FaixaMedia ClassificarFaixa(float media);
    // Retorna enum FaixaMedia { Alta, Media, Baixa }
}

public enum FaixaMedia { Alta, Media, Baixa }
// Alta  >= 80: bônus de Conhecimento extra contra TCC
// Media 50-79: batalha equilibrada
// Baixa  < 50: TCC começa com ataque inicial mais forte
```

---

## Persistência

### `IRepositorio` (interface) — `src/Persistencia/IRepositorio.cs`
```csharp
public interface IRepositorio
{
    void Salvar(EstadoJogo estado);
    EstadoJogo Carregar();
    bool ExisteArquivo();
    void Deletar();
}
```

### `EstadoJogo` — `src/Persistencia/EstadoJogo.cs`
```csharp
public class EstadoJogo
{
    public string Versao             { get; set; }
    public DateTime DataSalvamento   { get; set; }
    public DadosAluno Aluno          { get; set; }
    public Estatisticas Estatisticas { get; set; }

    public string NomeAluno          { get; set; }
    public int VidaAtual             { get; set; }
    public int Conhecimento          { get; set; }
    public int SemestreAtual         { get; set; }
}

public class DadosAluno
{
    public string Nome                { get; set; }
    public int VidaAtual              { get; set; }
    public int VidaMaxima             { get; set; }
    public int Conhecimento           { get; set; }
    public int SemestreAtual          { get; set; }
    public List<string> Habilidades   { get; set; }
    public int[] Aproveitamentos      { get; set; }  // [3]
    public List<ItemSalvo> Inventario { get; set; }
    public int CafesRecebidosSemestre    { get; set; }
    public int CadernosRecebidosSemestre { get; set; }
    public int LivrosRecebidosSemestre   { get; set; }
}

public class Estatisticas
{
    public int SemestresRepetidos    { get; set; }
    public int QuizzesConcluidos     { get; set; }
    public int ItensUsados           { get; set; }
    public bool BonusCoragemAplicado { get; set; }
}
```

### `RepositorioJson` — `src/Persistencia/RepositorioJson.cs`
```csharp
public class RepositorioJson : IRepositorio
{
    public RepositorioJson(string caminhoArquivo);

    public void Salvar(EstadoJogo estado);      // serializa JSON formatado
    public EstadoJogo Carregar();               // lança InvalidOperationException se não existe
    public bool ExisteArquivo();
    public void Deletar();
}
```

---

## Estados (padrão State)

### `IEstadoJogo` (interface) — `src/Estados/IEstadoJogo.cs`
```csharp
public interface IEstadoJogo
{
    void Entrar(JogoContexto contexto);   // executado ao ativar o estado
    void Executar(JogoContexto contexto); // loop principal do estado
    void Sair(JogoContexto contexto);     // executado ao desativar
}
```

### `JogoContexto` — `src/Estados/JogoContexto.cs`
```csharp
public class JogoContexto
{
    public Aluno AlunoAtivo          { get; set; }
    public IRepositorio Repositorio  { get; set; }
    public IEstadoJogo EstadoAtual   { get; private set; }

    public void MudarEstado(IEstadoJogo novoEstado);
    public void Iniciar();   // entra no EstadoMenu e inicia o loop
}
```

**Estados implementados:**
- `EstadoMenu` — menu principal (Novo / Carregar / Sair)
- `EstadoExplorando` — navegação pelo Hall e salas
- `EstadoExame` — Exame de Aproveitamento em andamento
- `EstadoGameOver` — derrota no TCC
- `EstadoVitoria` — vitória no TCC com estatísticas
- `EstadoSair` (interno em `JogoContexto`) — encerra o loop principal

---

## UI Console

### `IConsoleIO` (interface) — `src/UI/IConsoleIO.cs`
```csharp
public interface IConsoleIO
{
    void WriteLine(string mensagem);
    void Write(string mensagem);
    void WriteLineCor(string mensagem, ConsoleColor cor);
    string ReadLine();
    ConsoleKeyInfo ReadKey();
    void Clear();
    void SetCursorPosition(int left, int top);
    void ExibirTitulo(string titulo);
    void ExibirSeparador();
    void ExibirMensagemSucesso(string mensagem);
    void ExibirMensagemErro(string mensagem);
    void ExibirMensagemAlerta(string mensagem);
    void Pausar(string mensagem = "Pressione qualquer tecla para continuar...");
    int ExibirMenu(string[] opcoes);
}
```

### `ConsoleIO` — `src/UI/ConsoleIO.cs`
```csharp
public class ConsoleIO : IConsoleIO
{
    public void WriteLine(string mensagem);
    public void Write(string mensagem);
    public void WriteLineCor(string mensagem, ConsoleColor cor);
    public string ReadLine();
    public ConsoleKeyInfo ReadKey();
    public void Clear();
    public void SetCursorPosition(int left, int top);
    public void ExibirTitulo(string titulo);
    public void ExibirSeparador();
    public void ExibirMensagemSucesso(string mensagem);
    public void ExibirMensagemErro(string mensagem);
    public void ExibirMensagemAlerta(string mensagem);
    public void Pausar(string mensagem = "Pressione qualquer tecla para continuar...");
    public int ExibirMenu(string[] opcoes);  // retorna índice escolhido (0-based)
}
```

---

## Eventos (padrão Observer)

### `IEventoJogo` (interface marcadora) — `src/Eventos/IEventoJogo.cs`
```csharp
public interface IEventoJogo
{
    // Interface marcadora para eventos do sistema
}
```

### `IObservadorJogo<T>` (interface genérica) — `src/Eventos/IObservadorJogo.cs`
```csharp
public interface IObservadorJogo<in T> where T : IEventoJogo
{
    void OnEvento(T evento);
}
```

### `EventoBus` — `src/Eventos/EventoBus.cs`
```csharp
public static class EventoBus
{
    public static void Inscrever<T>(Action<T> handler) where T : IEventoJogo;
    public static void Desinscrever<T>(Action<T> handler) where T : IEventoJogo;
    public static void Publicar<T>(T evento) where T : IEventoJogo;
    public static void LimparTodos();
}
```

### `ItemAdquiridoEvento` — `src/Eventos/ItemAdquiridoEvento.cs`
```csharp
public class ItemAdquiridoEvento : IEventoJogo
{
    public Item Item { get; }
    public string NomeAluno { get; }
    
    public ItemAdquiridoEvento(Item item, string nomeAluno);
}
```

### `HabilidadeDesbloqueadaEvento` — `src/Eventos/HabilidadeDesbloqueadaEvento.cs`
```csharp
public class HabilidadeDesbloqueadaEvento : IEventoJogo
{
    public Habilidade Habilidade { get; }
    public string NomeAluno { get; }
    
    public HabilidadeDesbloqueadaEvento(Habilidade habilidade, string nomeAluno);
}
```

---

## Persistência (Extensões SQLite)

### `IRepositorioQuestoes` (interface) — `src/Persistencia/IRepositorioQuestoes.cs`
```csharp
public interface IRepositorioQuestoes
{
    List<Pergunta> BuscarPorSemestreEMateria(int semestre, string materia, string tipo);
    void Inserir(QuestaoDto questao);
    void Atualizar(QuestaoDto questao);
    void Deletar(int id);
    List<QuestaoDto> ListarTodas();
}
```

### `SqliteRepositorioSave` — `src/Persistencia/SqliteRepositorioSave.cs`
```csharp
public class SqliteRepositorioSave : IRepositorio
{
    public SqliteRepositorioSave(string caminhoDb = "database/campusquest.db");
    
    public void Salvar(EstadoJogo estado);
    public EstadoJogo Carregar();
    public bool ExisteArquivo();
    public void Deletar();
}
```

### `SqliteRepositorioQuestoes` — `src/Persistencia/SqliteRepositorioQuestoes.cs`
```csharp
public class SqliteRepositorioQuestoes : IRepositorioQuestoes
{
    public SqliteRepositorioQuestoes(string caminhoDb = "database/campusquest.db");
    
    public List<Pergunta> BuscarPorSemestreEMateria(int semestre, string materia, string tipo);
    public void Inserir(QuestaoDto questao);
    public void Atualizar(QuestaoDto questao);
    public void Deletar(int id);
    public List<QuestaoDto> ListarTodas();
}
```

### `DatabaseInitializer` — `src/Persistencia/DatabaseInitializer.cs`
```csharp
public static class DatabaseInitializer
{
    public static void Inicializar();                          // cria schema + seed
    private static void EnsureDatabaseDirectory();             // garante diretório database/
    private static void CriarSchemaSeNaoExistir();             // cria tabelas Questoes, SaveJogo
    private static void SeedQuestoesSeVazio();                 // insere 30+ questões padrão
}
```

### `EstadoJogoFactory` — `src/Persistencia/EstadoJogoFactory.cs`
```csharp
public static class EstadoJogoFactory
{
    public static EstadoJogo CriarDe(Aluno aluno);             // Aluno → EstadoJogo
    public static Aluno RestaurarAluno(EstadoJogo estado);     // EstadoJogo → Aluno
}
```

### `QuestaoDto` — `src/Persistencia/QuestaoDto.cs`
```csharp
public class QuestaoDto
{
    public int Id { get; set; }
    public string Enunciado { get; set; }
    public string[] Alternativas { get; set; }       // sempre 4
    public int IndiceCorreto { get; set; }           // 0-3
    public string Explicacao { get; set; }
    public int Semestre { get; set; }                // 1-4
    public string Materia { get; set; }              // IC, AED, POO, TCC
    public string Tipo { get; set; }                 // Quiz ou Exame
    public string Dificuldade { get; set; }          // Normal, Media, Dificil
    public bool Ativa { get; set; }                  // flag para desativar sem deletar
}
```

---

## Core (Extensões)

### `HabilidadeCatalogo` — `src/Core/HabilidadeCatalogo.cs`
```csharp
public static class HabilidadeCatalogo
{
    public static Habilidade FocoIC { get; }           // aumenta dano contra IC
    public static Habilidade FocoAED { get; }          // aumenta dano contra AED
    public static Habilidade FocoPOO { get; }          // aumenta dano contra POO
    
    public static Habilidade ObterPorNome(string nome);
}
```
