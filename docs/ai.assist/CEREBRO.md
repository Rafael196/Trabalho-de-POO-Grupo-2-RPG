# CEREBRO — Campus Quest
> Guia mestre de desenvolvimento. Consulte antes de implementar qualquer coisa.
> Mantenha este arquivo atualizado conforme o projeto evolui.

---

## Estado atual do projeto

```
FASE ATIVA : 1 — Console
VERSAO PRD : 1.1
ULTIMA ATUALIZACAO : Maio/2026
```

### Checklist de progresso (Fase 1)

#### Fundação
- [ ] `Personagem.cs` — classe abstrata base
- [ ] `Aluno.cs` — personagem jogável com aproveitamentos e habilidades
- [ ] `Habilidade.cs` + enum `TipoEfeito`
- [ ] `Inventario.cs` — lista com capacidade máxima

#### Matérias (Chefes)
- [ ] `Materia.cs` — abstrata com contrato de exame
- [ ] `IC.cs` — ataque Loop Infinito
- [ ] `AED.cs` — ataque Stack Overflow
- [ ] `POO.cs` — ataque NullPointerException
- [ ] `TCC.cs` — síntese total escalada pela média

#### NPCs
- [ ] `NPC.cs` — abstrata
- [ ] `Professor.cs` — sugestão e aplicação de quiz
- [ ] `Veterano.cs` — dicas e itens (no Hall)
- [ ] `Coordenador.cs` — trancamento de semestre

#### Itens
- [ ] `Item.cs` — abstrata
- [ ] `Cafe.cs`
- [ ] `Caderno.cs`
- [ ] `LivroTecnico.cs`

#### Quiz
- [ ] `Pergunta.cs`
- [ ] `SistemaQuiz.cs` + `ResultadoQuiz`
- [ ] Banco de perguntas por semestre (mín. 5 por semestre)

#### Exame
- [ ] `ContextoExame.cs`
- [ ] `SistemaExame.cs` + `ResultadoExame`
- [ ] `CalculadoraMedia.cs`

#### Persistência
- [ ] `EstadoJogo.cs` + `DadosAluno` + `Estatisticas`
- [ ] `IRepositorio.cs`
- [ ] `RepositorioJson.cs`

#### Estados (padrão State)
- [x] `IEstadoJogo.cs`
- [x] `JogoContexto.cs`
- [x] `EstadoMenu.cs`
- [x] `EstadoExplorando.cs`
- [x] `EstadoExame.cs`
- [x] `EstadoGameOver.cs`
- [x] `EstadoVitoria.cs`

#### Cenários
- [ ] `Hall.cs`
- [ ] `SalaProfessor.cs`
- [ ] `SalaExame.cs`
- [ ] `SalaCoordenacao.cs`

#### UI Console
- [ ] `MenuConsole.cs`

#### Integração e Testes
- [ ] Fluxo completo: novo jogo → semestre 1 → semestre 2 → semestre 3 → TCC
- [ ] Save e Load funcionando de ponta a ponta
- [ ] Todos os CA Alta concluídos

---

## Ordem recomendada de implementação

A ordem abaixo minimiza dependências e permite testar cada camada antes de avançar:

```
1. Pergunta → SistemaQuiz (sem Aluno ainda, teste isolado)
2. Personagem → Aluno → Habilidade → Inventario
3. Item → Cafe → Caderno → LivroTecnico
4. Materia → IC → AED → POO → TCC
5. ContextoExame → SistemaExame → CalculadoraMedia
6. NPC → Professor (integra SistemaQuiz) → Veterano → Coordenador
7. IRepositorio → EstadoJogo → RepositorioJson
8. IEstadoJogo → JogoContexto → todos os Estados
9. Hall → SalaProfessor → SalaExame → SalaCoordenacao
10. MenuConsole → Program.cs (integração final)
```

---

## Como usar IA no desenvolvimento

### Template de prompt padrão

