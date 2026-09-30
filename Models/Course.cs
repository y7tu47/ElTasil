namespace ElTasilAcademy.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        //public ICollection<User>? Users { get; set; }
    }
}
