using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApplication.Models.Interfaces;

namespace TruckApplication.Models
{
    public abstract class General: IGeneral
    {
        public abstract Task<bool> Save();
        public abstract Task<bool> RemoveAsync();
        public abstract Task<object> GetDataAsync();
    }
}
