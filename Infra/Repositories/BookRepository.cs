using ApiBiblioteca.Domain.Models;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Infra.Data;

namespace ApiBiblioteca.Infra.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly Context _db;

        public BookRepository(Context db)
        {
            _db = db;
        }

        public async Task Add(Book model)
        {
            await _db.Books.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, Book model)
        {
            _db.Books.Update(model);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var book = await _db.Books.FindAsync(id);
            _db.Books.Remove(book!);
            await _db.SaveChangesAsync();
        }

        private bool BookExists(Guid id)
        {
            return _db.Users.Any(e => e.Id == id);
        }
    }
}
