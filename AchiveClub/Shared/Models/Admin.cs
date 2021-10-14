using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace AchiveClub.Shared.Models
{
    class Admin
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
    }
}
