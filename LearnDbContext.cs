using LearnStudentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnStudentAPI
{
    public class LearnDbContext : DbContext
    {
        public LearnDbContext(DbContextOptions<LearnDbContext> options) : base(options) { }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey(x => x.CourseId);  
        }
    }
}
