using Mlib.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data
{
    public class ArtistRepository : GenericRepository<DbContext, Artist>
    {
        public ArtistRepository(DbContext context) : base(context)
        {
        }

        public override Artist Get(string id)
        {
            var all = GetAll();
            return all.FirstOrDefault(x => x.Name == id)??all.FirstOrDefault(x=>x.Id==id);

        }
    }
}
