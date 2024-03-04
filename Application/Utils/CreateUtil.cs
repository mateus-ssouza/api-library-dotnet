using ApiBiblioteca.Application.ViewModel;
using ApiBiblioteca.Domain.Models;

namespace ApiBiblioteca.Application.Utils
{
    public class CreateUtil
    {
        // Function to create loan instance
        public static Loan LoanCreate(LoanViewModel viewModel)
        {
            Loan loan = new()
            {
                LoanDate = viewModel.LoanDate,
                ReturnDate = viewModel.ReturnDate,
                Status = viewModel.Status,
                BookLendings = viewModel.Books.Select(copyId =>
                {
                    return new BookLending { CopyId = copyId };
                }).ToList()
            };

            return loan;
        }
    }
}
