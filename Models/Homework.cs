namespace ElTasilAcademy.Models
{
    public class Homework
    {
        public int HomeworkId { get; set; }
        public required string Question { get; set; }
        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }

    }
}
