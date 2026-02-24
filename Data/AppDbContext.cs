using ConceptsDB.Models;
using Microsoft.EntityFrameworkCore;

namespace ConceptsDB.Data;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Matriculation> Matriculations { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Port=3306;Database=ConceptsToDB;Uid=root;Pwd=Origin260706!;";
        optionsBuilder.UseMySql(connectionString,  ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Matriculation>()
            .HasKey(m => m.Id);

        // Um student tem várias matriculas e a chave deles são o studentId;
        modelBuilder.Entity<Matriculation>()
            .HasOne(m => m.Student)
            .WithMany(s => s.Matriculations)
            .HasForeignKey(fk => fk.StudentId);

        // Um course tem várias matriculas e a chave deles são o courseId;
        modelBuilder.Entity<Matriculation>()
            .HasOne(m => m.Course)
            .WithMany(s => s.Matriculations)
            .HasForeignKey(fk => fk.CourseId);

        modelBuilder.Entity<Matriculation>()
            .HasIndex(m => new { m.StudentId, m.CourseId })
            .IsUnique();
    }
}