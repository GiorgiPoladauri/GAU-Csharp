# Medical Billing API

A RESTful API built with ASP.NET Core 8, Entity Framework Core, SQL Server LocalDB, and JWT Authentication using Clean/Layered Architecture.

---

## Setup on a New Machine

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 with the **"ASP.NET and web development"** workload installed
  - This automatically includes SQL Server LocalDB — no separate install needed

### Steps

**1. Open the solution**
Double-click `MedicalBilling.sln` or open it from Visual Studio → File → Open → Project/Solution.

**2. Set the startup project**
In Solution Explorer, right-click `MedicalBilling.Presentation` → **Set as Startup Project**.

**3. Restore NuGet packages**
Visual Studio does this automatically. If not: right-click the solution → **Restore NuGet Packages**.

**4. Open Package Manager Console**
Tools → NuGet Package Manager → Package Manager Console.
Make sure the default project dropdown at the top of the console is set to `MedicalBilling.Infrastructure`.

**5. Generate the migration**

```
dotnet ef migrations add InitialCreate --project MedicalBilling.Infrastructure --startup-project MedicalBilling.Presentation --context HealthDbContext --output-dir Data/Migrations
```

**6. Apply the migration (creates the database and all tables)**

```
dotnet ef database update --project MedicalBilling.Infrastructure --startup-project MedicalBilling.Presentation --context HealthDbContext
```

You should see multiple `CREATE TABLE` lines in the output — Doctors, Patients, Users, Visits.

**7. Run the project**
Press **F5** or Ctrl+F5. Swagger UI opens at `http://localhost:5000`.

On first run the app automatically creates a default admin account:

- **Username:** `admin`
- **Password:** `Admin@123`

---

## Resetting the Database

If you need a clean slate at any point:

```
dotnet ef database drop --project MedicalBilling.Infrastructure --startup-project MedicalBilling.Presentation --context HealthDbContext --force
dotnet ef database update --project MedicalBilling.Infrastructure --startup-project MedicalBilling.Presentation --context HealthDbContext
```

---

## Testing Every Requirement

Open Swagger UI at `http://localhost:5000`. Click **Authorize** (top right), paste your JWT token in the format `Bearer {token}` after logging in.

---

### Step 1 — Get a JWT Token (required for everything else)

**Endpoint:** `POST /api/auth/login`

Request body:

```json
{
  "username": "admin",
  "password": "Admin@123"
}
```

Copy the `token` value from the response. Click **Authorize** in Swagger and paste it. You are now authenticated as admin.

---

### Step 2 — Tables and Relationships (5 pts)

Verify the tables exist by creating records with relationships.

**Create a Doctor:**
`POST /api/doctors`

```json
{
  "fullName": "Dr. John Smith",
  "specialization": "Cardiology"
}
```

Note the returned `id` (should be 1).

**Create a Patient:**
`POST /api/patients`

```json
{
  "fullName": "Jane Doe",
  "birthDate": "1990-05-15"
}
```

Note the returned `id` (should be 1).

**Create a Visit (links Patient → Doctor):**
`POST /api/visits`

```json
{
  "patientId": 1,
  "doctorId": 1,
  "visitDate": "2024-06-10",
  "fee": 150.0
}
```

✅ Visit correctly references both Patient and Doctor via foreign keys.

---

### Step 3 — CRUD Operations (8 pts)

#### Visits — Full CRUD

| Operation | Endpoint               | Body                                                                   |
| --------- | ---------------------- | ---------------------------------------------------------------------- |
| Create    | `POST /api/visits`     | `{ "patientId":1, "doctorId":1, "visitDate":"2024-06-11", "fee":200 }` |
| Read all  | `GET /api/visits`      | —                                                                      |
| Read one  | `GET /api/visits/1`    | —                                                                      |
| Update    | `PUT /api/visits/1`    | `{ "patientId":1, "doctorId":1, "visitDate":"2024-06-11", "fee":250 }` |
| Delete    | `DELETE /api/visits/1` | —                                                                      |

