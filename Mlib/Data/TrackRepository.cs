using Mlib.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data
{
    public class TrackRepository : GenericRepository<DbContext, Track>
    {
        public TrackRepository(DbContext context) : base(context)
        {
        }

        public override Track Get(string id)=> GetAll().FirstOrDefault(x => x.TrackId == int.Parse(id));
    }
}
