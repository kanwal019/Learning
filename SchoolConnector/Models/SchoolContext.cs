using Microsoft.EntityFrameworkCore;

namespace SchoolConnector.Models;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=LEGION-XIX;Database=SchoolDB;Trusted_Connection=True;");
    }
}
