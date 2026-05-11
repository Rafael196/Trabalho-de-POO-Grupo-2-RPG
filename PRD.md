# PRD — Campus Quest: A Jornada Universitária
> **Product Requirements Document** | Versão 1.0 | Maio/2026  
> Trabalho Prático — Programação Orientada por Objetos | PUC Minas Betim

---

## 1. Visão do Produto

**Campus Quest** é um jogo de RPG de texto desenvolvido em **C#**, ambientado na vida universitária do curso de Tecnologia da Informação. O jogador controla um estudante que enfrenta as disciplinas do curso como chefes em batalhas por turno, acumulando habilidades ao longo de 3 semestres para finalmente vencer o desafio máximo: o TCC.

O projeto tem duplo propósito:
- **Acadêmico:** demonstrar domínio dos 4 pilares da POO (Abstração, Encapsulamento, Herança e Polimorfismo) com boas práticas de programação em C#.
- **Produto:** entregar um jogo funcional, coeso e com progressão de personagem clara e satisfatória.

---

## 2. Escopo

### Dentro do escopo — v1 (entrega final)
- Jornada individual de um jogador
- 3 semestres com um chefe por semestre + boss final (TCC)
- Sistema de combate por turnos baseado em escolhas
- Sistema de quiz integrado ao NPC Professor
- Árvore de habilidades desbloqueadas por chefe derrotado
- Sistema de itens com inventário limitado
- 3 NPCs com interações distintas
- Save e Load em arquivo JSON
- Menu interativo em console (fase 1)
- Interface gráfica (fase 2)
- Migração para banco de dados (fase 3)

### Fora do escopo — v1
- Multiplayer ou cooperativo
- Múltiplos cursos (apenas TI por ora)
- Eventos aleatórios por semestre
- Geração procedural de cenários
- Integração com APIs externas

---

## 3. Estrutura da Jornada

```
[INÍCIO]
    |
   Hall
    |
   ├── Sala do Professor  ──> Quiz → aumenta Conhecimento
   ├── Sala do Veterano   ──> Dicas + Itens
   ├── Sala da Coordenação ──> Tranca semestre (salva sem batalhar)
    |
   Batalha (Semestre 1)
   Chefe: Introdução à Computação (IC)
   Derrota → Repete Semestre 1
   Vitória → Desbloqueia: Lógica Computacional
    |
   Batalha (Semestre 2)
   Chefe: Algoritmos e Estrutura de Dados (AED)
   Derrota → Repete Semestre 2
   Vitória → Desbloqueia: Pensamento Algorítmico
    |
   Batalha (Semestre 3)
   Chefe: Programação Orientada a Objetos (POO)
   Derrota → Repete Semestre 3
   Vitória → Desbloqueia: Abstração e Modelagem
    |
   Boss Final: TCC
   Derrota → Game Over (com opção de recomeço)
   Vitória → [FIM — Tela de conclusão + estatísticas]
```

---

## 4. Entidades e Arquitetura de Classes

### Mapeamento com os Pilares da POO

| Pilar | Onde se aplica | Implementação |
|---|---|---|
| **Abstração** | Classes base do sistema | `Personagem`, `Materia`, `NPC`, `Item` são classes abstratas |
| **Encapsulamento** | Atributos de todas as classes | `vida`, `conhecimento`, `inventario` privados com getters/setters controlados |
| **Herança** | Subclasses das entidades base | `Aluno : Personagem`; `IC, AED, POO, TCC : Materia`; `Professor, Veterano, Coordenador : NPC` |
| **Polimorfismo** | Comportamento dos chefes e NPCs | Cada `Materia` implementa `Atacar()` diferente; cada `NPC` implementa `Interagir()` próprio |

---

### 4.1 Diagrama de Classes (simplificado)

