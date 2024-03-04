using ApiBiblioteca.Domain.Models;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Infra.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly Context _db;

        public LoanRepository(Context db)
        {
            _db = db;
        }

        public async Task<ICollection<Loan>> GetAll()
        {
            return await _db.Loans
                .Include(l => l.User)
                .Include(bl => bl.BookLendings)
                .ThenInclude(bl => bl.Copy)
                .ThenInclude(c => c.Book)
                .ToListAsync();
        }

        public async Task<Loan> GetById(Guid id)
        {
            return await _db.Loans
                .Include(l => l.User)
                .Include(bl => bl.BookLendings)
                .ThenInclude(bl => bl.Copy)
                .ThenInclude(c => c.Book)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task Add(Loan model)
        {
            await _db.Loans.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, Loan model)
        {
            if (!LoanExists(id))
            {
                // Loan not found
                throw new InvalidOperationException("Loan not found");
            }
            else
            {
                var loanUpdate = await GetById(id);
                loanUpdate.LoanDate = model.LoanDate;
                loanUpdate.ReturnDate = model.ReturnDate;
                loanUpdate.Status = model.Status;
                loanUpdate.BookLendings = model.BookLendings;

                _db.Loans.Update(loanUpdate);
                await _db.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid id)
        {
            var loan = await _db.Loans.FindAsync(id);
            _db.Loans.Remove(loan!);
            await _db.SaveChangesAsync();
        }

        private bool LoanExists(Guid id)
        {
            return _db.Loans.Any(e => e.Id == id);
        }
    }
}
