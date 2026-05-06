using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly DbContext _context;

        public LibraryService(DbContext context)
        {
            _context = context;
        }

        public async Task<Loan> IssueBookAsync(int readerId, int bookId)
        {
            var reader = await _context.Set<Reader>().FindAsync(readerId) ?? throw new Exception("Reader not found.");
            var book = await _context.Set<Book>().FindAsync(bookId) ?? throw new Exception("Book not found.");

            if (book.AvailableQty <= 0) throw new Exception("Book is not available.");
            if (reader.Status != ReaderStatus.Active) throw new Exception("Reader is blocked or cancelled.");

            var unreturnedBooksCount = await _context.Set<Loan>().CountAsync(l => l.ReaderId == readerId && l.ReturnDate == null);
            if (unreturnedBooksCount >= 3) throw new Exception("Reader already has 3 unreturned books.");

            var loan = new Loan
            {
                ReaderId = readerId,
                BookId = bookId,
                IssueDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14)
            };

            book.AvailableQty -= 1;
            _context.Set<Loan>().Add(loan);
            await _context.SaveChangesAsync();

            return loan;
        }

        public async Task<Loan> ReturnBookAsync(int loanId, BookCondition condition)
        {
            var loan = await _context.Set<Loan>().Include(l => l.Book).Include(l => l.Reader).FirstOrDefaultAsync(l => l.Id == loanId)
                ?? throw new Exception("Loan record not found.");

            if (loan.ReturnDate != null) throw new Exception("Book is already returned.");

            loan.ReturnDate = DateTime.Now;
            loan.ReturnCondition = condition;

            int lateDays = (DateTime.Now.Date - loan.DueDate.Date).Days;
            loan.PenaltyAmount = lateDays > 0 ? lateDays * 1m : 0m;

            if (condition == BookCondition.Good) loan.Book.AvailableQty += 1;
            else if (condition == BookCondition.Lost) loan.Book.TotalQty -= 1;

            await _context.SaveChangesAsync();
            await UpdateReaderStatusesAsync();

            return loan;
        }

        public async Task UpdateReaderStatusesAsync()
        {
            var activeReaders = await _context.Set<Reader>().Where(r => r.Status == ReaderStatus.Active).ToListAsync();

            foreach (var reader in activeReaders)
            {
                var userLoans = await _context.Set<Loan>().Where(l => l.ReaderId == reader.Id).ToListAsync();

                var totalPenalty = userLoans.Sum(l => l.PenaltyAmount);
                var lostBooks = userLoans.Count(l => l.ReturnCondition == BookCondition.Lost);
                var overdueUnreturned = userLoans.Count(l => l.ReturnDate == null && l.DueDate < DateTime.Now);

                if (totalPenalty > 20 || lostBooks >= 2 || overdueUnreturned > 3)
                {
                    reader.Status = ReaderStatus.Blocked;
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}