```
┌─────────────────┐
│  Personagem     │  ← abstrata
│─────────────────│
│ #vida: int      │
│ #conhecimento:  │
│   int           │
│─────────────────│
│ +Atacar()       │
│ +ReceberDano()  │
│ +EstaVivo()     │
└────────┬────────┘
         │ herda
    ┌────┴────┐
    │  Aluno  │
    │─────────│
    │ inventario: Inventario
    │ habilidades: List<Habilidade>
    │ semestreAtual: int
    │─────────│
    │ +UsarItem()
    │ +AdicionarHabilidade()
    └─────────┘

┌─────────────────┐
│   Materia       │  ← abstrata
│─────────────────│
│ #nome: string   │
│ #vidaAtual: int │
│ #vidaMax: int   │
│─────────────────│
│ +Atacar(aluno)* │
│ +ReceberDano()  │
│ +EstaVencido()  │
│ +GetDescricao() │
└────────┬────────┘
         │ herda
   ┌─────┼──────┬──────┐
   │     │      │      │
  IC    AED    POO    TCC
  (Loop  (Stack (Null  (Combinado)
  Infinito) Overflow) Pointer)

┌─────────────────┐
│      NPC        │  ← abstrata
│─────────────────│
│ #nome: string   │
│─────────────────│
│ +Interagir()*   │
│ +GetDialogo()   │
└────────┬────────┘
         │ herda
   ┌─────┼──────┐
   │     │      │
Professor Veterano Coordenador
+AplicarQuiz() +OfereceItem() +TrancarSemestre()
+Conversar()   +DarDica()

┌─────────────────┐      ┌──────────────────┐
│    Inventario   │      │    Habilidade     │
│─────────────────│      │──────────────────│
│ -itens: List    │      │ -nome: string     │
│ -capacidadeMax  │      │ -descricao: string│
│─────────────────│      │ -efeito: TipoEfeito
│ +Adicionar()    │      │──────────────────│
│ +Remover()      │      │ +Aplicar(aluno)   │
│ +EstaCheia()    │      └──────────────────┘
└─────────────────┘

┌─────────────────┐      ┌──────────────────┐
│      Item       │  ←ab │  SistemaQuiz     │
│─────────────────│      │──────────────────│
│ -nome: string   │      │ -perguntas: List  │
│ -descricao: str │      │──────────────────│
│─────────────────│      │ +IniciarQuiz()    │
│ +Usar(aluno)*   │      │ +AvaliarResposta()│
└────────┬────────┘      │ +CalcularBonus()  │
         │               └──────────────────┘
   ┌─────┼──────┐
  Cafe  Caderno LivroTecnico
               (Cola — especial)

┌─────────────────┐      ┌──────────────────┐
│   GerenciadorSave│      │  Cenario / Sala  │
│─────────────────│      │──────────────────│
│ +Salvar(estado) │      │ -nome: string     │
│ +Carregar()     │      │ -npcsPresentes    │
│ +ExisteArquivo()│      │──────────────────│
└─────────────────┘      │ +Entrar(aluno)    │
                         │ +ListarOpcoes()   │
                         └──────────────────┘
```

---

## 5. Requisitos Funcionais

### RF01 — Menu Principal
- O jogo deve exibir menu com opções: **Novo Jogo**, **Carregar Jogo**, **Sair**
- Novo Jogo inicializa o Aluno com atributos padrão e salva estado inicial
- Carregar Jogo lê o arquivo JSON e restaura o estado anterior

### RF02 — Navegação entre Cenários
- O jogador deve poder navegar entre: Hall, Sala do Professor, Sala do Veterano, Sala da Coordenação e Sala de Batalha
- O Hall deve apresentar as opções de destino via menu numerado
- Cada sala exibe descrição e opções disponíveis

### RF03 — Sistema de Combate
- Combate ocorre por turnos alternados: Aluno ataca → Chefe ataca → Aluno escolhe próxima ação
- Opções por turno: **Atacar**, **Usar Item**, **Desistir**
- Dano do Aluno calculado com base no atributo Conhecimento + bônus de habilidades
- Cada chefe possui método de ataque único com efeitos distintos
- Vitória: vida do chefe chega a 0
- Derrota: vida do Aluno chega a 0 ou Aluno desiste

### RF04 — Progressão de Personagem
- Ao vencer um semestre, o Aluno desbloqueia automaticamente a habilidade correspondente
- Habilidades são passivas e aplicadas automaticamente em batalhas posteriores
- O atributo Conhecimento deve crescer progressivamente ao longo do jogo

### RF05 — Sistema de Quiz
- Quiz disponível na Sala do Professor, uma vez por visita
- Mínimo de 5 perguntas por semestre, temáticas à matéria atual
- Cada resposta correta incrementa o Conhecimento do Aluno
- Perguntas apresentadas em ordem, com 4 alternativas cada

