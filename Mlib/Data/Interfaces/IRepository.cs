using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data
{
    public interface IRepository
    {
        IDataEntity Get(string id);
        IQueryable<IDataEntity> GetAll();
        void Add(IDataEntity entity);
        void Delete(IDataEntity entity);
        void Edit(IDataEntity entity);
        void Save();
    }
}
