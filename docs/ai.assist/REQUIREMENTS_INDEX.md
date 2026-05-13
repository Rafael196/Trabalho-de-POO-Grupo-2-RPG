# REQUIREMENTS_INDEX — Campus Quest
> Use este arquivo para referenciar requisitos em prompts.
> Ex: "implemente RF05 respeitando RNF02 e garanta CA06"

---

## Requisitos Funcionais (RF)

| ID | Descrição resumida |
|---|---|
| RF01 | Menu principal: Novo Jogo / Carregar Jogo / Sair |
| RF02 | Navegação entre locais via Hall; Veterano fixo no Hall |
| RF03 | Quiz com Professor: sugestão → aluno escolhe semestre → perguntas → aproveitamento → drop |
| RF04 | Drop de item por faixa de aproveitamento do quiz (80–100 / 50–79 / 0–49) |
| RF05 | Exame de Aproveitamento: perguntas exclusivas, dano calculado com multiplicador de Conhecimento |
| RF06 | Bônus de risco: Conhecimento < 40 + vitória = bônus de coragem, aproveitamento máximo ~65 |
| RF07 | Média dos 3 semestres define poder do aluno no TCC (>=80 / 50–79 / <50) |
| RF08 | Habilidades passivas desbloqueadas por vitória, aplicadas automaticamente nos exames seguintes |
| RF09 | Inventário com capacidade máxima; validação antes de adicionar; remoção após uso |
| RF10 | Save e Load em JSON; trancamento via Coordenador também salva |
| RF11 | TCC desbloqueado após 3 semestres; usa ataques combinados escalados pela média |

---

## Requisitos Não Funcionais (RNF)

| ID | Descrição resumida |
|---|---|
| RNF01 | Tratar exceções em todas as operações de I/O |
| RNF02 | Validar todas as entradas — entradas inválidas não quebram o fluxo |
| RNF03 | PascalCase para classes/métodos; camelCase para variáveis locais |
| RNF04 | Cada classe com responsabilidade única (SRP) |
| RNF05 | Arquivo de save legível e editável manualmente (JSON formatado) |
| RNF06 | Persistência abstraída via interface IRepositorio para facilitar migração |

---

## Critérios de Aceite (CA)

| ID | Critério resumido | Prioridade |
|---|---|---|
| CA01 | Jogo inicializa sem erros e exibe menu principal | Alta |
| CA02 | Save e Load restauram todos os atributos | Alta |
| CA03 | Cada chefe tem ataque especial e perguntas únicos | Alta |
| CA04 | Quiz semestre atual gera mais Conhecimento que revisão | Alta |
| CA05 | Drop de item segue faixa de aproveitamento do quiz | Alta |
| CA06 | Dano no exame usa multiplicador de Conhecimento | Alta |
| CA07 | Derrota no exame repete o semestre sem avançar | Alta |
| CA08 | TCC só acessível após 3 semestres concluídos | Alta |
| CA09 | Média dos 3 semestres influencia o TCC | Alta |
| CA10 | Inventário não ultrapassa capacidade máxima | Alta |
| CA11 | Entradas inválidas não quebram o fluxo | Alta |
| CA12 | Bônus de risco aplicado e registrado corretamente | Média |
| CA13 | Veterano no Hall sem sala separada | Média |
| CA14 | Habilidades desbloqueadas afetam exames seguintes | Média |
| CA15 | Tela de conclusão exibe estatísticas ao vencer TCC | Média |
| CA16 | Padrões de projeto aplicados e documentados | Média |

---

## Fórmulas críticas (referência rápida)

```csharp
// Dano no exame
DanoAoChefe = DanoBase * (1 + Conhecimento / 100.0)
DanoAoAluno = Math.Max(1, DanoBase * (1 - Conhecimento / 100.0))

// Aproveitamento do semestre
fatorConhecimento = 0.5 + (Conhecimento / 200.0)   // range: 0.5 a 1.0
aproveitamento    = (acertos / (float)total * 100) * fatorConhecimento

// Ganho de Conhecimento no quiz
ganhoQuiz = (semestreQuiz == semestreAtual) ? GanhoBase : (int)(GanhoBase * 0.5)

// Bônus de risco
if (conhecimentoNoInicio < 40 && alunoPagou)
    aproveitamento = Math.Min(65, aproveitamento + BonusCoragem)

// Média final para o TCC
mediaFinal = (aprov[0] + aprov[1] + aprov[2]) / 3.0f
```
