using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiBiblioteca.Domain.Models
{
    public class Book
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        [StringLength(45)]
        public string Author { get; set; }

        [Required]
        [StringLength(45)]
        public string ISBN { get; set; }

        [Required]
        [StringLength(45)]
        public string CopyCode { get; set; }

        [JsonIgnore]
        public ICollection<BookLending> BookLendings { get; set; }
    }
}
