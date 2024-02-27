namespace ApiBiblioteca.Domain.Models.Interfaces
{
    public interface ILoanRepository
    {
        Task Add(Loan model);
        Task Update(Guid id, Loan model);
        Task Delete(Guid id);
    }
}
