# Freelance Marketplace Platform — Backend API

ASP.NET Core Web API для платформи фріланс-маркетплейсу. Реалізує повний цикл: реєстрація та автентифікація, управління проектами, заявками, котируваннями, контрактами (з майлстоунами), обмін повідомленнями, відгуки, спори, сповіщення в реальному часі та адміністративна панель.

---

## 🚀 Стек технологій

| Категорія | Технологія |
|---|---|
| Framework | ASP.NET Core 8 (.NET 8) |
| Мова | C# |
| ORM | Entity Framework Core 8 + Npgsql |
| База даних | PostgreSQL 16 |
| CQRS / Mediator | MediatR 13 |
| Валідація | FluentValidation 12 |
| Маппінг | AutoMapper 14 |
| Автентифікація | JWT Bearer + Google OAuth 2.0 |
| Real-time | SignalR (`NotificationHub`) |
| Документація | Swagger / OpenAPI (Swashbuckle) |
| Логування | Serilog (Console sink) |
| DI / scan | Scrutor |
| Контейнеризація | Docker + Docker Compose |
| CI/CD | GitHub Actions → GHCR |
| Тестування | xUnit (Integration Tests) |

---

## 📁 Структура проекту

```
FreelanceBack/
├── src/
│   ├── API/        # Presentation layer — Controllers, Program.cs, Middleware
│   ├── BLL/        # Business Logic — Commands/Queries (CQRS), Services, Hubs, ViewModels
│   ├── DAL/        # Data Access — AppDbContext, Repositories, Migrations
│   └── Domain/     # Domain models, enums, base abstractions
├── tests/
│   ├── Api.Tests.Integration/   # Integration tests (xUnit + WebApplicationFactory)
│   ├── Tests.Common/            # BaseIntegrationTest, TestFactory, helpers
│   └── TestsData/               # Фікстури та тестові дані
├── .github/workflows/ci-cd.yml  # CI/CD pipeline (Build → Test → Docker Push)
├── Dockerfile
├── docker-compose.yml
└── .env.example
```

---

## 🔐 Автентифікація та ролі

### Ролі
| Роль | Опис |
|---|---|
| `admin` | Повний доступ до системи |
| `employer` | Управління проектами, контрактами, прийняття квот |
| `freelancer` | Подача заявок, виконання контрактів, портфоліо |
| `moderator` | Розгляд та вирішення спорів |

### Авторизаційні політики
| Політика | Ролі |
|---|---|
| `AdminOrEmployer` | admin, employer |
| `AdminOrFreelancer` | admin, freelancer |
| `AdminOrModerator` | admin, moderator |
| `AnyAuthenticated` | будь-яка авторизована роль |

### Підтримувані методи входу
- **JWT** — стандартний sign-up / sign-in
- **Google OAuth 2.0** — зовнішній вхід через Google

---

## 🔌 API Контролери

### **AccountController** `/account`
| Метод | Endpoint | Доступ |
|---|---|---|
| POST | `/sign-up` | Публічний |
| POST | `/sign-in` | Публічний |
| POST | `/external-login` | Публічний (Google OAuth) |

---

### **UserController** `/user`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/get-myself` | Авторизований |
| PATCH | `/update-avatar` | Авторизований |
| GET | `/roles` | Авторизований |
| GET | `/proficiency-levels` | Авторизований |
| POST | `/languages` | Авторизований |
| PUT | `/languages` | Авторизований |
| DELETE | `/languages/{languageId}` | Авторизований |
| GET | `/` | Admin only |
| GET | `/{id}` | Admin only |
| GET | `/paginated` | Admin only |
| POST | `/` | Admin only |
| PUT | `/{id}` | Admin only |
| DELETE | `/{id}` | Admin only |

---

### **FreelancerController** `/freelancer`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/` | Admin / Freelancer |
| GET | `/{id}` | Admin / Freelancer |
| GET | `/{email}` | Admin / Freelancer |
| PUT | `/` | Admin / Freelancer |
| PUT | `/skills` | Admin / Freelancer |

---

### **EmployerController** `/employer`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/` | Admin / Employer |
| PUT | `/` | Admin / Employer |

---

### **ProjectController** `/project`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/` | Публічний |
| GET | `/{id}` | Публічний |
| GET | `/search` | Публічний (фільтрація + пагінація) |
| POST | `/` | Admin / Employer |
| PUT | `/{id}` | Admin / Employer |
| DELETE | `/{id}` | Admin / Employer |
| PATCH | `/categories/{projectId}` | Admin / Employer |
| GET | `/by-employer` | Admin / Employer |

---

### **ProjectMilestoneController** `/projectmilestone`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/{id}` | Admin / Employer |
| POST | `/` | Admin / Employer |
| PUT | `/{id}` | Admin / Employer |
| DELETE | `/{id}` | Admin / Employer |
| GET | `/by-project/{projectId}` | Публічний |
| GET | `/milestone-status-enums` | Admin / Employer |

