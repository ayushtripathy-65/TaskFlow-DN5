# TaskFlow - Cognizant DN 5.0 Deep Skilling

**Candidate:** Ayush Tripathy  
**Track:** DotNet FSE Angular  
**GitHub Repo:** https://github.com/ayushtripathy-65/TaskFlow-DN5

---

## Weekly Exercise Mapping

### Week 1: Engineering Concepts + Programming Languages

| Exercise | Module | File/Folder |
|----------|--------|-------------|
| Exercise 1: Singleton Pattern | Design Patterns | design-patterns/SingletonPattern.cs |
| Exercise 2: Factory Method Pattern | Design Patterns | design-patterns/FactoryPattern.cs |
| Exercise 2: E-commerce Search Function | DSA | dsa/SearchFunction.cs |
| Exercise 7: Financial Forecasting | DSA | dsa/FinancialForecasting.cs |
| Exercise 1: Window Functions | Advanced SQL | sql-exercises/01-window-function |
| Exercise 1: Create Stored Procedure | Advanced SQL | sql-exercises/02-stored-proc-create.sql |
| Exercise 5: Return Data from Stored Procedure | Advanced SQL | sql-exercises/03-stored-proc-return.sql |
| NUnit-Handson | NUnit | TaskFlow.Tests/UsersControllerTests.cs |
| Moq-Handson | Moq | TaskFlow.Tests/MoqTests.cs |

### Week 2: Products and Frameworks

| Exercise | Module | File/Folder |
|----------|--------|-------------|
| Lab 1: ORM with Retail Inventory | EF Core | TaskFlow.Core/Entities/ |
| Lab 2: Setting Up DbContext | EF Core | TaskFlow.API/Data/TaskFlowDbContext.cs |
| Lab 3: EF Core CLI Migrations | EF Core | TaskFlow.API/Migrations/ |
| Lab 4: Inserting Initial Data | EF Core | OnModelCreating seed data |
| Lab 5: Retrieving Data | EF Core | Controllers with LINQ |
| WebApi_Handson 1-6 | Web API | TaskFlow.API/Controllers/ |

### Week 3: Advanced API + Frontend

| Exercise | Module | File/Folder |
|----------|--------|-------------|
| Question 1: JWT Authentication | Microservices | AuthController.cs + JwtService.cs |
| Angular Hands-on | Angular | TaskFlow.Angular/ |

### Week 4-7: Platforms + DevOps

| Exercise | Module | File/Folder |
|----------|--------|-------------|
| Git-HOL 1-5 | Git | git-logs/git-commands.txt |
| Docker | Containerization | Dockerfile |
| Docker Compose | Containerization | docker-compose.yml |

---

## Project Structure

- TaskFlow.API/ - .NET 8 Web API
- TaskFlow.Core/ - Domain layer
- TaskFlow.Tests/ - NUnit + Moq tests
- TaskFlow.Angular/ - Angular 21 frontend
- design-patterns/ - Singleton, Factory
- dsa/ - Search, Forecasting
- sql-exercises/ - Window funcs, Stored procs
- git-logs/ - Git exercise documentation
- Dockerfile - Docker image
- docker-compose.yml - Docker orchestration

---

## Technologies Used

- Backend: .NET 8, ASP.NET Core Web API, EF Core 8, SQLite, JWT Authentication
- Frontend: Angular 21, Angular Material, RxJS
- Testing: NUnit, Moq
- DevOps: Docker, Docker Compose

---

## How to Run

Backend:
  cd TaskFlow.API
  dotnet run

Frontend:
  cd TaskFlow.Angular
  ng serve

---

## Assessment Info

- Program: Cognizant Digital Nurture 5.0 Deep Skilling
- Track: DotNet FSE Angular
- Final KBA: July 28, 2026
- Status: All 27 mandatory exercises completed
