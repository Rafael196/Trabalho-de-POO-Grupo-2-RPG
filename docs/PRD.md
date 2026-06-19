# PRD — Campus Quest: A Jornada Universitária
> **Product Requirements Document** | Versão 1.1 | Maio/2026  
> Trabalho Prático — Programação Orientada por Objetos | PUC Minas Betim

---

## 1. Visão do Produto

**Campus Quest** é um jogo de RPG de texto desenvolvido em **C#**, ambientado na vida universitária do curso de Tecnologia da Informação. O jogador controla um estudante que enfrenta as disciplinas do curso como chefes em **Exames de Aproveitamento** ao longo de 3 semestres, acumulando Conhecimento e habilidades para vencer o desafio final: o TCC.

O projeto tem duplo propósito:
- **Acadêmico:** demonstrar domínio dos 4 pilares da POO com boas práticas em C#.
- **Produto:** entregar um jogo funcional com progressão de personagem coesa e mecânica de risco vs recompensa clara.

---

## 2. Escopo

### Dentro do escopo — v1 (entrega final)
- Jornada individual de um jogador
- 3 semestres com um chefe por semestre + boss final (TCC)
- Exame de Aproveitamento com perguntas exclusivas e mais difíceis por matéria
- Sistema de Conhecimento como multiplicador de desempenho no exame
- Métrica de aproveitamento por semestre (0–100) e média final para o TCC
- Dinâmica de risco: possível vencer com Conhecimento baixo, com bônus de coragem mas aproveitamento menor
- Quiz com o Professor: player escolhe qual semestre revisar; quizzes de revisão rendem menos Conhecimento
- Drop de item do quiz baseado no aproveitamento obtido
- 3 NPCs com interações distintas (Veterano fixo no Hall)
- Sistema de itens com inventário limitado
- Save e Load em arquivo JSON
- Menu interativo em console (fase 1)
- Interface gráfica (fase 2)
- Migração para banco de dados (fase 3)

### Fora do escopo — v1
- Multiplayer ou cooperativo
- Múltiplos cursos (apenas TI por ora)
- Eventos aleatórios por semestre
- Geração procedural de cenários

---

## 3. Estrutura da Jornada

```
[INÍCIO — Menu Principal]
    |
   Hall
    |
   ├── Veterano (NPC fixo no Hall) ──> Dicas + Itens consumíveis
   ├── Sala do Professor           ──> Diálogo → Sugere Quiz → Aluno escolhe semestre
   │                                   Quiz → Aproveitamento → Drop de Item
   ├── Sala da Coordenação         ──> Tranca semestre (salva sem batalhar)
   └── Sala de Exame               ──> Exame de Aproveitamento (batalha por turnos de perguntas)
           |
    Resposta certa  → Dano ao chefe (× multiplicador de Conhecimento)
    Resposta errada → Dano ao aluno (÷ atenuador de Conhecimento)
    Usar item       → Efeito imediato (vida ou Conhecimento temporário)
           |
    Vitória → Habilidade desbloqueada + Aproveitamento do semestre registrado
    Derrota → Repete o semestre atual
           |
   [Após 3 semestres]
           |
   Boss Final: TCC
   Média dos 3 aproveitamentos (0–100) define poder do aluno na batalha
   Vitória → Tela de conclusão + estatísticas
   Derrota → Game Over → opção de recomeço
```

---

## 4. Sistema de Conhecimento

O **Conhecimento** é o atributo central do aluno. Ele **não causa dano diretamente** — funciona como **multiplicador de desempenho** nos Exames de Aproveitamento e como base para o cálculo da média final que escalona o poder do aluno contra o TCC.

### 4.1 Fontes de Conhecimento

| Fonte | Ganho | Observação |
|---|---|---|
| Quiz do semestre atual (Professor) | Alto | Máximo rendimento — matéria em andamento |
| Quiz de semestre anterior (revisão) | Reduzido | Conteúdo já visto — menor ganho |
| Item: Caderno de Anotações | Médio (temporário) | Bônus apenas no próximo exame |
| Item: Livro Técnico | Médio (permanente) | Bônus fixo contra matéria específica |
| Item: Café | Nenhum | Restaura Vida, não Conhecimento |

### 4.2 Como o Conhecimento age no Exame

```
DanoAoChefe  = DanoBase × (1 + Conhecimento / 100)
DanoAoAluno  = DanoBase × (1 - Conhecimento / 100)  // mínimo de 1
```

