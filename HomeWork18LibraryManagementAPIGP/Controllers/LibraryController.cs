using Application.Interfaces;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork18LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        private readonly AppDbContext _context;

        public LibraryController(ILibraryService libraryService, AppDbContext context)
        {
            _libraryService = libraryService;
            _context = context;
        }

        [HttpGet("all-data")]
        public async Task<IActionResult> GetAllData()
        {
            var data = new
            {
                Books = await _context.Books.ToListAsync(),
                Readers = await _context.Readers.ToListAsync(),
                Loans = await _context.Loans
                    .Include(l => l.Book)
                    .Include(l => l.Reader)
                    .ToListAsync()
            };

            return Ok(data);
        }

        [HttpPost("issue")]
        public async Task<IActionResult> IssueBook(int readerId, int bookId)
        {
            try { return Ok(await _libraryService.IssueBookAsync(readerId, bookId)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook(int loanId, BookCondition condition)
        {
            try { return Ok(await _libraryService.ReturnBookAsync(loanId, condition)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            return Ok(new
            {
                TotalBooks = await _context.Books.SumAsync(b => b.TotalQty),
                AvailableBooks = await _context.Books.SumAsync(b => b.AvailableQty),
                CurrentlyBorrowed = await _context.Loans.CountAsync(l => l.ReturnDate == null),
                OverdueReturns = await _context.Loans.CountAsync(l => l.ReturnDate == null && l.DueDate < DateTime.Now),
                BlockedReaders = await _context.Readers.CountAsync(r => r.Status == ReaderStatus.Blocked)
            });
        }

        // 1. წიგნების ძებნა (პუნქტი 7)
        [HttpGet("search-books")]
        public async Task<IActionResult> SearchBooks(string? title, string? author, string? category, string? isbn)
        {
            var results = await _context.Books.AsQueryable()
                .Where(b => (title == null || b.Title.Contains(title)) &&
                            (author == null || b.Author.Contains(author)) &&
                            (category == null || b.Category == category) &&
                            (isbn == null || b.ISBN == isbn))
                .ToListAsync();
            return Ok(results);
        }

        // 2. მკითხველების ძებნა სტატუსით (პუნქტი 7)
        [HttpGet("search-readers")]
        public async Task<IActionResult> SearchReaders(ReaderStatus status)
        {
            var readers = await _context.Readers.Where(r => r.Status == status).ToListAsync();
            return Ok(readers);
        }

        // 3. სრული სტატისტიკა (პუნქტი 8)
        [HttpGet("full-statistics")]
        public async Task<IActionResult> GetFullStatistics()
        {
            var now = DateTime.Now;
            var loans = await _context.Loans.Include(l => l.Book).Include(l => l.Reader).ToListAsync();

            var stats = new
            {
                TotalBooks = await _context.Books.SumAsync(b => b.TotalQty),
                AvailableBooks = await _context.Books.SumAsync(b => b.AvailableQty),
                CurrentlyBorrowed = loans.Count(l => l.ReturnDate == null),
                OverdueReturns = loans.Count(l => l.ReturnDate == null && l.DueDate < now),
                BlockedReaders = await _context.Readers.CountAsync(r => r.Status == ReaderStatus.Blocked),

                // ყველაზე ხშირად გაცემული წიგნი
                TopBook = loans.GroupBy(l => l.BookId)
                              .OrderByDescending(g => g.Count())
                              .Select(g => g.First().Book.Title).FirstOrDefault(),

                // ყველაზე აქტიური მკითხველი
                TopReader = loans.GroupBy(l => l.ReaderId)
                                .OrderByDescending(g => g.Count())
                                .Select(g => g.First().Reader.FirstName + " " + g.First().Reader.LastName).FirstOrDefault()
            };
            return Ok(stats);
        }
    }
}