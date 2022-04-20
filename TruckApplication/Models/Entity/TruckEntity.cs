using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApplication.Models.Entity
{
    public class TruckEntity
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public ModelEntity Model { get; set; }
        public int YearOfFactory { get; set; }
        public int YearOfModel { get; set; }
    }
}