Quanto maior o Conhecimento:
- Maior o dano causado por resposta certa
- Menor o dano sofrido por resposta errada
- Maior a margem de erro antes de ser derrotado

### 4.3 Bônus de Risco (Conhecimento Baixo + Vitória)

Aluno com Conhecimento baixo que vence o exame recebe um **bônus de coragem** aplicado sobre o aproveitamento registrado. Porém, o aproveitamento final ainda será inferior ao de um aluno bem preparado — impactando negativamente a média para o TCC.

```
Se Conhecimento < 40 e Aluno vence:
  aproveitamentoSemestre = aproveitamentoCalculado + bonusCoragem
  // Mesmo com bônus, aproveitamento máximo fica abaixo de 70
```

---

## 5. Métrica de Aproveitamento

### 5.1 Aproveitamento por Semestre

Registrado ao final de cada Exame de Aproveitamento vencido (escala 0–100):

```
aproveitamento = (acertosExame / totalPerguntas × 100) × fatorConhecimento
fatorConhecimento = 0.5 + (Conhecimento / 200)   // varia de 0.5 a 1.0
```

| Situação | Faixa de Aproveitamento |
|---|---|
| Conhecimento alto + muitos acertos | 80 – 100 |
| Conhecimento alto + poucos acertos | 50 – 79 |
| Conhecimento baixo + muitos acertos + bônus coragem | 40 – 65 |
| Conhecimento baixo + poucos acertos | 0 – 39 |

### 5.2 Média Final para o TCC

```
mediaFinal = (aproveitamento1 + aproveitamento2 + aproveitamento3) / 3
```

A `mediaFinal` escala os atributos do aluno na batalha contra o TCC:
- **mediaFinal >= 80:** aluno começa com vantagem (bônus de Conhecimento extra)
- **mediaFinal 50–79:** batalha equilibrada
- **mediaFinal < 50:** TCC começa com vantagem (ataque inicial mais forte)

---

## 6. Exame de Aproveitamento (Batalha)

### 6.1 Características das Perguntas do Exame

- **Exclusivas:** banco de perguntas separado do quiz do Professor
- **Mais difíceis:** exigem aplicação de conceito, não só memorização
- **Correlacionadas:** temática alinhada à matéria do semestre
- Mínimo de **8 perguntas** por exame, com 4 alternativas cada

### 6.2 Fluxo do Exame por Turno

```
1. Exibe pergunta atual e 4 alternativas
2. Jogador escolhe: [A] [B] [C] [D] ou [U] Usar Item
3a. Resposta correta → DanoAoChefe calculado → exibe feedback positivo
3b. Resposta errada  → DanoAoAluno calculado → exibe feedback + resposta correta
4. Verifica condição de vitória/derrota
5. Se chefe ainda vivo: próxima pergunta
6. Fim do banco de perguntas sem vitória → derrota
```

### 6.3 Ataques Temáticos dos Chefes

Além do dano por resposta errada, cada chefe possui um ataque especial que ocorre em intervalos definidos:

| Chefe | Ataque Especial | Efeito |
|---|---|---|
| IC | Loop Infinito | Dano contínuo por 2 perguntas consecutivas |
| AED | Stack Overflow | Dano único muito alto em uma pergunta |
| POO | NullPointerException | Anula o multiplicador de Conhecimento por 1 turno |
| TCC | Síntese Total | Combina os 3 ataques anteriores, escalados pela média do aluno |

---

## 7. Sistema de Quiz (Professor)

### 7.1 Fluxo da Interação

```
Aluno entra na Sala do Professor
  → Professor exibe diálogo / dica temática
  → Professor sugere quiz: "Quer reforçar seus conhecimentos?"
  → Aluno escolhe: [S] Sim / [N] Não
    → Se Sim: lista de semestres disponíveis
      → Semestre atual sempre disponível
      → Semestres anteriores disponíveis como revisão
    → Aluno escolhe o semestre
    → Quiz iniciado (mín. 5 perguntas, 4 alternativas)
    → Fim do quiz: cálculo de aproveitamento
    → Drop de item conforme faixa de aproveitamento
    → Ganho de Conhecimento conforme semestre escolhido
```

### 7.2 Ganho de Conhecimento por Tipo de Quiz

```csharp
if (semestreQuiz == semestreAtual)
    ganho = GanhoBase;           // Ex: +10 por acerto
else
    ganho = GanhoBase * 0.5;     // Ex: +5 por acerto (revisão)
```

