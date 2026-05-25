# IMPLEMENTATION STATUS — Campus Quest

Resumo do que está implementado no backend e o que falta, mapeado contra os Requisitos Funcionais (RF).

- **RF01 — Menu Principal**: Implementado — fluxo principal via State (`EstadoMenu`).
- **RF02 — Navegação entre Locais**: Implementado — Hall e opcoes de navegacao no `EstadoExplorando`.
- **RF03 — Quiz do Professor**: Implementado — `SistemaQuiz` com escolha de semestre e ganho de Conhecimento.
- **RF04 — Drop de Item por Quiz**: Implementado — item por faixa de aproveitamento.
- **RF05 — Exame de Aproveitamento**: Implementado — fluxo de perguntas, dano e resultado com narracao.
- **RF06 — Bônus de Risco**: Implementado — bonus de coragem quando conhecimento inicial < 40.
- **RF07 — TCC (boss final)**: Implementado — `TCC` escala com media final.
- **RF08 — Habilidades**: Parcial — `Habilidade` aplicada no exame; desbloqueio ainda depende do fluxo do jogo.
- **RF09 — Inventário**: Parcial — inventario e itens existem; uso completo fora do exame ainda pendente.
- **RF10 — Save e Load (JSON)**: Implementado — `RepositorioJson` grava em `backend/saves/slot1.json`.
- **RF11 — Boss Final: fluxo completo**: Implementado — `TCC` acessivel apos 3 semestres via estados.

Novidades recentes
- Sistema de estados completo (menu, exploracao, exame, vitoria, game over).
- NPCs com saudacoes personalizadas, dicas e item request por conversa.
- Quiz com banco de perguntas em array e ordem aleatoria.
- Exame com logs de dano, ataques especiais, efeitos de itens e resumo final.

Pontos de atencao
- Persistencia JSON usa arquivo local; persistencia em BD fica para etapa futura.
- Fluxo de uso de itens fora do exame ainda limitado.


