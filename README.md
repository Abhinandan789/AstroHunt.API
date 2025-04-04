# AstroHunt.API 🚀

A beginner-friendly .NET Core Web API project built to practice and showcase backend fundamentals including:
- 🔐 JWT Authentication
- 🧑‍🚀 User Registration & Login
- 🧾 Profile management (GET & UPDATE)
- 🗄 Entity Framework Core + SQL Server
- 📑 Swagger integration for API testing

---

## 📦 Tech Stack

- ASP.NET Core 9.0 Web API
- Entity Framework Core (EF Core)
- Microsoft SQL Server
- JWT Bearer Authentication
- Swagger / Swashbuckle for API docs

---

## 🧠 Features

- User registration with hashed passwords
- Secure login and token generation
- Protected endpoints using `[Authorize]`
- Profile management (`/me` GET and PUT)
- Token-based identity using JWT claims
- Fully documented via Swagger UI

---

## 🧪 API Endpoints

### 🔐 Auth

| Method | Endpoint            | Description              |
|--------|---------------------|--------------------------|
| POST   | `/api/Auth/register` | Register new user        |
| POST   | `/api/Auth/login`    | Login and get JWT token  |
| GET    | `/api/Auth/me`       | Test token, get username |

### 👤 Profile

| Method | Endpoint          | Description               |
|--------|-------------------|---------------------------|
| GET    | `/api/User/me`    | Get current user profile  |
| PUT    | `/api/User/me`    | Update profile (bio etc.) |

> 🔐 All `User/` endpoints require JWT Authorization

---

## 🛠 How to Run Locally

```bash
git clone https://github.com/yourusername/AstroHunt.API.git
cd AstroHunt.API

# Open with Visual Studio 2022+
# Make sure you have .NET 9 SDK and SQL Server installed

# Update appsettings.json with your SQL Server connection string

# Run migrations
dotnet ef migrations add Init
dotnet ef database update

# Run the app
dotnet run
```

Then open [https://localhost:{port}/swagger](https://localhost:{port}/swagger) to test your endpoints.

---

## 🙌 What I Learned

- How JWT works behind the scenes
- Secure password hashing & validation
- Connecting EF Core with SQL Server
- Structuring API with services, repos, and DTOs
- Using Swagger to test and document APIs
- Writing clean, readable, and secure backend code

---

## 🔮 Next Up

- Role-based access (Admin/User)
- Frontend with React
- Saving Astronomy Watchlists 🌌

---

## 📸 Screenshots (Optional)

> You can add Swagger UI screenshots or terminal outputs here

---

## 📬 Feedback or Ideas?

Feel free to reach out or fork and build your own version!
