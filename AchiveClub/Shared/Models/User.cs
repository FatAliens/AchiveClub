﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace AchiveClub.Shared.Models
{
    public class User
    {
        [BsonId]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [BsonRef("Achivements")]
        public List<Achive> CompletedAchivements { get; set; }
    }
}