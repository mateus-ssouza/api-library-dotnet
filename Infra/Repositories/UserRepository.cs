using ApiBiblioteca.Domain.Models;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _db;

        public UserRepository(Context db)
        {
            _db = db;
        }

        public async Task Add(User model)
        {
            await _db.Users.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, User model)
        {
            _db.Users.Update(model);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            _db.Users.Remove(user!);
            await _db.SaveChangesAsync();
        }

        private bool UserExists(Guid id)
        {
            return _db.Users.Any(e => e.Id == id);
        }
    }
}
