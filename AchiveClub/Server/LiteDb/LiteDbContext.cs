using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.Extensions.Options;

namespace AchiveClub.Server
{
    public class LiteDbContext
    {
        public LiteDatabase Db { get; set; }

        public LiteDbContext(IOptions<LiteDbOptions> options)
        {
            Db = new LiteDatabase(options.Value.DatabaseLocation);
        }
    }
}
