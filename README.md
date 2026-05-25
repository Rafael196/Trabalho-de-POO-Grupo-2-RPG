# Trabalho-de-POO

## Guia rapido de arquitetura e padroes

### Estrutura por dominio
- `backend/src/Core`: entidades base do jogo (ex: `Aluno`, `Personagem`, `Inventario`).
- `backend/src/Materias`: chefes por materia (`IC`, `AED`, `POO`, `TCC`).
- `backend/src/Exame`: combate do exame e resultados.
- `backend/src/Quiz`: quiz do professor, perguntas e resultado.
- `backend/src/Itens`: itens consumiveis e permanentes.
- `backend/src/NPCs`: interacoes com NPCs.
- `backend/src/Persistencia`: contratos de save/load (stub e futuro repositorio).
- `backend/src/UI`: interfaces de I/O para desacoplar console.

### Padroes aplicados (resumo)
- Strategy [OK]: `Materia` e subclasses encapsulam ataques e perguntas.
- Repository [OK]: `IRepositorio` abstrai persistencia.
- State [OK]: fluxo do jogo separado por estados (menu, exploracao, exame).
- Observer [OK]: eventos como item adquirido ou habilidade desbloqueada.

### Responsabilidades (regra pratica)
- Logica de regra do jogo fica em `Core`, `Exame`, `Quiz` e `Itens`.
- Entrada/saida fica em `UI` (console agora, UI grafica depois).
- Persistencia nunca conhece a UI; recebe somente DTOs.

### Pontos de integracao
- UI chama `SistemaQuiz` e `SistemaExame` com `IConsoleIO`.
- Itens aplicam efeito e o exame calcula bonus temporarios/permanentes.
- Persistencia recebe `EstadoJogo` e devolve o mesmo DTO.