#### Patients — Full CRUD

| Operation | Endpoint                 | Body                                                          |
| --------- | ------------------------ | ------------------------------------------------------------- |
| Create    | `POST /api/patients`     | `{ "fullName":"John Doe", "birthDate":"1985-03-20" }`         |
| Read all  | `GET /api/patients`      | —                                                             |
| Read one  | `GET /api/patients/1`    | —                                                             |
| Update    | `PUT /api/patients/1`    | `{ "fullName":"John Doe Updated", "birthDate":"1985-03-20" }` |
| Delete    | `DELETE /api/patients/2` | —                                                             |

#### Doctors — Read and Create ONLY (no Update, no Delete)

| Operation | Endpoint             | Body                                                          |
| --------- | -------------------- | ------------------------------------------------------------- |
| Create    | `POST /api/doctors`  | `{ "fullName":"Dr. Anna Lee", "specialization":"Neurology" }` |
| Read all  | `GET /api/doctors`   | —                                                             |
| Read one  | `GET /api/doctors/1` | —                                                             |

✅ Confirm there are no `PUT /api/doctors` or `DELETE /api/doctors` endpoints in Swagger.

---

### Step 4 — Pagination (4 pts)

Create several visits first (at least 5, on different dates for the same patient you will need different patients).

**Test pagination:**
`GET /api/visits?pageNumber=1&pageSize=2`

Expected response shape:

```json
{
  "items": [ ... ],
  "totalCount": 5,
  "totalPages": 3,
  "currentPage": 1,
  "pageSize": 2
}
```

Test next page:
`GET /api/visits?pageNumber=2&pageSize=2`

✅ `currentPage` changes, different items are returned.

---

### Step 5 — Filtering and Sorting (4 pts)

**Filter by doctorId:**
`GET /api/visits?doctorId=1`

**Filter by date range:**
`GET /api/visits?visitDateFrom=2024-01-01&visitDateTo=2024-12-31`

**Filter by fee range:**
`GET /api/visits?minFee=100&maxFee=300`

**Sort by Fee ascending:**
`GET /api/visits?sortBy=Fee&sortDirection=asc`

**Sort by VisitDate descending:**
`GET /api/visits?sortBy=VisitDate&sortDirection=desc`

**Combined filter + sort + pagination:**
`GET /api/visits?doctorId=1&minFee=50&sortBy=Fee&sortDirection=desc&pageNumber=1&pageSize=5`

✅ Each filter narrows results correctly, sorting changes the order.

---

### Step 6 — Authorization (5 pts)

#### Test admin role

Login as admin (done in Step 1). Admin can access everything — all endpoints return 200.

#### Test doctor role

**First create a second doctor:**
`POST /api/doctors`

```json
{ "fullName": "Dr. Sarah Connor", "specialization": "Surgery" }
```

Note the id (e.g. 2).

**Create a visit for doctor 2:**
`POST /api/visits`

```json
{ "patientId": 1, "doctorId": 2, "visitDate": "2024-07-01", "fee": 300 }
```

**Register a user account linked to doctor 1:**
`POST /api/auth/register`

```json
{
  "username": "drsmith",
  "password": "Doctor@123",
  "role": "doctor",
  "doctorId": 1
}
```

**Login as drsmith:**
`POST /api/auth/login`

```json
{ "username": "drsmith", "password": "Doctor@123" }
```

Copy the new token and re-authorize in Swagger with it.

**Now test restrictions:**

`GET /api/visits` — ✅ Returns ONLY visits belonging to doctor 1, not doctor 2's visits.

`GET /api/visits/{id}` where that visit belongs to doctor 2 — ✅ Returns **403 Forbidden**.

`POST /api/visits` — ✅ Returns **403 Forbidden** (doctors cannot create visits).

`DELETE /api/visits/1` — ✅ Returns **403 Forbidden**.

`POST /api/patients` — ✅ Returns **403 Forbidden**.