### RF06 — NPCs
- **Professor:** exibe diálogo introdutório → opção de fazer quiz
- **Veterano:** exibe dica aleatória sobre o chefe atual → opção de receber/comprar item
- **Coordenador:** exibe opção de trancar o semestre (salva progresso e retorna ao menu)

### RF07 — Sistema de Itens
- O Inventário tem capacidade máxima definida
- Itens podem ser usados em qualquer momento fora de combate ou durante batalha (na vez do Aluno)
- Ao usar um item, ele é removido do inventário
- Validar se o inventário está cheio antes de adicionar novo item

### RF08 — Save e Load
- Estado salvo em arquivo `.json` na pasta do executável
- Estado inclui: semestreAtual, vidaAtual, conhecimento, inventário, habilidades desbloqueadas
- Load restaura exatamente o estado salvo
- Deve existir validação se o arquivo de save existe antes de tentar carregar

### RF09 — Boss Final (TCC)
- Desbloqueado somente após completar os 3 semestres
- Usa habilidades combinadas dos 3 chefes anteriores
- Derrota → Game Over com opção de recomeço (novo jogo)
- Vitória → tela de conclusão com estatísticas (semestres repetidos, quizzes feitos, itens usados)

---

## 6. Requisitos Não Funcionais

| Código | Requisito |
|---|---|
| RNF01 | O sistema deve tratar exceções em todas as operações de I/O (leitura/escrita de arquivos) |
| RNF02 | Todas as entradas do usuário devem ser validadas (entradas inválidas não devem quebrar o jogo) |
| RNF03 | O código deve seguir a convenção PascalCase para classes e métodos, e camelCase para variáveis |
| RNF04 | Cada classe deve ter responsabilidade única (Single Responsibility Principle) |
| RNF05 | O arquivo de save deve ser legível e editável manualmente (JSON formatado) |
| RNF06 | A camada de persistência deve ser abstraída para facilitar a migração para banco de dados |

---

## 7. Padrões de Projeto

| Padrão | Onde Aplicar | Justificativa |
|---|---|---|
| **Strategy** | Sistema de ataque dos chefes | Cada Materia encapsula sua estratégia de ataque de forma intercambiável |
| **State** | Estados do jogo | Separar comportamento dos estados: Menu, Explorando, EmBatalha, GameOver, Vitoria |
| **Repository** | Camada de persistência | Abstrai a fonte de dados (JSON ou banco), permitindo troca sem impacto no domínio |
| **Observer** | Eventos do jogo | Notifica o sistema de eventos como: habilidade desbloqueada, item adquirido, semestre concluído |

---

## 8. Fases de Desenvolvimento

### Fase 1 — Console (Prioridade máxima)
- [ ] Estrutura de classes base (Personagem, Materia, NPC, Item)
- [ ] Subclasses de Materia (IC, AED, POO, TCC) com ataques distintos
- [ ] Subclasses de NPC (Professor, Veterano, Coordenador)
- [ ] Sistema de combate por turnos
- [ ] Sistema de quiz com perguntas por semestre
- [ ] Árvore de habilidades (desbloqueio por vitória)
- [ ] Sistema de inventário com limite de capacidade
- [ ] Cenários e navegação entre salas via menu
- [ ] Save e Load em JSON
- [ ] Menu principal funcional
- [ ] Tratamento de exceções e validações de input
- [ ] Tela de Game Over e tela de Vitória final

### Fase 2 — Interface Gráfica
- [ ] Migrar menu e navegação para Windows Forms ou WPF
- [ ] Exibir status do personagem (vida, conhecimento, habilidades) em painel visual
- [ ] Combate com botões de ação
- [ ] Inventário com lista visual e botão de usar
- [ ] Quiz com tela própria

### Fase 3 — Banco de Dados
- [ ] Criar schema relacional para persistência (tabelas: jogador, inventario, habilidades, progresso)
- [ ] Implementar repositório para banco de dados (SQLite ou SQL Server)
- [ ] Migrar GerenciadorSave para usar o novo repositório
- [ ] Manter compatibilidade com repositório JSON (padrão Repository)

---

## 9. Estrutura de Arquivos Sugerida

