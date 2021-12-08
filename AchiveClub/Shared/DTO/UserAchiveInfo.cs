﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchiveClub.Shared.DTO
{
    public class UserAchiveInfo
    {
        public int Id { get; set; }
        public int Xp { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LogoURL { get; set; }
        public bool Completed { get; set; }
    }
}