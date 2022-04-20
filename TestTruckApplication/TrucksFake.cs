using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckApplication.Models;
using TruckApplication.Models.Entity;

namespace TestTruckApplication
{
    public class TrucksFake : General
    {
        private readonly List<TruckEntity> lstTruck;
        private readonly TruckEntity _truck = new TruckEntity();
        private int Id;
        public TrucksFake()
        {
            lstTruck = new List<TruckEntity>()
            {
                new TruckEntity() { Id = 1, ModelId = 1, Model = new ModelEntity(){ Id = 1, Description = "Model 1"}, YearOfFactory = 2010, YearOfModel = 2010 },
                new TruckEntity() { Id = 2, ModelId = 2, Model = new ModelEntity(){ Id = 2, Description = "Model 2"}, YearOfFactory = 2011, YearOfModel = 2011 },
                new TruckEntity() { Id = 3, ModelId = 3, Model = new ModelEntity(){ Id = 3, Description = "Model 3"}, YearOfFactory = 2012, YearOfModel = 2013 },
                new TruckEntity() { Id = 4, ModelId = 1, Model = new ModelEntity(){ Id = 1, Description = "Model 1"}, YearOfFactory = 2013, YearOfModel = 2013 },
                new TruckEntity() { Id = 5, ModelId = 1, Model = new ModelEntity(){ Id = 1, Description = "Model 1"}, YearOfFactory = 2020, YearOfModel = 2020 },
                new TruckEntity() { Id = 6, ModelId = 2, Model = new ModelEntity(){ Id = 2, Description = "Model 2"}, YearOfFactory = 2021, YearOfModel = 2022 }
            };
        }
        public TrucksFake(int id)
        {
            this.Id = id;
            lstTruck = new List<TruckEntity>()
            {
                new TruckEntity() { Id = 1, ModelId = 1, Model = new ModelEntity(){ Id = 1, Description = "Model 1"}, YearOfFactory = 2010, YearOfModel = 2010 },
                new TruckEntity() { Id = 2, ModelId = 2, Model = new ModelEntity(){ Id = 2, Description = "Model 2"}, YearOfFactory = 2011, YearOfModel = 2011 },
                new TruckEntity() { Id = 3, ModelId = 3, Model = new ModelEntity(){ Id = 3, Description = "Model 3"}, YearOfFactory = 2012, YearOfModel = 2013 },
                new TruckEntity() { Id = 4, ModelId = 1, Model = new ModelEntity(){ Id = 1, Description = "Model 1"}, YearOfFactory = 2013, YearOfModel = 2013 },
                new TruckEntity() { Id = 5, ModelId = 1, Model = new ModelEntity(){ Id = 1, Description = "Model 1"}, YearOfFactory = 2020, YearOfModel = 2020 },
                new TruckEntity() { Id = 6, ModelId = 2, Model = new ModelEntity(){ Id = 2, Description = "Model 2"}, YearOfFactory = 2021, YearOfModel = 2022 }
            };
        }
        public TrucksFake(TruckEntity truck)
        {
            lstTruck = new List<TruckEntity>()
            {
                new TruckEntity() { Id = 1, ModelId = 1, Model = new ModelEntity(){ Id = 1, Description = "Model 1"}, YearOfFactory = 2010, YearOfModel = 2010 },
                new TruckEntity() { Id = 2, ModelId = 2, Model = new ModelEntity(){ Id = 2, Description = "Model 2"}, YearOfFactory = 2011, YearOfModel = 2011 },
                new TruckEntity() { Id = 3, ModelId = 3, Model = new ModelEntity(){ Id = 3, Description = "Model 3"}, YearOfFactory = 2012, YearOfModel = 2013 },
                new TruckEntity() { Id = 4, ModelId = 1, Model = new ModelEntity(){ Id = 1, Description = "Model 1"}, YearOfFactory = 2013, YearOfModel = 2013 },
                new TruckEntity() { Id = 5, ModelId = 1, Model = new ModelEntity(){ Id = 1, Description = "Model 1"}, YearOfFactory = 2020, YearOfModel = 2020 },
                new TruckEntity() { Id = 6, ModelId = 2, Model = new ModelEntity(){ Id = 2, Description = "Model 2"}, YearOfFactory = 2021, YearOfModel = 2022 }
            };

            this._truck = truck;
        }

        public async override Task<object> GetDataAsync()
        {
            return lstTruck;
        }

        public async override Task<bool> RemoveAsync()
        {
            TruckEntity truckFound = lstTruck.FirstOrDefault(a => a.Id == this.Id);
            var ret = false;
            if (truckFound != null)
            {
                ret = lstTruck.Remove(truckFound);
            }

            return ret;
        }

        public async override Task<bool> Save()
        {
            TruckEntity TruckFound = lstTruck.Find(a=>a.Id == this._truck.Id);
            if (TruckFound == null)
            {
                TruckEntity newTruck = new TruckEntity();
                newTruck.Id = GeraId();
                newTruck.ModelId = this._truck.ModelId;
                newTruck.YearOfFactory = this._truck.YearOfFactory;
                newTruck.YearOfModel = this._truck.YearOfModel;
                lstTruck.Add(newTruck);
            }
            else
            {
                TruckFound.ModelId = this._truck.ModelId;
                TruckFound.YearOfFactory = this._truck.YearOfFactory;
                TruckFound.YearOfModel = this._truck.YearOfModel;
            }
            return true;
        }
        static int GeraId()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }
    }
}
