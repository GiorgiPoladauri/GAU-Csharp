using Domain.Entities;
using Domain.Enums;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILibraryService
    {
        Task<Loan> IssueBookAsync(int readerId, int bookId);
        Task<Loan> ReturnBookAsync(int loanId, BookCondition condition);
        Task UpdateReaderStatusesAsync();
    }
}