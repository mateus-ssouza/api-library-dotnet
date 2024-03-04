namespace ApiBiblioteca.Domain.DTOs
{
    public class CopyDTO
    {
        public string CopyCode { get; set; }
        public bool Available { get; set; }
        public BookDTO Book { get; set; }
    }
}
