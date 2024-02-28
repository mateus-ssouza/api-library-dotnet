using ApiBiblioteca.Application.ViewModel;
using ApiBiblioteca.Domain.Models;

namespace ApiBiblioteca.Application.Utils
{
    public class CreateUtil
    {
        // Function to create user instance
        public static User UserCreate(UserViewModel viewModel)
        {
            User user = new()
            {   Name = viewModel.Name,
                Cpf = viewModel.Cpf,
                Birthday = viewModel.Birthday,
                UserType = viewModel.UserType,
                Email = viewModel.Email,
                Password = viewModel.Password
            };

            return user;
        }

        // Function to create book instance
        public static Book BookCreate(BookViewModel viewModel)
        {
            Book book = new()
            {
                Title = viewModel.Title,
                Author = viewModel.Author,
                ISBN = viewModel.ISBN,
                CopyCode = viewModel.CopyCode
            };

            return book;
        }

        // Function to create loan instance
        public static Loan LoanCreate(LoanViewModel viewModel)
        {
            Loan loan = new()
            {
                LoanDate = viewModel.LoanDate,
                ReturnDate = viewModel.ReturnDate,
                Status = viewModel.Status
            };

            return loan;
        }
    }
}
