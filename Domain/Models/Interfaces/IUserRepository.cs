namespace ApiBiblioteca.Domain.Models.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User model);
        Task Update(Guid id, User model);
        Task Delete(Guid id);
    }
}
