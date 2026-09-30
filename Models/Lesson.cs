namespace ElTasilAcademy.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public required string Title { get; set; } 
        public string? Descreption { get; set; }
        public string? FilePath { get; set; } 
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
