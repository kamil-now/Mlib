using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data
{
    public interface IDataEntity
    {
        [PrimaryKey, AutoIncrement]
        int ID { get; set; }
        string Id { get; }
    }
}