### 7.3 Drop de Item por Aproveitamento do Quiz

| Aproveitamento no Quiz | Item Dropado | Efeito |
|---|---|---|
| 80% – 100% | Livro Técnico | Bônus permanente de Conhecimento contra matéria específica |
| 50% – 79% | Caderno de Anotações | Bônus temporário de Conhecimento no próximo exame |
| 0% – 49% | Café | Restaura Vida parcialmente |

---

## 8. Locais e NPCs

### 8.1 Mapa de Locais

| Local | Acesso | Conteúdo |
|---|---|---|
| Hall | Sempre disponível | Veterano (NPC fixo) + menu de navegação |
| Sala do Professor | Via Hall | Diálogo + sugestão de quiz |
| Sala de Exame | Via Hall | Exame de Aproveitamento com a matéria do semestre |
| Sala da Coordenação | Via Hall | Trancamento de semestre (save sem batalha) |

### 8.2 NPCs

#### Professor
- **Local:** Sala do Professor
- **Métodos:** `Conversar()`, `SugerirQuiz()`, `AplicarQuiz(semestreEscolhido)`
- Não inicia quiz automaticamente — apenas sugere; o aluno decide se quer e qual semestre

#### Veterano
- **Local:** Hall (NPC fixo, sempre presente)
- **Métodos:** `DarDica()`, `OfereceItem()`
- Dicas são temáticas ao chefe do semestre atual

#### Coordenador
- **Local:** Sala da Coordenação
- **Métodos:** `TrancarSemestre()`
- Salva o progresso atual sem exigir batalha

---

## 9. Sistema de Itens e Inventário

### 9.1 Itens Disponíveis

| Item | Efeito | Origem | Limite |
|---|---|---|---|
| Café | Restaura Vida parcialmente | Veterano / Quiz 0–49% | 3 por semestre |
| Caderno de Anotações | Bônus temporário de Conhecimento (próximo exame) | Quiz 50–79% | 2 por semestre |
| Livro Técnico | Bônus permanente de Conhecimento vs. matéria específica | Quiz 80–100% | 1 por chefe |
| Cola (item secreto) | Elimina duas alternativas no exame — uso único | Evento especial | 1 por jogo |

### 9.2 Regras do Inventário

- Capacidade máxima definida por constante (ex: `CapacidadeMaxima = 6`)
- Ao tentar adicionar item com inventário cheio: exibir aviso e não adicionar
- Itens consumíveis removidos do inventário após uso
- Estado do inventário persistido no arquivo de save

---

## 10. Entidades e Arquitetura de Classes

### 10.1 Mapeamento com os Pilares da POO

| Pilar | Onde se aplica | Implementação |
|---|---|---|
| **Abstração** | Classes base do sistema | `Personagem`, `Materia`, `NPC`, `Item` são abstratas |
| **Encapsulamento** | Atributos de todas as classes | `vida`, `conhecimento`, `aproveitamentos` privados com getters/setters |
| **Herança** | Subclasses das entidades base | `Aluno : Personagem`; `IC, AED, POO, TCC : Materia`; `Professor, Veterano, Coordenador : NPC` |
| **Polimorfismo** | Comportamento dos chefes e NPCs | Cada `Materia` implementa `Atacar()` e `GetPerguntasExame()` de forma única; cada `NPC` implementa `Interagir()` próprio |

### 10.2 Diagrama de Classes (simplificado)