`DELETE /api/patients/1` — ✅ Returns **403 Forbidden**.

**Test unauthenticated access:**
Click **Authorize** in Swagger → **Logout**. Then call any endpoint.
✅ Returns **401 Unauthorized**.

---

### Step 7 — Additional Logic and Validation (8 pts)

#### Fee must be > 0 and < 1000

Re-authorize as admin. Try:

`POST /api/visits`

```json
{ "patientId": 1, "doctorId": 1, "visitDate": "2024-08-01", "fee": 0 }
```

✅ Returns **400 Bad Request** — "Visit fee must be greater than 0."

`POST /api/visits`

```json
{ "patientId": 1, "doctorId": 1, "visitDate": "2024-08-01", "fee": 1000 }
```

✅ Returns **400 Bad Request** — "Visit fee must be less than 1000."

`POST /api/visits`

```json
{ "patientId": 1, "doctorId": 1, "visitDate": "2024-08-01", "fee": -50 }
```

✅ Returns **400 Bad Request**.

#### One visit per patient per day

`POST /api/visits`

```json
{ "patientId": 1, "doctorId": 1, "visitDate": "2024-06-10", "fee": 100 }
```

(Use a date that patient 1 already has a visit on)
✅ Returns **409 Conflict** — "Patient already has a visit on that date."

#### Doctor must have specialization

This is enforced at doctor creation — you cannot create a doctor without specialization:
`POST /api/doctors`

```json
{ "fullName": "Dr. Nobody", "specialization": "" }
```

✅ Returns **400 Bad Request** — "Specialization is required."

#### Total billing per patient (TotalPaid)

`GET /api/patients/1/billing`

✅ Returns:

```json
{
  "patientId": 1,
  "patientFullName": "Jane Doe",
  "totalPaid": 450.0,
  "totalVisits": 3
}
```

`totalPaid` is the sum of all visit fees for that patient.

#### Doctor visit count (TotalVisitsByDoctor)

`GET /api/doctors/1/stats`

✅ Returns:

```json
{
  "doctorId": 1,
  "doctorFullName": "Dr. John Smith",
  "specialization": "Cardiology",
  "totalVisitsByDoctor": 3
}
```

---

### Step 8 — Validation at Service Layer

Open any service file in the project:

- `MedicalBilling.Application/Services/VisitService.cs`
- `MedicalBilling.Application/Services/PatientService.cs`
- `MedicalBilling.Application/Services/DoctorService.cs`

✅ All validation (`if (fee <= 0)`, duplicate date check, specialization check) is inside the service methods, not in the controllers. Controllers only call the service and return the result.

---

### Step 9 — Architecture Verification

Open each project and confirm the layer boundaries:

| Layer          | Project                         | Contains                                                                                        |
| -------------- | ------------------------------- | ----------------------------------------------------------------------------------------------- |
| Domain         | `MedicalBilling.Domain`         | `Patient`, `Doctor`, `Visit` entities; `IRepository<T>`, `IVisitRepository` interfaces          |
| Application    | `MedicalBilling.Application`    | DTOs, service interfaces, `VisitService`, `PatientService`, `DoctorService`, all business logic |
| Infrastructure | `MedicalBilling.Infrastructure` | `HealthDbContext`, `VisitRepository`, `DoctorRepository`, `TokenService`                        |
| Presentation   | `MedicalBilling.Presentation`   | Controllers, `ErrorHandlingMiddleware`, `JwtAuthMiddleware`, `Program.cs`                       |

✅ `Domain` has no references to any other project.
✅ `Application` only references `Domain`.
✅ `Infrastructure` references `Domain` and `Application`.
✅ `Presentation` references `Application` and `Infrastructure`.
✅ No business logic in controllers — they only call services and return results.

---

## Default Credentials

| Username | Password  | Role  |
| -------- | --------- | ----- |
| admin    | Admin@123 | admin |

Create doctor users via `POST /api/auth/register` after creating a doctor record.
