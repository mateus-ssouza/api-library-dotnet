using ApiBiblioteca.Domain.Models;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Infra.Repositories
{
    public class CopyRepository : ICopyRepository
    {
        private readonly Context _db;

        public CopyRepository(Context db)
        {
            _db = db;
        }

        public async Task<ICollection<Copy>> GetAll()
        {
            return await _db.Copies
                .Include(b => b.Book)
                .ToListAsync();
        }

        public async Task<Copy> GetById(Guid id)
        {
            return await _db.Copies
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task Add(Copy model)
        {
            await _db.Copies.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, Copy model)
        {
            if (!CopyExists(id))
            {
                // Copy not found
                throw new InvalidOperationException("Copy not found");
            }
            else
            {
                var copyUpdate = await GetById(id);
                copyUpdate.CopyCode = model.CopyCode;
                copyUpdate.Available = model.Available;

                _db.Copies.Update(copyUpdate);
                await _db.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid id)
        {
            if (!CopyExists(id))
            {
                // Copy not found
                throw new InvalidOperationException("Copy not found");
            }

            var copy = await _db.Copies.FindAsync(id);
            _db.Copies.Remove(copy!);
            await _db.SaveChangesAsync();
        }

        private bool CopyExists(Guid id)
        {
            return _db.Copies.Any(e => e.Id == id);
        }
    }
}
