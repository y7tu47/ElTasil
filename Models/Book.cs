namespace ElTasilAcademy.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int BookCategoryId { get; set; }
        public BookCategory? BookCategory { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
