using Azure;

namespace ApiBiblioteca.Domain.Models
{
    public class BookLending
    {
        public Guid LoanId { get; set; }
        public Loan Loan { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
