using System.ComponentModel.DataAnnotations.Schema;

namespace ElTasilAcademy.Models
{
    public class HomeworkAnswer
    {
        public int HomeworkAnswerId { get; set; }
        public required string Answer { get; set; }
        public int UserId { get; set; }
        public int HomeworkId { get; set; }
        public User? User { get; set; }
        public Homework? Homework { get; set; }
    }
}
