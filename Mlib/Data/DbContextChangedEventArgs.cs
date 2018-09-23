using Mlib.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data
{
    public class DbContextChangedEventArgs : EventArgs
    {
        public EntityState State { get; }
        public IDataEntity Entity { get; }
        public DbContextChangedEventArgs(IDataEntity entity, EntityState state)
        {
            Entity = entity;
            State = state;
        }

    }
}
