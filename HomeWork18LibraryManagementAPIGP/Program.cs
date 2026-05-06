using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// სერვისების რეგისტრაცია
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// მონაცემთა ბაზის კონფიგურაცია (In-Memory)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("LibraryDb"));

// Dependency Injection-ის დაკავშირება
builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddScoped<ILibraryService, LibraryService>();

var app = builder.Build();

// მონაცემების ავტომატური შევსება (Data Seeding)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // 1. დავამატოთ 5 მკითხველი
    if (!context.Readers.Any())
    {
        context.Readers.AddRange(
            new Domain.Entities.Reader { FirstName = "გიორგი", LastName = "ბერიძე", PersonalNumber = "01010101011", Email = "giorgi@gmail.com", Status = Domain.Enums.ReaderStatus.Active, RegistrationDate = DateTime.Now },
            new Domain.Entities.Reader { FirstName = "ნინო", LastName = "კაპანაძე", PersonalNumber = "01010101012", Email = "nino@gmail.com", Status = Domain.Enums.ReaderStatus.Active, RegistrationDate = DateTime.Now },
            new Domain.Entities.Reader { FirstName = "დავით", LastName = "გელაშვილი", PersonalNumber = "01010101013", Email = "dato@gmail.com", Status = Domain.Enums.ReaderStatus.Active, RegistrationDate = DateTime.Now },
            new Domain.Entities.Reader { FirstName = "მარიამ", LastName = "ლომიძე", PersonalNumber = "01010101014", Email = "mari@gmail.com", Status = Domain.Enums.ReaderStatus.Blocked, RegistrationDate = DateTime.Now },
            new Domain.Entities.Reader { FirstName = "ლუკა", LastName = "კვარაცხელია", PersonalNumber = "01010101015", Email = "luka@gmail.com", Status = Domain.Enums.ReaderStatus.Active, RegistrationDate = DateTime.Now }
        );
    }

    // 2. დავამატოთ 5 წიგნი
    if (!context.Books.Any())
    {
        context.Books.AddRange(
            new Domain.Entities.Book { Title = "Clean Architecture", Author = "Robert C. Martin", ISBN = "9780134494166", Category = "Tech", TotalQty = 5, AvailableQty = 5, PublishYear = 2017 },
            new Domain.Entities.Book { Title = "The Hobbit", Author = "J.R.R. Tolkien", ISBN = "9780547928227", Category = "Fantasy", TotalQty = 3, AvailableQty = 3, PublishYear = 1937 },
            new Domain.Entities.Book { Title = "C# in Depth", Author = "Jon Skeet", ISBN = "9781617294532", Category = "Tech", TotalQty = 2, AvailableQty = 2, PublishYear = 2019 },
            new Domain.Entities.Book { Title = "1984", Author = "George Orwell", ISBN = "9780451524935", Category = "Dystopian", TotalQty = 10, AvailableQty = 10, PublishYear = 1949 },
            new Domain.Entities.Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "9780743273565", Category = "Classic", TotalQty = 4, AvailableQty = 4, PublishYear = 1925 }
        );
    }

    context.SaveChanges();
}

// HTTP pipeline-ის გამართვა
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();