```
+----------------------+
|  Personagem (abs)    |
|----------------------|
| #vida: int           |
| #vidaMaxima: int     |
| #conhecimento: int   |
|----------------------|
| +Atacar()*           |
| +ReceberDano()       |
| +EstaVivo()          |
+----------+-----------+
           | herda
      +----+----+
      |  Aluno  |
      |---------|
      | -inventario: Inventario
      | -habilidades: List<Habilidade>
      | -semestreAtual: int
      | -aproveitamentos: int[3]
      |---------|
      | +UsarItem()
      | +AdicionarHabilidade()
      | +RegistrarAproveitamento()
      | +GetMediaFinal(): float
      +---------+

+----------------------+
|   Materia (abs)      |
|----------------------|
| #nome: string        |
| #vidaAtual: int      |
| #vidaMaxima: int     |
|----------------------|
| +Atacar(aluno)*      |
| +AtaqueEspecial()*   |
| +ReceberDano()       |
| +EstaVencido()       |
| +GetPerguntasExame()*|
+----------+-----------+
           | herda
   +-------+--------+-------+
   |       |        |       |
  IC      AED      POO     TCC

+----------------------+
|    NPC (abs)         |
|----------------------|
| #nome: string        |
|----------------------|
| +Interagir()*        |
| +GetDialogo()        |
+----------+-----------+
           | herda
   +-------+-------+----------+
   |               |           |
Professor       Veterano   Coordenador
+SugerirQuiz()  +DarDica()  +TrancarSemestre()
+AplicarQuiz()  +OfereceItem()

+--------------------+     +--------------------+
|    Inventario      |     |     Habilidade     |
|--------------------|     |--------------------|
| -itens: List<Item> |     | -nome: string      |
| -capacidadeMax:int |     | -descricao: string |
|--------------------|     | -efeito: TipoEfeito|
| +Adicionar()       |     |--------------------|
| +Remover()         |     | +Aplicar(aluno)    |
| +EstaCheia()       |     +--------------------+
+--------------------+

+--------------------+     +--------------------+
|   Item (abs)       |     |   SistemaQuiz      |
|--------------------|     |--------------------|
| -nome: string      |     | -perguntas: List   |
| -descricao: string |     |--------------------|
|--------------------|     | +IniciarQuiz()     |
| +Usar(aluno)*      |     | +AvaliarResposta() |
+--------+-----------+     | +CalcAproveitamento|
         | herda            | +DefinirDropItem() |
  +------+------+          +--------------------+
 Cafe  Caderno  LivroTecnico
                            +--------------------+
+--------------------+      |  SistemaExame      |
|  CalculadoraMedia  |      |--------------------|
|--------------------|      | -perguntas: List   |
| +Calcular(int[3])  |      | -turnoAtual: int   |
| +AplicarBonus()    |      |--------------------|
+--------------------+      | +IniciarExame()    |
                            | +ProcessarResposta()|
+--------------------+      | +CalcularDano()    |
|  GerenciadorSave   |      | +RegistrarAprov()  |
|--------------------|      +--------------------+
| +Salvar(estado)    |
| +Carregar()        |
| +ExisteArquivo()   |
+--------------------+
```

---

## 11. Requisitos Funcionais

### RF01 — Menu Principal
- Exibir opções: **Novo Jogo**, **Carregar Jogo**, **Sair**
- Novo Jogo: inicializa Aluno com atributos padrão e cria arquivo de save
- Carregar Jogo: valida existência do arquivo antes de carregar

### RF02 — Navegação entre Locais
- Hall exibe menu de navegação com opções disponíveis
- Veterano sempre acessível no Hall sem precisar acessar sala separada
- Sala de Exame só disponível se o semestre não estiver trancado

### RF03 — Quiz do Professor
- Professor não inicia quiz automaticamente — sugere ao aluno
- Aluno escolhe semestre: atual (ganho alto) ou anterior (ganho reduzido)
- Mínimo de 5 perguntas por quiz, 4 alternativas cada
- Ao fim: calcula aproveitamento → ganha Conhecimento → dropa item conforme tabela

### RF04 — Drop de Item por Aproveitamento do Quiz
- 80–100%: Livro Técnico
- 50–79%: Caderno de Anotações
- 0–49%: Café
- Item não adicionado se inventário estiver cheio (exibe aviso)

### RF05 — Exame de Aproveitamento
- Perguntas exclusivas do exame, mais difíceis que as do quiz
- Resposta certa: `DanoAoChefe = DanoBase × (1 + Conhecimento / 100)`
- Resposta errada: `DanoAoAluno = DanoBase × (1 - Conhecimento / 100)`, mínimo 1
- Aluno pode usar item na sua vez antes de responder
- Ao vencer: registrar aproveitamento do semestre + desbloquear habilidade

### RF06 — Bônus de Risco
- Se `Conhecimento < 40` no início do exame e aluno vencer:
  - Registrar e exibir bônus de coragem na tela de resultado
  - Aproveitamento final ainda limitado (máximo ~65 mesmo com bônus)

### RF07 — Aproveitamento e Média para o TCC
- Aproveitamento de cada semestre calculado e armazenado em `aproveitamentos[3]`
- Média final = soma dos 3 aproveitamentos / 3
- Média >= 80: bônus de Conhecimento extra contra TCC
- Média 50–79: batalha equilibrada
- Média < 50: TCC começa com ataque inicial mais forte

