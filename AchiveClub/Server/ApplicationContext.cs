using AchiveClub.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Bogus;

namespace AchiveClub.Server;
public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Achive> Achivements { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<CompletedAchive> CompletedAchivements { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        int userIdCounter = 1, adminIdCounter = 1, achiveIdCounter = 1;

        var userFaker = new Faker<User>("ru")
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Password, f => f.Internet.Password(6))
                .RuleFor(u => u.Id, f => userIdCounter++);

        var adminFaker = new Faker<Admin>("ru")
            .RuleFor(a => a.Name, f => f.Person.FullName)
            .RuleFor(a => a.Key, f => f.Internet.Password(8))
            .RuleFor(u => u.Id, f => adminIdCounter++);

        var achiveFaker = new Faker<Achive>("ru")
            .RuleFor(a => a.Xp, f => f.Random.Number(5, 100) * 10)
            .RuleFor(a => a.Title, f => $"{f.Hacker.Verb()} {f.Hacker.Adjective()} {f.Hacker.Noun()}")
            .RuleFor(a => a.Description, f => f.Hacker.Phrase())
            .RuleFor(a => a.LogoURL, f => f.Image.PicsumUrl())
            .RuleFor(u => u.Id, f => achiveIdCounter++);

        modelBuilder.Entity<User>().HasData(userFaker.Generate(20));
        modelBuilder.Entity<Admin>().HasData(adminFaker.Generate(5));
        modelBuilder.Entity<Achive>().HasData(achiveFaker.Generate(40));
    }
}
