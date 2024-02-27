using Azure;

namespace ApiBiblioteca.Domain.Models.Interfaces
{
    public interface IBookRepository
    {
        Task Add(Book model);
        Task Update(Guid id, Book model);
        Task Delete(Guid id);
    }
}
