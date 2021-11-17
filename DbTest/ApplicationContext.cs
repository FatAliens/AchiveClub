using AchiveClub.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AchiveClub.DbTest;
public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Achive> Achivements { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<CompletedAchive> CompletedAchivements { get; set; }

    public ApplicationContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
    }
}
