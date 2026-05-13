# CONTEXT_COMPACT — Campus Quest
> Cole este arquivo no início de qualquer prompt para IA ter contexto completo com mínimo de tokens.

---

## Stack e Ambiente
- Linguagem: C# (.NET 8+)
- Console (fase atual) → Windows Forms/WPF (fase 2) → Banco de dados (fase 3)
- Persistência atual: JSON via `System.Text.Json`
- Estrutura: `src/` com subpastas por domínio (Core, Materias, NPCs, Itens, Quiz, Exame, Persistencia, Estados, UI)

## O que é o jogo
RPG de texto universitário. Jogador = Aluno. Vilões = matérias do curso (IC, AED, POO). Boss final = TCC. Jornada individual, 3 semestres + boss final. Derrota em semestre = repetição do semestre.

## Atributo central: Conhecimento
- Não causa dano diretamente — é multiplicador de desempenho
- Sobe via quiz com Professor e itens
- `DanoAoChefe = DanoBase * (1 + Conhecimento / 100.0)`
- `DanoAoAluno  = Math.Max(1, DanoBase * (1 - Conhecimento / 100.0))`

## Exame de Aproveitamento (batalha)
- Perguntas exclusivas por matéria (mais difíceis que quiz), mín. 8
- Resposta certa → dano ao chefe; errada → dano ao aluno
- Aproveitamento registrado (0–100) ao vencer: `(acertos/total*100) * (0.5 + Conhecimento/200.0)`
- Bônus de risco: Conhecimento < 40 + vitória = bônus de coragem, aproveitamento máximo ~65
- Cada chefe tem ataque especial: IC=LoopInfinito, AED=StackOverflow, POO=NullPointer, TCC=SíntesTotal

## Média final para o TCC
- `mediaFinal = (aprov[0] + aprov[1] + aprov[2]) / 3.0f`
- >= 80 → aluno com vantagem | 50–79 → equilibrado | < 50 → TCC com vantagem

## Quiz com Professor
- Professor sugere; aluno escolhe semestre (atual = ganho cheio, anterior = 50% do ganho)
- Drop por aproveitamento do quiz: 80–100% = LivroTecnico | 50–79% = Caderno | 0–49% = Café

## Locais
Hall (hub central com Veterano fixo) → Sala do Professor | Sala de Exame | Sala da Coordenação

## NPCs
- Professor (Sala do Professor): sugere quiz, aplica quiz, diálogo temático
- Veterano (Hall, fixo): dicas sobre chefe atual + itens consumíveis
- Coordenador (Sala da Coord.): tranca semestre (salva progresso sem batalhar)

## Hierarquia de classes (resumo)
```
Personagem (abs) ← Aluno
Materia    (abs) ← IC, AED, POO, TCC
NPC        (abs) ← Professor, Veterano, Coordenador
Item       (abs) ← Cafe, Caderno, LivroTecnico
IRepositorio    ← RepositorioJson, RepositorioBancoDados (fase 3)
IEstadoJogo     ← EstadoMenu, EstadoExplorando, EstadoExame, EstadoGameOver, EstadoVitoria
```

## Padrões de projeto em uso
| Padrão | Onde |
|---|---|
| Strategy | Ataque e perguntas de cada Materia |
| State | JogoContexto + IEstadoJogo |
| Repository | IRepositorio + implementações |
| Observer | Eventos: habilidade desbloqueada, item adquirido, semestre concluído |

## Regras de nomenclatura
- Classes e métodos: PascalCase
- Variáveis locais e parâmetros: camelCase
- Constantes: PascalCase em propriedade `const` dentro da classe dona
- Arquivo = nome da classe (um por arquivo)

## O que NÃO existe no projeto
- Multiplayer | Eventos aleatórios | Múltiplos cursos | Geração procedural
