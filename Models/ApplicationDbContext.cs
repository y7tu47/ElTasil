using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ElTasilAcademy.Models;

namespace ElTasilAcademy.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Lesson> Lesson { get; set; }
        public DbSet<Book> Book { get; set; } 
        public DbSet<ElTasilAcademy.Models.BookCategory> BookCategory { get; set; } = default!;
        public DbSet<ElTasilAcademy.Models.Course> Course { get; set; } = default!;
        public DbSet<ElTasilAcademy.Models.Homework> Homework { get; set; } = default!;
        public DbSet<ElTasilAcademy.Models.HomeworkAnswer> HomeworkAnswer { get; set; } = default!;
        public DbSet<ElTasilAcademy.Models.User> User { get; set; } = default!;
        
    }
}
