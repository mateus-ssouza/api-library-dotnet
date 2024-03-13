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
            try
            {
                return await _db.Loans
                .Include(l => l.User)
                .Include(bl => bl.BookLendings)
                .ThenInclude(bl => bl.Copy)
                .ThenInclude(c => c.Book)
                .ToListAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task<Loan> GetById(Guid id)
        {
            try
            {
                return await _db.Loans
                .Include(l => l.User)
                .Include(bl => bl.BookLendings)
                .ThenInclude(bl => bl.Copy)
                .ThenInclude(c => c.Book)
                .FirstOrDefaultAsync(l => l.Id == id);
            }
            catch (Exception) { throw; }   
        }

        public async Task Add(Loan model)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                foreach (var copyLoan in model.BookLendings)
                {
                    var copy = await _db.Copies.FirstOrDefaultAsync(c => c.Id == copyLoan.CopyId && c.Available);

                    if (copy != null) copy.Available = false;
                    else throw new InvalidOperationException($"The copy with ID '{copyLoan.CopyId}' is not available for loan.");
                }

                _db.Loans.Add(model);
                await _db.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task Update(Guid id, Loan model)
        {
            try
            {
                var loanUpdate = await GetById(id);
                loanUpdate.LoanDate = model.LoanDate;
                loanUpdate.ReturnDate = model.ReturnDate;
                loanUpdate.Status = model.Status;
                loanUpdate.BookLendings = model.BookLendings;

                _db.Loans.Update(loanUpdate);
                await _db.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var loan = await _db.Loans.FindAsync(id);
                _db.Loans.Remove(loan!);
                await _db.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }
    }
}
