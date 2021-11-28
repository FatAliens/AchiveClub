using AchiveClub.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Bogus;
using System.Linq;

namespace AchiveClub.Server;
public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Achievement> Achivements { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<CompletedAchievement> CompletedAchivements { get; set; }
    public DbSet<Club> Clubs { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        int userIdCounter = 1, adminIdCounter = 1, achiveIdCounter = 1, completedAchiveIdCounter = 1;

        int usersCount = 20, achiveCount = 10, adminsCount = 5, completedAchiveCount = 100;

        var userFaker = new Faker<User>("ru")
            .RuleFor(u => u.Id, f => userIdCounter++)
            .RuleFor(u => u.FirstName, f => f.Person.FirstName)
            .RuleFor(u => u.LastName, f => f.Person.LastName)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Avatar, f => $"/image/avatars/{f.Random.Number(1, 16)}.jpg")
            .RuleFor(u => u.Password, f => f.Internet.Password(6))
            .RuleFor(u => u.ClubRefId, f => 1);

        var adminFaker = new Faker<Admin>("ru")
            .RuleFor(u => u.Id, f => adminIdCounter++)
            .RuleFor(a => a.Name, f => f.Person.FullName)
            .RuleFor(a => a.Key, f => f.Internet.Password(8));

        var achiveFaker = new Faker<Achievement>("ru")
            .RuleFor(u => u.Id, f => achiveIdCounter++)
            .RuleFor(a => a.Xp, f => f.Random.Number(5, 100) * 10)
            .RuleFor(a => a.Title, f => $"{f.Hacker.Verb()} {f.Hacker.Adjective()} {f.Hacker.Noun()}")
            .RuleFor(a => a.Description, f => f.Hacker.Phrase())
            .RuleFor(a => a.LogoURL, f => f.Image.PicsumUrl(600, 600, imageId: f.Random.Number(1, 100)));

        var comletedAchiveFaker = new Faker<CompletedAchievement>("ru")
            .RuleFor(a => a.Id, f => completedAchiveIdCounter++)
            .RuleFor(a => a.AchiveRefId, f => f.Random.Number(1, achiveCount))
            .RuleFor(a => a.UserRefId, f => f.Random.Number(1, usersCount))
            .RuleFor(a => a.AdminRefId, f => f.Random.Number(1, adminsCount))
            .RuleFor(a => a.DateOfCompletion, f => f.Date.Recent(60));

        var users = userFaker.Generate(usersCount);

        users.Add(new User
        {
            Email = "test@mail.com",
            Password = "1234",
            FirstName = "Алёша",
            LastName = "Попович",
            Avatar = "/image/avatars/8.jpg",
            Id = userIdCounter++,
            ClubRefId = 1
        });

        var admins = adminFaker.Generate(adminsCount);

        admins.Add(new Admin
        {
            Name = "admin",
            Key = "admin",
            Id = adminIdCounter++
        });

        modelBuilder.Entity<Club>().HasData(
            new Club
            {
                Id = 1,
                Title = "Дворец",
                Description = "Господа, современная методология разработки" +
                    "не даёт нам иного выбора, кроме определения стандартных подходов." +
                    "Высокий уровень вовлечения представителей целевой аудитории является" +
                    "четким доказательством простого факта: консультация с широким активом влечет" +
                    "за собой процесс внедрения и модернизации существующих финансовых и административных условий.",
                LogoURL = "./image/dvorec_logo.png",
                Address = "ул. Долгобродская 24,к. 74-75"
            });
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Admin>().HasData(admins);
        modelBuilder.Entity<Achievement>().HasData(achiveFaker.Generate(achiveCount));
        modelBuilder.Entity<CompletedAchievement>().HasData(comletedAchiveFaker.Generate(completedAchiveCount));
    }
}
