using ApiBiblioteca.Domain.Enums;
using ApiBiblioteca.Domain.Models;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace ApiBiblioteca.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _db;

        public UserRepository(Context db)
        {
            _db = db;
        }

        public async Task<ICollection<User>> GetAll()
        {
            return await _db.Users
                .Include(u => u.Address)
                .ToListAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _db.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Add(User model)
        {
            await _db.Users.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, User model)
        {
            
            if (!UserExists(id))
            {
                // User not found
                throw new InvalidOperationException("Project not found");
            }
            else
            {
                var userUpdate = await GetById(id);
                userUpdate.Name = model.Name;
                userUpdate.Cpf = model.Cpf;
                userUpdate.Birthday = model.Birthday;
                userUpdate.UserType = model.UserType;
                userUpdate.Email = model.Email;
                userUpdate.Password = model.Password;
                userUpdate.Address = model.Address;

                _db.Users.Update(userUpdate);
                await _db.SaveChangesAsync();
            }
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
