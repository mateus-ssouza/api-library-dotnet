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

        public async Task<ICollection<Loan>> GetAllByUserId(Guid userId)
        {
            try
            {
                return await _db.Loans
                    .Where(l => l.UserId == userId)
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
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                var loanUpdate = await GetById(id);

                if (!AreBookLendingsEqual(loanUpdate.BookLendings, model.BookLendings))
                {
                    foreach (var copyLoan in loanUpdate.BookLendings)
                    {
                        copyLoan.Copy.Available = true;
                    }

                    foreach (var copyLoan in model.BookLendings)
                    {
                        var copy = await _db.Copies.FirstOrDefaultAsync(c => c.Id == copyLoan.CopyId);

                        if(copy != null) copy.Available = false;
                        else throw new InvalidOperationException($"The copy with ID '{copyLoan.CopyId}' is not available for loan.");
                    }

                    loanUpdate.BookLendings = model.BookLendings;
                }

                loanUpdate.LoanDate = model.LoanDate;
                loanUpdate.ReturnDate = model.ReturnDate;
                loanUpdate.Status = model.Status;

                _db.Loans.Update(loanUpdate);
                await _db.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                var loan = await _db.Loans
                    .Include(l => l.BookLendings)
                    .ThenInclude(bl => bl.Copy)
                    .FirstOrDefaultAsync(l => l.Id == id);

                if (loan == null) throw new InvalidOperationException($"Loan with ID '{id}' not found.");

                foreach (var copyLoan in loan.BookLendings)
                {
                    copyLoan.Copy.Available = true;
                }

                _db.Loans.Remove(loan);
                await _db.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> LoanIsUser(Guid idUser, Guid idLoan)
        {
            try
            {
                return await _db.Loans.AnyAsync(l => l.UserId == idUser && l.Id == idLoan);
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> ExistsLoan(Guid id)
        {
            try
            {
                return await _db.Loans.AnyAsync(l => l.Id == id);
            }
            catch (Exception) { throw; }
        }

        private bool AreBookLendingsEqual(ICollection<BookLending> list1, ICollection<BookLending> list2)
        {
            if (list1.Count != list2.Count) return false;

            var copiesList1 = list1.Select(bl => bl.CopyId).OrderBy(id => id);
            var copiesList2 = list2.Select(bl => bl.CopyId).OrderBy(id => id);

            return copiesList1.SequenceEqual(copiesList2);
        }
    }
}
