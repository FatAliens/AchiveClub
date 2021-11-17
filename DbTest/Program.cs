using AchiveClub.Shared.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace AchiveClub.DbTest;

class Program
{
    private static List<User> users;
    private static List<Admin> admins;
    private static List<Achive> achivements;

    static void Main()
    {
        FillDb();

        SetRelationships();

        LoadDb();

        PrintCollection(nameof(admins), admins);
        PrintCollection(nameof(users), users);
        PrintCollection(nameof(achivements), achivements);

        using (var db = new ApplicationContext())
        {
            var completedAchives = db.CompletedAchivements
                .Include(a=>a.Achive)
                .Include(a=>a.User)
                .Include(a=>a.Admin)
                .ToList();

            foreach(var completedAchive in completedAchives)
            {
                Console.WriteLine($"{completedAchive.Id} :" +
                    $" {completedAchive.Achive.Title}: " +
                    $" {completedAchive.User.FirstName} {completedAchive.User.LastName}: " +
                    $" {completedAchive.Admin.Name}");
            }
        }
    }

    static void FillDb()
    {
        var userFaker = new Faker<User>("ru")
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Password, f => f.Internet.Password(6));

        var adminFaker = new Faker<Admin>("ru")
            .RuleFor(a => a.Name, f => f.Person.FullName)
            .RuleFor(a => a.Key, f => f.Internet.Password(8));

        var achiveFaker = new Faker<Achive>("ru")
            .RuleFor(a => a.Xp, f => f.Random.Number(5, 100) * 10)
            .RuleFor(a => a.Title, f => $"{f.Hacker.Verb()} {f.Hacker.Adjective()} {f.Hacker.Noun()}")
            .RuleFor(a => a.Description, f => f.Hacker.Phrase())
            .RuleFor(a => a.LogoURL, f => f.Image.PicsumUrl());

        var randomUsers = userFaker.Generate(8);
        var randomAdmins = adminFaker.Generate(3);
        var randomAchivements = achiveFaker.Generate(12);

        using (var db = new ApplicationContext())
        {
            if (db.Users.Count() < 6)
            {
                db.Users.AddRange(randomUsers);
            }
            if (db.Admins.Count() < 3)
            {
                db.Admins.AddRange(randomAdmins);
            }
            if (db.Achivements.Count() < 9)
            {
                db.Achivements.AddRange(randomAchivements);
            }

            //regenerate
            //db.Achivements.RemoveRange(db.Achivements.ToList());
            //db.Achivements.AddRange(randomAchivements);

            db.SaveChanges();
        }
    }

    static void SetRelationships()
    {
        using (var db = new ApplicationContext())
        {
            if (db.CompletedAchivements.Count() < 20)
            {
                var comletedAchiveFaker = new Faker<CompletedAchive>("ru")
                    .RuleFor(a => a.Achive, f => f.PickRandom(db.Achivements.ToList()))
                    .RuleFor(a => a.User, f => f.PickRandom(db.Users.ToList()))
                    .RuleFor(a => a.Admin, f => f.PickRandom(db.Admins.ToList()))
                    .RuleFor(a => a.DateOfCompletion, f => f.Date.Recent(60));

                db.CompletedAchivements.AddRange(comletedAchiveFaker.Generate(40));

                db.SaveChanges();
            }
        }
    }

    static void LoadDb()
    {
        using (var db = new ApplicationContext())
        {
            users = db.Users.Include(u=>u.CompletedAchivements).ToList();
            admins = db.Admins.Include(a => a.CompletedAchivements).ToList();
            achivements = db.Achivements.Include(a => a.CompletedAchivements).ToList();
        }
    }

    static void PrintCollection<T>(string collectionName, IEnumerable<T> collection)
    {
        Console.WriteLine($"@{collectionName}:");
        foreach (var item in collection)
        {
            Console.WriteLine("-----------------");
            Console.WriteLine(item?.ToString());
        }
        Console.WriteLine();
    }
}