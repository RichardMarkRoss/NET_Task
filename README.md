# UserGroup Solution — Tasks 1, 2 & 3

This project implements **Task 1 (Database + EF Core Code-First)**, **Task 2 (Web API)**, and begins **Task 3 (Web Interface)** from the assignment.

---

## ✅ What Has Been Done
1. **Task 1 — Database (Code-First EF Core + SQL Server)**
   - Domain entities: `User`, `Group`, `Permission`, `UserGroup`, `GroupPermission`.
   - Relationships configured:
     - Users ↔ Groups (many-to-many via `UserGroup`).
     - Groups ↔ Permissions (many-to-many via `GroupPermission`).
   - Constraints and indexes added.
   - Seed data added (Admin/Users groups and some permissions).
   - EF Core migrations created and applied to SQL Server.

2. **Docker Setup**
   - Added `docker-compose.yml` for:
     - SQL Server container (`db` service).
     - API container (`api` service).
   - Dockerfile in `src/Api` builds and publishes the .NET API.

3. **Task 2 — Web API**
   - API project (`UserGroup.Api`) scaffolding created.
   - Connected to the same `AppDbContext`.
   - Swagger configured for testing endpoints (**currently not showing endpoints — blocker**).

4. **Task 3 — Web Interface**
   - ASP.NET Core MVC/Minimal UI scaffolding started under `UserGroup.Api`.
   - UI will consume the Web API for adding, editing, and deleting users.
   - Visual design is minimal as per requirements.

---

## ⏳ Current Blocker
- Swagger UI loads but **does not display the controller endpoints**.  
  Likely causes:
  - Controllers not properly registered in Program.cs.
  - Missing `[ApiController]` / `[Route("api/[controller]")]` attributes.
  - API project not building with referenced Infrastructure project.

---

## ⏳ What Still Needs To Be Done
- Fix Swagger endpoint issue so controllers appear.
- Finish API Controllers:
  - `UsersController` → Add, Remove, Update Users.
  - Endpoints for:
    - Total user count.
    - Number of users per group.
- Complete Web Interface (Task 3) to consume API endpoints.
- Unit and Integration Tests (Extra points).
- Optional: Add `IsActive` field to `User` if time allows.

---

## ▶️ How to Run the Project

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/).
- [Docker Desktop](https://www.docker.com/products/docker-desktop/).

### Steps
1. Clone the repo:
   ```bash
   git clone <your-repo-url>
   cd usergroup-sln
   ```

2. Build & run containers with Docker:
   ```bash
   docker compose up --build
   ```

3. The following services will start:
   - SQL Server (on `localhost:1433`).
   - Web API (on `localhost:8080`).

4. Test API via Swagger (when fixed):
   - Open: [http://localhost:8080/swagger](http://localhost:8080/swagger)

---

## 🔧 Useful Commands

### Apply EF Migrations manually
```bash
dotnet ef migrations add InitialCreate -p src/Infrastructure -s src/Migrator -o Data/Migrations
dotnet run --project src/Migrator
```

### Stop and remove containers
```bash
docker compose down
```

### Rebuild without cache
```bash
docker compose build --no-cache api
```

---

## 📌 Notes
- Seed data currently uses fixed GUIDs to avoid EFCore model drift.
- If migrations get stuck, run:
  ```bash
  dotnet ef migrations remove -p src/Infrastructure -s src/Migrator
  ```
- Next steps are to **fix Swagger**, finish Task 2 controllers, and complete Task 3 web interface.
