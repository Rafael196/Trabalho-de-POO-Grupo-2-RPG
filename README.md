# Campus Quest - Trabalho de POO

Jogo de RPG inspirado na jornada universitaria, desenvolvido em C# com foco nos pilares de POO. Possui interface em console (backend) e interface gráfica WinForms (frontend, somente Windows).

## Como rodar

### Requisitos
- .NET SDK 8 instalado
- SQLite (incluído via pacote NuGet `Microsoft.Data.Sqlite`)

---

### Backend (console) — Windows, macOS e Linux

**Windows:**
```powershell
dotnet run --project backend\CampusQuest.Backend.csproj
```

**macOS/Linux:**
```bash
dotnet run --project backend/CampusQuest.Backend.csproj
```

---

### Frontend (interface gráfica WinForms) — somente Windows

> **Atenção:** o frontend usa Windows Forms e só funciona em Windows.

1) Abra um terminal na raiz do repositorio.
2) Rode o comando abaixo:

```powershell
dotnet run --project frontend\repos\CampusQuest\CampusQuest.WinForms\CampusQuest.WinForms.csproj
```

### Onde os dados são salvos
- **Save do jogo**: `backend/database/campusquest.db` (tabela SaveJogo) - persistência SQLite
- **Backup JSON**: `backend/saves/slot1.json` - formato legível (opcional)
- **Questões**: `backend/database/campusquest.db` (tabela Questoes) - banco com 30+ perguntas

O jogo inicializa automaticamente o banco de dados SQLite na primeira execução.

## Estrutura do projeto (backend)
- `backend/src/Core`: entidades base do jogo (ex: `Aluno`, `Personagem`, `Inventario`, `Habilidade`).
- `backend/src/Materias`: chefes por materia (`IC`, `AED`, `POO`, `TCC`).
- `backend/src/Exame`: combate do exame e resultados (`SistemaExame`, `CalculadoraMedia`).
- `backend/src/Quiz`: quiz do professor, perguntas e resultado (`SistemaQuiz`, `Pergunta`).
- `backend/src/Itens`: itens consumiveis e permanentes (`Cafe`, `Caderno`, `LivroTecnico`, `Cola`).
- `backend/src/NPCs`: interacoes com NPCs (`Professor`, `Veterano`, `Coordenador`).
- `backend/src/Persistencia`: contratos de save/load (JSON e SQLite).
  - `IRepositorio`: interface para persistência de save
  - `RepositorioJson`: implementação JSON (backup legível)
  - `SqliteRepositorioSave`: implementação SQLite (principal)
  - `IRepositorioQuestoes`: interface para banco de questões
  - `SqliteRepositorioQuestoes`: implementação SQLite para questões
  - `DatabaseInitializer`: inicializa BD com schema e seed de 30+ questões
  - `EstadoJogoFactory`: converte Aluno ↔ EstadoJogo (DTO)
- `backend/src/UI`: interfaces de I/O para desacoplar console (`IConsoleIO`, `ConsoleIO`).
- `backend/src/Estados`: padrão State para fluxo do jogo (`EstadoMenu`, `EstadoExplorando`, `EstadoExame`, etc).
- `backend/src/Eventos`: sistema de eventos (Observer) com `EventoBus` e eventos tipados.
- `backend/src/Testes`: testes manuais de fluxo (`FluxoTeste`).
- `backend/database/`: banco SQLite criado em runtime (gitignored).
- `backend/saves/`: backup JSON dos saves (opcional).

## Padroes aplicados (resumo)
- **Strategy**: `Materia` e subclasses encapsulam ataques e perguntas.
- **Repository**: `IRepositorio` abstrai persistencia (JSON e SQLite).
- **State**: fluxo do jogo separado por estados (menu, exploracao, exame, vitoria, game over).
- **Observer**: eventos como item adquirido ou habilidade desbloqueada via `EventoBus`.
- **Factory**: `EstadoJogoFactory` converte entre domínio (Aluno) e DTO (EstadoJogo).

## Responsabilidades (regra pratica)
- Logica do jogo fica em `Core`, `Exame`, `Quiz` e `Itens`.
- Entrada/saida fica em `UI` (console via backend, interface gráfica via frontend WinForms).
- Persistencia nao conhece UI; recebe apenas DTOs.

## Fluxo rapido do jogo
- Menu principal: novo jogo, carregar ou sair.
- Hall: veterano, professor, coordenacao e acesso ao exame.
- Exame: batalha por perguntas, com itens e habilidades.