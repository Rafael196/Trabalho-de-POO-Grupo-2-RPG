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

### `MenuConsole` — `src/UI/Console/MenuConsole.cs`
```csharp
public static class MenuConsole
{
    public static int ExibirOpcoes(string titulo, string[] opcoes);
    // Exibe menu numerado, valida input, retorna índice escolhido (0-based)

    public static void ExibirStatus(Aluno aluno);
    // Exibe vida, conhecimento, semestre atual, aproveitamentos e habilidades

    public static void ExibirMensagem(string mensagem, TipoMensagem tipo);
    // TipoMensagem: Info, Sucesso, Erro, Alerta

    public static string LerEntrada(string prompt);
    // Lê string com validação de nulo/vazio

    public static void Limpar();
    public static void Pausar(string mensagem = "Pressione qualquer tecla para continuar...");
}

public enum TipoMensagem { Info, Sucesso, Erro, Alerta }
```