```
CampusQuest/
├── CampusQuest.sln
├── README.md
├── docs/
│   └── documentacao.pdf
├── src/
│   ├── Program.cs                  ← ponto de entrada
│   ├── Core/
│   │   ├── Personagem.cs           ← abstrata
│   │   ├── Aluno.cs
│   │   ├── Habilidade.cs
│   │   └── Inventario.cs
│   ├── Materias/
│   │   ├── Materia.cs              ← abstrata
│   │   ├── IC.cs
│   │   ├── AED.cs
│   │   ├── POO.cs
│   │   └── TCC.cs
│   ├── NPCs/
│   │   ├── NPC.cs                  ← abstrata
│   │   ├── Professor.cs
│   │   ├── Veterano.cs
│   │   └── Coordenador.cs
│   ├── Itens/
│   │   ├── Item.cs                 ← abstrata
│   │   ├── Cafe.cs
│   │   ├── Caderno.cs
│   │   └── LivroTecnico.cs
│   ├── Cenarios/
│   │   ├── Cenario.cs
│   │   ├── Hall.cs
│   │   ├── SalaProfessor.cs
│   │   ├── SalaVeterano.cs
│   │   └── SalaCoordenacao.cs
│   ├── Quiz/
│   │   ├── SistemaQuiz.cs
│   │   └── Pergunta.cs
│   ├── Persistencia/
│   │   ├── IRepositorio.cs         ← interface (padrão Repository)
│   │   ├── RepositorioJson.cs
│   │   └── RepositorioBancoDados.cs (fase 3)
│   ├── Estados/
│   │   ├── IEstadoJogo.cs          ← interface (padrão State)
│   │   ├── EstadoMenu.cs
│   │   ├── EstadoExplorando.cs
│   │   ├── EstadoBatalha.cs
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

## 10. Modelo do Arquivo de Save (JSON)

```json
{
  "versao": "1.0",
  "dataSalvamento": "2026-05-10T14:30:00",
  "aluno": {
    "nome": "Jogador",
    "vidaAtual": 80,
    "vidaMaxima": 100,
    "conhecimento": 35,
    "semestreAtual": 2,
    "habilidades": [
      "Lógica Computacional"
    ],
    "inventario": [
      { "tipo": "Cafe", "quantidade": 2 },
      { "tipo": "Caderno", "quantidade": 1 }
    ]
  },
  "estatisticas": {
    "semestresRepetidos": 0,
    "quizzesConcluidos": 3,
    "itensUsados": 1
  }
}
```

---

## 11. Critérios de Aceite

| ID | Critério | Prioridade |
|---|---|---|
| CA01 | O jogo inicializa sem erros e exibe menu principal | Alta |
| CA02 | Save e Load funcionam corretamente restaurando todos os atributos | Alta |
| CA03 | Cada chefe possui comportamento de ataque distinto e identificável | Alta |
| CA04 | Quiz aumenta o atributo Conhecimento do Aluno corretamente | Alta |
| CA05 | Derrota em batalha resulta em repetição do semestre (não avança) | Alta |
| CA06 | TCC só é acessível após completar os 3 semestres | Alta |
| CA07 | Inventário não ultrapassa o limite de capacidade definido | Alta |
| CA08 | Entradas inválidas do usuário não quebram o fluxo do jogo | Alta |
| CA09 | Habilidades desbloqueadas afetam o combate dos semestres seguintes | Média |
| CA10 | Tela de conclusão exibe estatísticas ao vencer o TCC | Média |
| CA11 | Código está organizado segundo a estrutura de pastas definida | Média |
| CA12 | Padrões de projeto estão aplicados e documentados | Média |

---

## 12. Entregáveis e Datas

| Entregável | Data | Canal |
|---|---|---|
| Definição do grupo e tema (validado pelo professor) | 17/04/2026 | Canvas + Aula |
| Código-fonte completo (console + GUI) | 26/06/2026 | Canvas + GitHub |
| Documentação PDF (arquitetura, diagramas, padrões) | 26/06/2026 | Canvas + GitHub |
| README.md com instruções de compilação e execução | 26/06/2026 | GitHub |
| Apresentação (15 minutos) | 26/06/2026 | Sala ou Microsoft Teams |

---

## 13. Observações

- Bibliotecas externas são permitidas desde que referenciadas no README e na documentação
- O tema foi escolhido fora da lista sugerida e deve ser validado com o professor
- Este documento deve ser atualizado conforme o projeto evolui
- Dúvidas via Canvas ou em aula

---

*PRD v1.0 — Campus Quest | PUC Minas — POO | Maio/2026*
