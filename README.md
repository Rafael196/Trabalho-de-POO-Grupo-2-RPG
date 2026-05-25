# Campus Quest - Trabalho de POO

Jogo de RPG em console inspirado na jornada universitaria, desenvolvido em C# com foco nos pilares de POO.

## Como rodar

### Requisitos
- .NET SDK 8 instalado

### Passo a passo (Windows)
1) Abra um terminal na raiz do repositorio.
2) Rode o comando abaixo:

```powershell
dotnet run --project backend\CampusQuest.Backend.csproj
```

### Passo a passo (macOS/Linux)
1) Abra um terminal na raiz do repositorio.
2) Rode o comando abaixo:

```bash
dotnet run --project backend/CampusQuest.Backend.csproj
```

O jogo cria o save em `backend/saves/slot1.json`.

## Estrutura do projeto (backend)
- `backend/src/Core`: entidades base do jogo (ex: `Aluno`, `Personagem`, `Inventario`).
- `backend/src/Materias`: chefes por materia (`IC`, `AED`, `POO`, `TCC`).
- `backend/src/Exame`: combate do exame e resultados.
- `backend/src/Quiz`: quiz do professor, perguntas e resultado.
- `backend/src/Itens`: itens consumiveis e permanentes.
- `backend/src/NPCs`: interacoes com NPCs.
- `backend/src/Persistencia`: contratos de save/load (stub e repositorio JSON).
- `backend/src/UI`: interfaces de I/O para desacoplar console.

## Padroes aplicados (resumo)
- Strategy: `Materia` e subclasses encapsulam ataques e perguntas.
- Repository: `IRepositorio` abstrai persistencia.
- State: fluxo do jogo separado por estados (menu, exploracao, exame).
- Observer: eventos como item adquirido ou habilidade desbloqueada.

## Responsabilidades (regra pratica)
- Logica do jogo fica em `Core`, `Exame`, `Quiz` e `Itens`.
- Entrada/saida fica em `UI` (console agora, UI grafica depois).
- Persistencia nao conhece UI; recebe apenas DTOs.

## Fluxo rapido do jogo
- Menu principal: novo jogo, carregar ou sair.
- Hall: veterano, professor, coordenacao e acesso ao exame.
- Exame: batalha por perguntas, com itens e habilidades.