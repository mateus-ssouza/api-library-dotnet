﻿namespace ApiBiblioteca.Domain.Models.Interfaces
{
    public interface ILoanRepository
    {
        Task Add(Loan model);
        Task Update(Guid id, Loan model);
        Task Delete(Guid id);
        Task<ICollection<Loan>> GetAll();
        Task<Loan> GetById(Guid id);
    }
}
