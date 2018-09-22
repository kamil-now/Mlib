using Mlib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data
{
    public interface IDatabaseService
    {
        void Update(IDatabaseEntity entity);
    }
}