```
[CONTEXTO — cole o conteúdo de CONTEXT_COMPACT.md aqui]

---

Tarefa: implemente a classe `NomeDaClasse` conforme o contrato abaixo.

Contrato (de CLASS_CONTRACTS.md):
[cole apenas o bloco da classe em questão]

Requisitos relacionados: RF0X, RNF0X
Critérios de aceite: CA0X, CA0X

Restrições:
- Siga PascalCase para métodos e propriedades, camelCase para variáveis locais
- Trate exceções onde indicado
- Não crie dependências fora do contrato definido
- [adicione restrições específicas da tarefa]
```

### Dicas de uso eficiente

**Para implementar uma classe:**
Cole CONTEXT_COMPACT + o bloco da classe em CLASS_CONTRACTS. Não cole o PRD inteiro.

**Para corrigir um bug:**
Cole CONTEXT_COMPACT + o código com o bug + o erro exato. Aponte o RF ou CA que está falhando.

**Para revisar código:**
Cole CONTEXT_COMPACT + o código + peça revisão contra RNF01–RNF04 e os padrões de projeto.

**Para criar perguntas de quiz/exame:**
Indique o semestre, a matéria e quantas perguntas. As do quiz são mais simples (memorização), as do exame exigem aplicação de conceito.

**Para integrar duas classes:**
Cole CONTEXT_COMPACT + os dois blocos de CLASS_CONTRACTS + descreva a integração esperada.

---

## Decisões de design tomadas (não rever sem consenso do grupo)

| # | Decisão | Motivo |
|---|---|---|
| D01 | Conhecimento é multiplicador, não dano direto | Permite vencer com preparo baixo (mecânica de risco) |
| D02 | Veterano fica no Hall, sem sala própria | Simplifica navegação; NPC de suporte deve ser sempre acessível |
| D03 | Quiz do Professor é sugerido, não automático | Preserva agência do jogador; aluno decide quando e qual semestre |
| D04 | Quiz de revisão vale 50% do ganho base | Penaliza levemente o conteúdo fora do momento ideal, sem bloquear |
| D05 | Perguntas do exame são exclusivas e mais difíceis | Distingue claramente estudo (quiz) de avaliação (exame) |
| D06 | Aproveitamento máximo com bônus de risco é ~65 | Garante que despreparo tenha consequência real na média final |
| D07 | TCC escala pela média dos 3 semestres | Torna toda a jornada relevante, não apenas o último semestre |
| D08 | Persistência via IRepositorio (interface) | Permite trocar JSON por banco de dados sem mudar o domínio |
| D09 | Padrão State para fluxo do jogo | Isola responsabilidade de cada tela/estado, facilita testes |
| D10 | Um curso apenas (TI) por ora | Foco no prazo; arquitetura permite adicionar cursos depois via herança |

---

## Regras de contribuição no GitHub

```
Branches:
  main          → código estável, só recebe merge via PR
  dev           → branch de integração
  feat/nome     → nova funcionalidade (ex: feat/sistema-quiz)
  fix/nome      → correção de bug (ex: fix/save-inventario)
  docs/nome     → documentação (ex: docs/diagrama-classes)

Commits (mensagem em português, imperativo):
  [feat] Implementa SistemaQuiz com drop de item
  [fix]  Corrige cálculo de aproveitamento no exame
  [docs] Atualiza CLASS_CONTRACTS com ContextoExame
  [refactor] Extrai CalculadoraMedia de SistemaExame

Pull Request:
  - Descrever o que foi feito e qual RF/CA cobre
  - Requer aprovação de pelo menos 1 integrante
  - Não fazer merge em main sem testes manuais do fluxo completo
```

---

## Banco de perguntas — orientações

### Quiz do Professor (mais simples — memorização/reconhecimento)

**Semestre 1 — IC (Introdução à Computação)**
Temas sugeridos: conceitos de hardware/software, sistemas operacionais, representação binária, história da computação, redes básicas.

