using ApiBiblioteca.Domain.Models;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Infra.Data;

namespace ApiBiblioteca.Infra.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly Context _db;

        public LoanRepository(Context db)
        {
            _db = db;
        }

        public async Task Add(Loan model)
        {
            await _db.Loans.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, Loan model)
        {
            _db.Loans.Update(model);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var loan = await _db.Loans.FindAsync(id);
            _db.Loans.Remove(loan!);
            await _db.SaveChangesAsync();
        }
        private bool LoanExists(Guid id)
        {
            return _db.Users.Any(e => e.Id == id);
        }
    }
}
