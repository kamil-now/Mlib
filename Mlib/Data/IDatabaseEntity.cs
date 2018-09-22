using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data
{
    public interface IDatabaseEntity
    {
        [PrimaryKey, AutoIncrement]
        int ID { get; set; }
    }
}
