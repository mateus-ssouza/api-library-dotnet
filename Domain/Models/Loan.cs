using ApiBiblioteca.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiBiblioteca.Domain.Models
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly LoanDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly ReturnDate { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        [StringLength(45)]
        public double Fines { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public ICollection<BookLending> BookLendings { get; set; }
    }
}