**Semestre 2 — AED (Algoritmos e Estrutura de Dados)**
Temas sugeridos: conceito de algoritmo, complexidade básica (O(n)), arrays, listas, pilhas, filas, busca linear e binária, ordenação básica.

**Semestre 3 — POO**
Temas sugeridos: os 4 pilares, diferença classe/objeto, construtores, herança vs composição, interfaces, encapsulamento com get/set.

### Exame de Aproveitamento (mais difícil — aplicação de conceito)

**IC — exame:** identificar representação binária de valores, distinguir tipos de memória, analisar topologias de rede, interpretar diagrama de sistema operacional.

**AED — exame:** analisar complexidade de trecho de código, traçar execução de algoritmo de busca, identificar estrutura ideal para um problema descrito, ordenar um array passo a passo.

**POO — exame:** identificar pilar aplicado em trecho de código C#, detectar erro de encapsulamento, completar hierarquia de herança, escolher padrão de projeto para cenário descrito.

**TCC — exame:** combina os três tipos acima com cenários que integram conceitos das 3 matérias.

---

## Constantes do jogo (valores-padrão, ajustáveis)

```csharp
// Aluno
VidaMaximaInicial       = 100
ConhecimentoInicial     = 0

// Inventário
CapacidadeMaxima        = 6

// Exame
DanoBaseChefe           = 20   // dano base do chefe por resposta errada
DanoBaseAluno           = 15   // dano base do aluno por resposta certa
MinimoPerguntas         = 8

// Quiz
GanhoBaseConhecimento   = 10   // por acerto no semestre atual
MultRevisao             = 0.5  // multiplicador para revisão de semestre anterior
MinimoPerguntas         = 5

// Bônus de risco
LimiteConhecimentoBaixo = 40
BonusCoragem            = 10
AproveitamentoMaxRisco  = 65

// Items
CafeRestauracao         = 25
CadernoBonus            = 15   // temporário
LivroTecnicoBonus       = 20   // permanente vs matéria específica

// Faixas de drop de item
FaixaLivro              = 80   // >= 80% → LivroTecnico
FaixaCaderno            = 50   // >= 50% → Caderno
                               // < 50%  → Cafe

// Faixas de média para o TCC
FaixaMediaAlta          = 80
FaixaMediaBaixa         = 50
```

---

## Armadilhas conhecidas e como evitar

| Armadilha | Como evitar |
|---|---|
| Lógica de jogo dentro de classes de UI | Nunca colocar regras de negócio em `MenuConsole` — apenas exibição e leitura |
| Instanciar `Materia` diretamente em `SistemaExame` | Receber `Materia` por parâmetro (injeção) — facilita testes |
| Serializar objetos complexos diretamente | Usar `EstadoJogo` (DTO puro) para save; nunca serializar `Aluno` diretamente |
| Perguntas hardcoded dentro das classes de Materia | Criar método `GetPerguntasExame()` que carrega de lista separada ou arquivo |
| Quebrar fluxo com `Console.ReadLine()` sem validação | Usar sempre `MenuConsole.LerEntrada()` ou `MenuConsole.ExibirOpcoes()` |
| Misturar lógica de exame e quiz | `SistemaExame` e `SistemaQuiz` são classes separadas com propósitos distintos |

---

## Documentação obrigatória para entrega (26/06/2026)

- [ ] README.md com: pré-requisitos, como compilar, como executar, como jogar
- [ ] Documentação PDF com:
  - [ ] Descrição detalhada do projeto
  - [ ] Arquitetura do sistema (estrutura de pastas + responsabilidades)
  - [ ] Diagrama de classes (pode exportar de CLASS_CONTRACTS.md)
  - [ ] Diagrama de fluxo do jogo
  - [ ] Padrões de projeto utilizados com justificativa (Strategy, State, Repository, Observer)
- [ ] Código hospedado no GitHub com histórico de commits organizado

---

*CEREBRO v1.0 — Campus Quest | Atualizar a cada decisão de design relevante*
