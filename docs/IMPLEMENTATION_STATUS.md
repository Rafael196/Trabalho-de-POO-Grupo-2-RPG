# IMPLEMENTATION STATUS — Campus Quest

Resumo do que está implementado no backend e o que falta, mapeado contra os Requisitos Funcionais (RF).

## Requisitos Funcionais (RF)

- **RF01 — Menu Principal**: ✅ Implementado — fluxo principal via State (`EstadoMenu`).
- **RF02 — Navegação entre Locais**: ✅ Implementado — Hall e opcoes de navegacao no `EstadoExplorando`.
- **RF03 — Quiz do Professor**: ✅ Implementado — `SistemaQuiz` com escolha de semestre e ganho de Conhecimento.
- **RF04 — Drop de Item por Quiz**: ✅ Implementado — item por faixa de aproveitamento.
- **RF05 — Exame de Aproveitamento**: ✅ Implementado — fluxo de perguntas, dano e resultado com narracao.
- **RF06 — Bônus de Risco**: ✅ Implementado — bonus de coragem quando conhecimento inicial < 40.
- **RF07 — Média Final e TCC**: ✅ Implementado — `TCC` escala com media final via `CalculadoraMedia`.
- **RF08 — Habilidades**: ✅ Implementado — `Habilidade` com catálogo (`HabilidadeCatalogo`) aplicada no exame; desbloqueio por vitória.
- **RF09 — Inventário**: ✅ Implementado — inventario completo com capacidade máxima, validações e itens funcionais.
- **RF10 — Save e Load**: ✅ Implementado — Dual: `RepositorioJson` (saves/slot1.json) + `SqliteRepositorioSave` (database/).
- **RF11 — Boss Final: fluxo completo**: ✅ Implementado — `TCC` acessivel apos 3 semestres via estados.

## Funcionalidades Extras Implementadas

### Sistema de Persistência Expandido
- ✅ **Persistência JSON**: `RepositorioJson` salva em `backend/saves/slot1.json` (conforme documentado)
- ✅ **Persistência SQLite**: `SqliteRepositorioSave` salva em `backend/database/campusquest.db` (não documentado)
- ✅ **Banco de Questões**: `SqliteRepositorioQuestoes` gerencia perguntas em SQLite (não documentado)
- ✅ **Factory de Conversão**: `EstadoJogoFactory` converte Aluno ↔ EstadoJogo (não documentado)
- ✅ **Inicialização de BD**: `DatabaseInitializer` cria schema e seed com 30+ questões (não documentado)

### Sistema de Eventos (Padrão Observer)
- ✅ **EventoBus**: Implementação pub/sub genérica para eventos do jogo (não documentado)
- ✅ **ItemAdquiridoEvento**: Disparado ao adicionar item ao inventário (não documentado)
- ✅ **HabilidadeDesbloqueadaEvento**: Disparado ao desbloquear habilidade (não documentado)
- ✅ **Interfaces**: `IEventoJogo`, `IObservadorJogo<T>` (não documentado)

### Melhorias em Classes Core
- ✅ **HabilidadeCatalogo**: Catálogo centralizado com habilidades (Foco IC, AED, POO) (não documentado)
- ✅ **Estatísticas do Aluno**: `SemestresRepetidos`, `QuizzesConcluidos`, `ItensUsados` (não documentado)
- ✅ **Métodos Extras em Aluno**: `RegistrarQuizConcluido()`, `RegistrarItemUsado()`, `RegistrarSemestreRepetido()` (não documentado)

### Banco de Questões
- ✅ **30+ Questões**: 15 de quiz + 15 de exame, distribuídas por semestre e matéria
- ✅ **Diferenciação**: Questões de quiz (mais simples) vs exame (mais difíceis)
- ✅ **Metadados**: Tipo (Quiz/Exame), Dificuldade (Normal/Media/Dificil), Ativa (flag)

## Arquitetura de Estados

- ✅ **EstadoMenu**: Menu principal (Novo Jogo, Carregar, Sair)
- ✅ **EstadoExplorando**: Navegação pelo Hall com NPCs (Veterano, Professor, Coordenador) e acesso ao Exame
- ✅ **EstadoExame**: Execução do exame com perguntas, dano, itens e habilidades
- ✅ **EstadoVitoria**: Tela de conclusão com estatísticas após vencer TCC
- ✅ **EstadoGameOver**: Tela de derrota com opção de recomeço
- ✅ **EstadoSair**: Estado interno para encerrar loop principal

**Nota**: Cenários (Hall, SalaProfessor, SalaExame, SalaCoordenacao) foram integrados em `EstadoExplorando` ao invés de classes separadas.

## UI Console

- ✅ **IConsoleIO**: Interface para entrada/saída (abstração para facilitar testes)
- ✅ **ConsoleIO**: Implementação concreta com formatação, cores e validações

**Nota**: A documentação menciona `MenuConsole.cs`, mas a implementação usa `ConsoleIO` + `IConsoleIO` (mais modular).

## Pontos de Atenção

- ⚠️ **Persistência Dual**: Projeto suporta JSON e SQLite; configurar qual usar em `Program.cs`
- ⚠️ **Duplicação**: Existe `Item.cs` em `Core/` (vazio) e `Itens/` (classe real) - considerar remover duplicata
- ⚠️ **Banco de Dados**: Pasta `database/` criada em runtime; adicionar ao `.gitignore`

## Status Geral

**Implementação**: ✅ 100% dos requisitos funcionais  
**Funcionalidades Extras**: ✅ SQLite, EventoBus, Catálogos, Estatísticas  
**Padrões de Projeto**: ✅ Strategy, State, Repository, Observer, Factory  
**Pronto para Entrega**: ✅ Sim

---

*Última atualização: 15/06/2026*

