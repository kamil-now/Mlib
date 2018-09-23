using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data.Models
{
    public class Artist:IDataEntity
    {
        public string Id => Name;
        public string Name { get; set; }
        public string ImageId { get; set; }
        ICollection<Track> Tracks { get; set; }
        ICollection<Album> Albums { get; set; }

    }
}
