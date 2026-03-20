================================================================
  StudentCRUD - Windows Forms + WCF + Entity Framework
  .NET 4.8 | სრული სახელმძღვანელო
================================================================

================================================================
 პროექტის სტრუქტურა
================================================================

StudentCRUD/
├── StudentCRUD.sln                          ← Solution (Visual Studio-ს ხსნის)
│
├── StudentCRUD.Service/                     ← WCF სერვისი + EF (ბაზა)
│   ├── Models/
│   │   └── Student.cs                       ← სტუდენტის მოდელი (ბაზის ცხრილი)
│   ├── Data/
│   │   └── SchoolDbContext.cs               ← Entity Framework კავშირი
│   ├── IStudentService.cs                   ← WCF Interface (მენიუ)
│   ├── StudentService.cs                    ← WCF Implementation (ლოგიკა)
│   ├── Program.cs                           ← სერვისის გამშვები
│   └── App.config                           ← Connection string + WCF config
│
└── StudentCRUD.Client/                      ← Windows Forms UI
    ├── Form1.cs                             ← ღილაკების ლოგიკა
    ├── Form1.Designer.cs                    ← UI კომპონენტები
    └── App.config                           ← სერვისის მისამართი


================================================================
 ნაბიჯ-ნაბიჯ: Visual Studio-ში გახსნა და კონფიგურაცია
================================================================

── ᲜᲐᲑᲘᲯᲘ 1: Solution გახსნა ──────────────────────────────────

1. Visual Studio გახსენით
2. File → Open → Project/Solution
3. StudentCRUD.sln ფაილი შეარჩიეთ
4. OK


── ᲜᲐᲑᲘᲯᲘ 2: EntityFramework NuGet Package ────────────────────

Service პროექტს სჭირდება EntityFramework package:

1. StudentCRUD.Service-ზე Right-click
2. Manage NuGet Packages...
3. Browse ჩანართი
4. მოძებნეთ: EntityFramework
5. Version: 6.4.4 შეარჩიეთ
6. Install
7. Accept License → OK


── ᲜᲐᲑᲘᲯᲘ 3: Migration - ბაზის შექმნა ────────────────────────

Tools → NuGet Package Manager → Package Manager Console

Default Project-ად StudentCRUD.Service შეარჩიეთ! შემდეგ:

  PM> Enable-Migrations
  PM> Add-Migration InitialCreate
  PM> Update-Database

  ✓ ეს ბრძანებები ავტომატურად შექმნიან:
    - SchoolDB ბაზას SQL LocalDB-ში
    - Students ცხრილს Student კლასის მიხედვით


── ᲜᲐᲑᲘᲯᲘ 4: Service Reference-ის დამატება ───────────────────

⚠️ ᲛᲜᲘᲨᲕᲜᲔᲚᲝᲕᲐᲜᲘ: სერვისი გაშვებული უნდა იყოს!

  4a. StudentCRUD.Service პროექტი Set as Startup Project
  4b. F5 (გაუშვით) - Console-ში გამოჩნდება "Service RUNNING"
  4c. სერვისი გათიშეთ (Enter)

  4d. StudentCRUD.Client-ზე Right-click
  4e. Add → Service Reference...
  4f. Address: http://localhost:8080/StudentService
  4g. Go ღილაკი (სერვისი გაშვებული უნდა იყოს!)
  4h. Namespace: StudentServiceRef
  4i. OK

  Visual Studio ავტომატურად შექმნის proxy კლასს!


── ᲜᲐᲑᲘᲯᲘ 5: Multiple Startup Projects ───────────────────────

ორივე პროექტი ერთდროულად გაშვებისთვის:

1. Solution-ზე Right-click → Properties
2. Common Properties → Startup Project
3. Multiple startup projects შეარჩიეთ
4. StudentCRUD.Service → Action: Start
5. StudentCRUD.Client → Action: Start
6. OK

ახლა F5-ზე ორივე გაიხსნება!


── ᲜᲐᲑᲘᲯᲘ 6: გაშვება ─────────────────────────────────────────

F5 → ჯერ Console (სერვისი), შემდეგ Windows Forms გაიხსნება


================================================================
 პროგრამის გამოყენება
================================================================

➕ ᲡᲢᲣᲓᲔᲜᲢᲘᲡ ᲓᲐᲛᲐᲢᲔᲑᲐ:
   1. მარჯვენა პანელში შეავსეთ ველები
   2. "დამატება" ღილაკი

✎  ᲡᲢᲣᲓᲔᲜᲢᲘᲡ ᲠᲔᲓᲐᲥᲢᲘᲠᲔᲑᲐ:
   1. სიაში სტუდენტზე დააკლიკეთ (ველები ავტომატურად შეივსება)
   2. შეცვალეთ სასურველი ველები
   3. "განახლება" ღილაკი

✖  ᲡᲢᲣᲓᲔᲜᲢᲘᲡ ᲬᲐᲨᲚᲐ:
   1. სიაში სტუდენტი შეარჩიეთ
   2. "წაშლა" ღილაკი
   3. დადასტურება

↺  ᲒᲐᲡᲣᲤᲗᲐᲕᲔᲑᲐ:
   ველების გაწმენდა და Add რეჟიმზე დაბრუნება


================================================================
 ხშირი შეცდომები
================================================================

❌ "EndpointNotFoundException"
   → სერვისი გაშვებული არ არის. Console პროგრამა გაუშვით.

❌ "Could not connect to database"
   → Update-Database არ გაუშვია. PMC-ში გაუშვით.

❌ "Could not find type StudentServiceRef.IStudentService"
   → Service Reference არ დაემატა. ნაბიჯი 4 გაიმეორეთ.

❌ Form1.cs build error - StudentServiceClient not found
   → Service Reference-ის namespace: StudentServiceRef
   → Form1.cs-ში: using StudentCRUD.Client.StudentServiceRef;


================================================================
 კოდის განმარტება - საკვანძო კონცეფციები
================================================================

▶ Entity Framework (EF)
  C# კლასებს → SQL ცხრილებად გარდაქმნის
  SQL კოდის წერა არ გჭირდება!
  
  Student კლასი = Students ცხრილი ბაზაში
  db.Students.Add()     = INSERT INTO Students
  db.Students.ToList()  = SELECT * FROM Students
  db.Students.Find(id)  = SELECT WHERE Id = ?
  db.Students.Remove()  = DELETE FROM Students WHERE Id = ?
  db.SaveChanges()      = ⚠️ ბაზაში შენახვა (COMMIT)

▶ WCF (Windows Communication Foundation)
  სერვისი = სერვერი, კლიენტი ქსელით ურთიერთობს
  
  [ServiceContract]   = Interface WCF-ის სერვისია
  [OperationContract] = ეს მეთოდი ქსელით ხელმისაწვდომია
  [DataContract]      = ეს კლასი ქსელით გაიგზავნება
  [DataMember]        = ეს property-ც გაიგზავნება
  
  ServiceHost = სერვისის "გამავალი კარი" (listener)
  proxy       = კლიენტი სერვისის ადგილობრივი ასლი

▶ using ბლოკი
  using (var db = new SchoolDbContext()) { ... }
  ბლოკის დახურვისას db ავტომატურად იხურება!
  კავშირი ღია არ რჩება = სერვერი არ გადაიტვირთება

▶ CRUD
  C = Create = Add()    + SaveChanges()
  R = Read   = Find()   ან ToList()
  U = Update = Find()   + property შეცვლა + SaveChanges()
  D = Delete = Remove() + SaveChanges()

================================================================
