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

## 📥 Cloning the Repository

```bash
git clone https://github.com/casmanzano95/comments-api.git
cd comments-api
```

Replace `yourusername` with the actual GitHub username or repository URL.

---

## 🐳 Running the Project

### 1️⃣ Build & Start Containers

```bash
docker compose build
docker compose up -d
```

> For older Docker versions:  
> ```bash>
docker-compose build
docker-compose up -d
```

This will:
- Build the Docker image for the API
- Start the API container
- Apply EF Core migrations automatically
- Create the SQLite database and tables inside a Docker volume

### 2️⃣ Stop Containers

```bash
docker compose down
```

> Database is persisted in a Docker volume: `comments_data`

### 3️⃣ View Logs (Optional)

```bash
docker compose logs -f
```

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
# Build Docker image
docker compose build

# Start containers
docker compose up -d

#