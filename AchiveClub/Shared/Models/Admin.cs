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
