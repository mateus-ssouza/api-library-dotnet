using ApiBiblioteca.Domain.Models;
using ApiBiblioteca.Domain.Models.Interfaces;
using ApiBiblioteca.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Infra.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly Context _db;

        public BookRepository(Context db)
        {
            _db = db;
        }

        public async Task<ICollection<Book>> GetAll()
        {
            return await _db.Books.ToListAsync();
        }

        public async Task<Book> GetById(Guid id)
        {
            return await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task Add(Book model)
        {
            await _db.Books.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Guid id, Book model)
        {
            if (!BookExists(id))
            {
                // Book not found
                throw new InvalidOperationException("Book not found");
            }
            else
            {
                var bookUpdate = await GetById(id);
                bookUpdate.Title = model.Title;
                bookUpdate.Author = model.Author;
                bookUpdate.ISBN = model.ISBN;

                _db.Books.Update(bookUpdate);
                await _db.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid id)
        {
            var book = await _db.Books.FindAsync(id);
            _db.Books.Remove(book!);
            await _db.SaveChangesAsync();
        }

        private bool BookExists(Guid id)
        {
            return _db.Books.Any(e => e.Id == id);
        }
    }
}