### RF08 — Habilidades
- Cada chefe derrotado desbloqueia uma habilidade passiva
- Habilidades acumulam e são aplicadas automaticamente nos exames seguintes
- As 3 habilidades juntas concedem vantagem cumulativa contra o TCC

### RF09 — Sistema de Inventário
- Capacidade máxima configurável por constante
- Validação antes de adicionar item
- Itens consumíveis removidos após uso
- Estado persistido no save

### RF10 — Save e Load
- Salvar: serializa estado completo em JSON
- Carregar: valida arquivo, desserializa e restaura estado
- Trancamento via Coordenador também aciona o save

### RF11 — Boss Final (TCC)
- Desbloqueado somente após completar os 3 semestres
- Usa ataque combinado dos 3 chefes escalado pela média do aluno
- Derrota: Game Over com opção de recomeço
- Vitória: tela de conclusão com estatísticas

---

## 12. Requisitos Não Funcionais

| Código | Requisito |
|---|---|
| RNF01 | Tratar exceções em todas as operações de I/O |
| RNF02 | Validar todas as entradas do usuário — entradas inválidas não quebram o fluxo |
| RNF03 | Convenção PascalCase para classes/métodos; camelCase para variáveis locais |
| RNF04 | Cada classe com responsabilidade única (SRP) |
| RNF05 | Arquivo de save legível e editável manualmente (JSON formatado) |
| RNF06 | Camada de persistência abstraída via interface para facilitar migração para banco de dados |

---

## 13. Padrões de Projeto

| Padrão | Onde Aplicar | Justificativa |
|---|---|---|
| **Strategy** | Ataque e perguntas dos chefes | Cada `Materia` encapsula sua estratégia de ataque e banco de perguntas |
| **State** | Estados do jogo | Separa comportamento por estado: Menu, Explorando, EmExame, GameOver, Vitoria |
| **Repository** | Camada de persistência | Abstrai fonte de dados (JSON ou banco), troca sem impacto no domínio |
| **Observer** | Eventos do jogo | Notifica: habilidade desbloqueada, item adquirido, semestre concluído, bônus de risco |

---

## 14. Fases de Desenvolvimento

### Fase 1 — Console (Prioridade máxima)
- [ ] Classes base abstratas: `Personagem`, `Materia`, `NPC`, `Item`
- [ ] `Aluno` com vida, conhecimento, inventário, habilidades e aproveitamentos
- [ ] Subclasses de `Materia`: IC, AED, POO, TCC com ataques e perguntas próprios
- [ ] Subclasses de `NPC`: Professor, Veterano, Coordenador
- [ ] `SistemaQuiz`: perguntas, aproveitamento, drop de item, ganho diferenciado por semestre
- [ ] `SistemaExame`: perguntas exclusivas, turnos, cálculo de dano com multiplicador
- [ ] Bônus de risco para Conhecimento baixo + vitória
- [ ] `CalculadoraMedia`: média dos 3 semestres e aplicação no TCC
- [ ] `Habilidade`: desbloqueio por vitória e aplicação passiva
- [ ] `Inventario` com limite de capacidade e validações
- [ ] Navegação entre locais via menu numerado
- [ ] Veterano fixo no Hall
- [ ] Save e Load em JSON via `GerenciadorSave`
- [ ] Tratamento de exceções e validações de input
- [ ] Tela de Game Over, vitória e conclusão com estatísticas

### Fase 2 — Interface Gráfica
- [ ] Migrar menu e navegação para Windows Forms ou WPF
- [ ] Painel visual de status: vida, conhecimento, habilidades, aproveitamentos
- [ ] Tela de exame com botões de alternativas e barra de progresso
- [ ] Tela de quiz com feedback por pergunta
- [ ] Inventário com lista visual e botão de usar

### Fase 3 — Banco de Dados
- [ ] Schema relacional: jogador, inventario, habilidades, progresso, aproveitamentos
- [ ] Implementar `IRepositorio` e `RepositorioBancoDados` (SQLite ou SQL Server)
- [ ] Manter `RepositorioJson` funcional (padrão Repository)

---

## 15. Estrutura de Arquivos Sugerida