---

### **BidController** `/bid`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/{id}` | Публічний |
| POST | `/` | Admin / Freelancer |
| PUT | `/{id}` | Admin / Freelancer |
| DELETE | `/{id}` | Admin / Freelancer |
| GET | `/by-project/{projectId}` | Публічний |

---

### **QuoteController** `/quote`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/{id}` | Публічний |
| POST | `/` | Admin / Freelancer |
| PUT | `/{id}` | Admin / Freelancer |
| DELETE | `/{id}` | Admin / Freelancer |
| GET | `/by-project/{projectId}` | Публічний |

---

### **ContractController** `/contract`
| Метод | Endpoint | Доступ |
|---|---|---|
| POST | `/{quoteId}` | Employer only |
| PUT | `/` | Employer only |
| PUT | `/update-status/{contractId}` | Employer only |
| GET | `/status-enums` | Авторизований |
| GET | `/by-user` | Авторизований |
| GET | `/completed-by-freelancer-id/{freelancerId}` | Авторизований |

---

### **ContractMilestoneController** `/contractmilestone`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/{id}` | Авторизований |
| POST | `/` | Авторизований |
| PUT | `/{id}` | Авторизований |
| DELETE | `/{id}` | Авторизований |
| GET | `/by-contract/{contractId}` | Публічний |
| GET | `/milestone-status-enums` | Авторизований |
| GET | `/status-freelancer-enums` | Авторизований |
| GET | `/status-employer-enums` | Авторизований |
| PUT | `/status/{id}/freelancer` | Freelancer only |
| PUT | `/status/{id}/employer` | Employer only |

---

### **MessageController** `/message`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/{id}` | Авторизований |
| POST | `/` | Авторизований |
| PUT | `/{id}` | Авторизований |
| DELETE | `/{id}` | Авторизований |
| POST | `/without-contract` | Авторизований |
| GET | `/by-user` | Авторизований |
| GET | `/by-contract/{contractId}` | Авторизований |

---

### **ReviewController** `/review`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/{id}` | Авторизований |
| POST | `/` | AnyAuthenticated |
| PUT | `/{id}` | AnyAuthenticated |
| DELETE | `/{id}` | AnyAuthenticated |
| GET | `/by-reviewed-user/{email}` | AnyAuthenticated |
| GET | `/average-rating/{email}` | AnyAuthenticated |
| GET | `/by-user` | AnyAuthenticated |

---

### **NotificationController** `/notification`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/is-not-read` | Авторизований |
| GET | `/paginated` | Авторизований |
| GET | `/filtered` | Авторизований |
| GET | `/type-employer-enums` | Авторизований |
| GET | `/type-freelancer-enums` | Авторизований |
| PATCH | `/{id}/toggle-read` | Авторизований |
| PATCH | `/read-all` | Авторизований |

---

### **DisputeController** `/dispute`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/` | Admin / Moderator |
| GET | `/{id}` | Admin / Moderator |
| GET | `/by-user` | Авторизований |
| POST | `/` | Авторизований |
| DELETE | `/{id}` | Admin / Moderator |
| GET | `/status-moderator-enums` | Admin / Moderator |
| PUT | `/{id}/status` | Admin / Moderator |

---

### **DisputeResolutionController** `/disputeresolution`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/` | Admin / Moderator |
| GET | `/{id}` | Admin / Moderator |
| POST | `/` | Admin / Moderator |
| DELETE | `/{id}` | Admin / Moderator |

---

### **FreelancerPortfolioController** `/freelancerportfolio`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/{id}` | Freelancer only |
| POST | `/` | Freelancer only |
| PUT | `/{id}` | Freelancer only |
| DELETE | `/{id}` | Freelancer only |
| GET | `/get-by-freelancer/{freelancerId}` | Публічний |

---

### **CategoryController** `/category`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/` | Публічний |
| GET | `/{id}` | Публічний |
| GET | `/paginated` | Публічний |
| POST | `/` | Admin only |
| PUT | `/{id}` | Admin only |
| DELETE | `/{id}` | Admin only |

---

### **SkillController** `/skill`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/` | Публічний |
| GET | `/{id}` | Публічний |
| GET | `/paginated` | Публічний |
| POST | `/` | Admin only |
| PUT | `/{id}` | Admin only |
| DELETE | `/{id}` | Admin only |

---

### **CountryController** `/country`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/` | Публічний |
| GET | `/{id}` | Публічний |
| GET | `/paginated` | Публічний |
| POST | `/` | Admin only |
| PUT | `/{id}` | Admin only |
| DELETE | `/{id}` | Admin only |

---

### **LanguageController** `/language`
| Метод | Endpoint | Доступ |
|---|---|---|
| GET | `/` | Публічний |
| GET | `/{id}` | Публічний |
| GET | `/paginated` | Публічний |
| POST | `/` | Admin only |
| PUT | `/{id}` | Admin only |
| DELETE | `/{id}` | Admin only |

