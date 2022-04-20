using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApplication.Models.Interfaces
{
    interface IGeneral
    {
        Task<bool> Save();

        Task<bool> RemoveAsync();
        Task<object> GetDataAsync();
    }
}
