namespace ElTasilAcademy.Models
{
    public class BookCategory
    {
        public int BookCategoryId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