---

## 📡 SignalR Hub

### `NotificationHub` — `/notifications`
Push-only хаб для сповіщень у реальному часі. Сервер надсилає подію `ReceiveNotification` клієнту через `IHubContext<NotificationHub>`.

Ідентифікація користувача — через JWT claim `id` (User Guid), що реалізовано в `NotificationUserIdProvider`.

**Типи сповіщень для роботодавця:** `NewBidReceived`, `NewMessage`, `DisputeOpened`, `ReviewLeft`, `SystemAnnouncement`, `ProjectDeadlineReminder`

**Типи сповіщень для фрілансера:** `NewMessage`, `MilestoneApproved`, `MilestoneRejected`, `ContractCreated`, `PaymentReceived`, `DisputeOpened`, `ReviewLeft`, `SystemAnnouncement`, `ProjectDeadlineReminder`

---

## 🏗️ Архітектура

### Clean Architecture (4 шари)
- **Domain** — чисті моделі та enums, без залежностей
- **DAL** — EF Core DbContext, міграції, репозиторії
- **BLL** — CQRS, сервіси, маппінг, валідація, хаби
- **API** — контролери, middleware, конфігурація

### CQRS з MediatR
- **Commands** — зміна стану (Create, Update, Delete)
- **Queries** — читання даних (GetAll, GetById, GetFiltered, GetPaginated)
- **Generic CRUD** — базові handlers для типових операцій
- **ValidationBehaviour** — pipeline behavior для автоматичної валідації FluentValidation перед кожним запитом

### Repository Pattern
Кожна сутність має власний інтерфейс репозиторію в BLL, реалізований у DAL.

---

## 🛡️ Middleware

### `MiddlewareExceptionsHandling`
Глобальна обробка винятків:
| Виняток | HTTP статус |
|---|---|
| `SecurityTokenException` | `426 Upgrade Required` |
| `ValidationException` | `400 Bad Request` |
| Інші `Exception` | `500 Internal Server Error` |

---

## 🧪 Тестування

Покриття через **Integration Tests** із `WebApplicationFactory`:

| Файл тестів | Що тестує |
|---|---|
| `AccountControllerTests` | Sign-up, Sign-in |
| `ProjectControllerTests` | CRUD, фільтрація |
| `BidControllerTests` | Заявки фрілансерів |
| `ContractControllerTests` | Контракти |
| `ContractMilestones/` | Майлстоуни контрактів |
| `DisputeControllerTests` | Спори |
| `ReviewControllerTests` | Відгуки |
| `MessageControllerTests` | Повідомлення |
| ...та інші | Повне API-покриття |

---

## 🐳 Docker

### Запуск через Docker Compose (рекомендовано)

**1. Скопіюйте файл зі змінними оточення:**
```bash
cp .env.example .env
```

**2. Запустіть сервіси:**
```bash
docker compose up -d
```

Запустяться два контейнери:
- `freelance-db` — PostgreSQL 16 (порт `5432`)
- `freelance-api` — API сервер (порт `8080`)

API буде доступне за адресою: **http://localhost:8080/swagger**

### Змінні оточення (`.env`)
| Змінна | Опис |
|---|---|
| `GOOGLE_CLIENT_ID` | Client ID Google OAuth 2.0 |
| `JWT_KEY` | Секретний ключ для підпису JWT |
| `JWT_ISSUER` | Видавець токена |
| `JWT_AUDIENCE` | Аудиторія токена |

### Docker Image
Образ автоматично публікується до **GitHub Container Registry**:
```
ghcr.io/andromaan/freelanceback:latest
```

---

## ⚙️ Локальний запуск (без Docker)

**Вимоги:** .NET 8 SDK, PostgreSQL 16

```bash
# Клонування репозиторію
git clone https://github.com/andromaan/FreelanceBack.git
cd FreelanceBack

# Відновлення залежностей
dotnet restore

# Налаштування рядка підключення в src/API/appsettings.Development.json
# "Default": "Server=localhost;Port=5432;Database=freelance-db;User Id=postgres;Password=postgres;"

# Запуск проекту
dotnet run --project src/API

# Swagger UI
# http://localhost:{port}/swagger
```

---

## 🔄 CI/CD

GitHub Actions пайплайн (`.github/workflows/ci-cd.yml`):

| Job | Тригер | Дії |
|---|---|---|
| `Build & Test` | push / PR → `main` | `dotnet restore` → `build` → `dotnet test` → публікація TRX звіту |
| `Build & Push Docker Image` | push → `main` | Логін до GHCR → build image → push `latest` + `sha-*` тег |

---

## 📝 Ліцензія

Цей проект є приватним і належить [@andromaan](https://github.com/andromaan).
