using Mlib.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data
{
    public class AlbumRepository : GenericRepository<DbContext, Album>
    {
        public AlbumRepository(DbContext context) : base(context)
        {
        }

        public override Album Get(string id) => GetAll().FirstOrDefault(x => x.Id == id) ;
        public Album Get(int id) => GetAll().FirstOrDefault(x => x.AlbumId == id);

      
    }
}
