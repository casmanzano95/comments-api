# 📝 Comments API

**REST API** built with **.NET 8**, **Entity Framework Core**, and **SQLite**, fully containerized with **Docker**.  
Runs on **Windows, macOS, Linux** without requiring a local .NET installation.

---

## 🚀 Requirements

- **Docker Desktop** (Windows/macOS) or Docker Engine/Compose (Linux)  
- Verify installation:

```bash
docker --version
```
---

## 🐳 Running the Project

### 1️⃣ Build & Start

```bash
docker compose up --build -d
```

> For older Docker versions:  
> ```bash>
docker-compose up --build -d
```

### 2️⃣ Stop Containers

```bash
docker compose down
```

> Database is persisted in a Docker volume: `comments_data`

---

## 🌐 Accessing the API
 
- **Base API URL:** `http://localhost:8081/index.html`

---

## 🧭 API Endpoints

| Method | URL                        | Body Example | Description |
|--------|---------------------------|--------------|------------|
| POST   | `/api/comments`           | `{ "productId": "p1", "userId": "u1", "commentText": "excelente producto" }` | Create a comment and analyze sentiment |
| GET    | `/api/comments`           | Query params: `productId` (optional), `sentiment` (optional) | List all comments with optional filters |
| GET    | `/api/sentiment-summary`  | - | Summary of comments sentiment |

---

## 🗄️ Database

- **Engine:** SQLite  
- **Connection string:** `Data Source=/data/comments.db`  
- **Persistence:** Docker volume `comments_data`  
- **Table `Comments`**  

| Column      | Type      | Description                                        |
|------------|----------|--------------------------------------------------|
| Id         | int      | Primary key                                      |
| ProductId  | string   | Product ID                                       |
| UserId     | string   | User ID                                          |
| CommentText| string   | Comment text                                     |
| Sentiment  | string   | Sentiment (`positive`, `negative`, `neutral`)  |
| CreatedAt  | DateTime | Timestamp                                        |

---

## 🧪 Running Tests

### Unit Tests

```bash
dotnet test Tests/CommentsApi.UnitTests
```

### Integration Tests

```bash
dotnet test Tests/CommentsApi.IntegrationTests
```

> Test folders may need to be created if not present.

---

## ✅ Key Features

- .NET 8 Web API  
- Entity Framework Core with migrations  
- SQLite database with Docker volume persistence  
- Docker multi-platform support (ARM / x64)  
- Automatic database initialization  
- Swagger documentation  
- Sentiment analysis with normalized text (accents removed)  

---

## 💡 Design Decisions

- **Sentiment analysis:** Based on predefined words and phrases.  
- **Database:** SQLite for portability; can be replaced with SQL Server/PostgreSQL.  
- **Docker:** API exposed on port 8081 if 8080 is busy.  
- **Swagger:** Included for easy testing.  
- **Text normalization:** Accents removed, lowercased for accurate matching.

---

## ⚡ Quick Commands

```bash
# Build and start
docker compose up --build -d

# Stop containers
docker compose down

# Check logs
docker compose logs -f

# Enter container shell (Linux only)
docker compose exec comments-api sh
```

---

## 📌 Summary

- Fully containerized, cross-platform  
- Automatic database setup and migrations  
- Swagger for testing  
- No local .NET SDK required  
- Works on macOS, Windows, Linux with Docker only