```
CampusQuest/
├── CampusQuest.sln
├── README.md
├── docs/
│   └── documentacao.pdf
├── src/
│   ├── Program.cs
│   ├── Core/
│   │   ├── Personagem.cs           <- abstrata
│   │   ├── Aluno.cs
│   │   ├── Habilidade.cs
│   │   └── Inventario.cs
│   ├── Materias/
│   │   ├── Materia.cs              <- abstrata
│   │   ├── IC.cs
│   │   ├── AED.cs
│   │   ├── POO.cs
│   │   └── TCC.cs
│   ├── NPCs/
│   │   ├── NPC.cs                  <- abstrata
│   │   ├── Professor.cs
│   │   ├── Veterano.cs
│   │   └── Coordenador.cs
│   ├── Itens/
│   │   ├── Item.cs                 <- abstrata
│   │   ├── Cafe.cs
│   │   ├── Caderno.cs
│   │   └── LivroTecnico.cs
│   ├── Cenarios/
│   │   ├── Hall.cs
│   │   ├── SalaProfessor.cs
│   │   ├── SalaExame.cs
│   │   └── SalaCoordenacao.cs
│   ├── Quiz/
│   │   ├── SistemaQuiz.cs
│   │   └── Pergunta.cs
│   ├── Exame/
│   │   ├── SistemaExame.cs
│   │   └── CalculadoraMedia.cs
│   ├── Persistencia/
│   │   ├── IRepositorio.cs         <- interface (Repository)
│   │   ├── RepositorioJson.cs
│   │   └── RepositorioBancoDados.cs (fase 3)
│   ├── Estados/
│   │   ├── IEstadoJogo.cs          <- interface (State)
│   │   ├── EstadoMenu.cs
│   │   ├── EstadoExplorando.cs
│   │   ├── EstadoExame.cs
│   │   └── EstadoGameOver.cs
│   └── UI/
│       ├── Console/
│       │   └── MenuConsole.cs
│       └── GUI/                    (fase 2)
│           └── MainWindow.cs
└── saves/
    └── save.json
```

---

## 16. Modelo do Arquivo de Save (JSON)

```json
{
  "versao": "1.1",
  "dataSalvamento": "2026-05-10T14:30:00",
  "aluno": {
    "nome": "Jogador",
    "vidaAtual": 80,
    "vidaMaxima": 100,
    "conhecimento": 47,
    "semestreAtual": 2,
    "habilidades": [
      "Logica Computacional"
    ],
    "aproveitamentos": [72, 0, 0],
    "inventario": [
      { "tipo": "Cafe", "quantidade": 2 },
      { "tipo": "Caderno", "quantidade": 1 }
    ]
  },
  "estatisticas": {
    "semestresRepetidos": 1,
    "quizzesConcluidos": 4,
    "itensUsados": 2,
    "bonusCoragemAplicado": false
  }
}
```

---

## 17. Critérios de Aceite

| ID | Critério | Prioridade |
|---|---|---|
| CA01 | Jogo inicializa sem erros e exibe menu principal | Alta |
| CA02 | Save e Load restauram todos os atributos corretamente | Alta |
| CA03 | Cada chefe tem ataque especial e banco de perguntas únicos | Alta |
| CA04 | Quiz com semestre atual gera mais Conhecimento que revisão | Alta |
| CA05 | Drop de item do quiz segue corretamente a faixa de aproveitamento | Alta |
| CA06 | Dano no exame é calculado com o multiplicador de Conhecimento | Alta |
| CA07 | Derrota no exame repete o semestre sem avançar | Alta |
| CA08 | TCC só é acessível após os 3 semestres concluídos | Alta |
| CA09 | Média dos 3 semestres influencia o poder do aluno no TCC | Alta |
| CA10 | Inventário não ultrapassa a capacidade máxima | Alta |
| CA11 | Entradas inválidas não quebram o fluxo do jogo | Alta |
| CA12 | Bônus de risco é aplicado e registrado corretamente | Média |
| CA13 | Veterano está no Hall sem sala separada | Média |
| CA14 | Habilidades desbloqueadas afetam os exames seguintes | Média |
| CA15 | Tela de conclusão exibe estatísticas ao vencer o TCC | Média |
| CA16 | Padrões de projeto aplicados e documentados | Média |

---

## 18. Entregáveis e Datas

| Entregável | Data | Canal |
|---|---|---|
| Definição do grupo e tema (validado) | 17/04/2026 | Canvas + Aula |
| Código-fonte completo (console + GUI) | 26/06/2026 | Canvas + GitHub |
| Documentação PDF | 26/06/2026 | Canvas + GitHub |
| README.md | 26/06/2026 | GitHub |
| Apresentação (15 min) | 26/06/2026 | Sala ou Teams |

---

*PRD v1.1 — Campus Quest | PUC Minas — POO | Maio/2026*
