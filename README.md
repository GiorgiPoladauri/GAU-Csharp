# 🟩 Wordle API — .NET 8 Clean Architecture

A fully featured RESTful Wordle game API built with **C# .NET 8**, **Clean Architecture**, **EF Core Code First**, and **SQL Server**.

---

## 📁 Project Structure

```
WordleAPI/
├── WordleAPI.Domain/           # Entities, Enums (no dependencies)
│   ├── Entities/
│   │   ├── Game.cs
│   │   └── Guess.cs
│   └── Enums/
│       ├── LetterStatus.cs
│       └── GameStatus.cs
│
├── WordleAPI.Application/      # Business logic, Interfaces, DTOs
│   ├── DTOs/
│   │   └── GameDTOs.cs
│   ├── Interfaces/
│   │   ├── IGameRepository.cs
│   │   ├── IGuessRepository.cs
│   │   ├── IWordService.cs
│   │   └── IGameService.cs
│   └── Services/
│       └── GameService.cs      ← Core Wordle logic
│
├── WordleAPI.Infrastructure/   # EF Core, Repositories, WordService
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   ├── Migrations/
│   ├── Repositories/
│   │   ├── GameRepository.cs
│   │   └── GuessRepository.cs
│   └── Services/
│       └── WordService.cs
│
└── WordleAPI.API/              # ASP.NET Core Web API
    ├── Controllers/
    │   └── GamesController.cs
    ├── Program.cs
    ├── appsettings.json
    └── appsettings.Development.json
```

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB, Express, or full)
- (Optional) Visual Studio 2022 or VS Code

### 1. Configure the Connection String

Edit `WordleAPI.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=WordleDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

For LocalDB:
```
Server=(localdb)\\mssqllocaldb;Database=WordleDB;Trusted_Connection=True;
```

### 2. Apply Migrations

The app auto-migrates on startup. Or run manually:

```bash
cd WordleAPI.API
dotnet ef database update --project ../WordleAPI.Infrastructure
```

### 3. Run the API

```bash
cd WordleAPI.API
dotnet run
```

Swagger UI opens at: **http://localhost:5000**

---

## 📡 API Endpoints

### POST `/api/games/start`
Start a new game.

**Response 201:**
```json
{
  "gameId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "startDate": "2024-01-01T10:00:00Z",
  "maxAttempts": 6,
  "wordLength": 5,
  "message": "Game started! Guess the 5-letter word. You have 6 attempts."
}
```

---

### POST `/api/games/guess`
Make a guess.

**Request body:**
```json
{
  "gameId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "word": "crane"
}
```

**Response 200:**
```json
{
  "gameId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "word": "CRANE",
  "guessNumber": 1,
  "attemptsRemaining": 5,
  "letterResults": [
    { "letter": "C", "position": 0, "status": "absent",  "statusText": "absent"  },
    { "letter": "R", "position": 1, "status": "correct", "statusText": "correct" },
    { "letter": "A", "position": 2, "status": "present", "statusText": "present" },
    { "letter": "N", "position": 3, "status": "absent",  "statusText": "absent"  },
    { "letter": "E", "position": 4, "status": "correct", "statusText": "correct" }
  ],
  "gameStatus": "InProgress",
  "gameStatusText": "InProgress",
  "isCorrect": false,
  "message": "Wrong guess. 5 attempt(s) remaining."
}
```

---

## 🎮 Game Rules

| Rule | Detail |
|------|--------|
| Max attempts | **6** |
| Word length | **5 letters** |
| Letter statuses | `correct`, `present`, `absent` |
| Duplicate handling | Correct positions claimed first; remaining duplicates handled fairly |

### Letter Status Logic

- 🟩 **correct** — letter is in the word at the exact position
- 🟨 **present** — letter is in the word but at a different position
- ⬜ **absent** — letter is not in the word

---

## 🗄️ Database Models

### `Games` Table
| Column | Type | Description |
|--------|------|-------------|
| Id | uniqueidentifier | PK |
| TargetWord | nvarchar(5) | The secret word |
| StartDate | datetime2 | When game started |
| EndDate | datetime2? | When game ended |
| Attempts | int | Number of guesses made |
| IsWin | bit | Whether the player won |
| Status | nvarchar(20) | InProgress / Won / Lost |

### `Guesses` Table
| Column | Type | Description |
|--------|------|-------------|
| Id | uniqueidentifier | PK |
| GameId | uniqueidentifier | FK → Games |
| Word | nvarchar(5) | The guessed word |
| GuessNumber | int | Attempt number (1–6) |
| GuessResult | nvarchar(1000) | JSON array of letter results |

---

## 🛠️ Technologies

- **C# / .NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core 8** (Code First)
- **SQL Server**
- **Swagger / Swashbuckle**
- **Clean Architecture** (Domain → Application → Infrastructure → API)
