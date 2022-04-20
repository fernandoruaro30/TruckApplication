using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckApplication.Models;
using TruckApplication.Models.Entity;

namespace TestTruckApplication
{
    public class ModelFake : General
    {
        private readonly List<ModelEntity> lstModel;
        private readonly ModelEntity _model = new ModelEntity();
        private int Id;
        public ModelFake()
        {
            lstModel = new List<ModelEntity>()
            {
                new ModelEntity() { Id = 1, Description = "Model 1"},
                new ModelEntity() { Id = 2, Description = "Model 2"},
                new ModelEntity() { Id = 3, Description = "Model 3"},
                new ModelEntity() { Id = 4, Description = "Model 1"},
                new ModelEntity() { Id = 5, Description = "Model 1"},
                new ModelEntity() { Id = 6, Description = "Model 2"}
            };
        }
        public ModelFake(int id)
        {
            this.Id = id;
            lstModel = new List<ModelEntity>()
            {
                new ModelEntity() { Id = 1, Description = "Model 1"},
                new ModelEntity() { Id = 2, Description = "Model 2"},
                new ModelEntity() { Id = 3, Description = "Model 3"},
                new ModelEntity() { Id = 4, Description = "Model 1"},
                new ModelEntity() { Id = 5, Description = "Model 1"},
                new ModelEntity() { Id = 6, Description = "Model 2"}
            };
        }
        public ModelFake(ModelEntity model)
        {
            lstModel = new List<ModelEntity>()
            {
                new ModelEntity() { Id = 1, Description = "Model 1"},
                new ModelEntity() { Id = 2, Description = "Model 2"},
                new ModelEntity() { Id = 3, Description = "Model 3"},
                new ModelEntity() { Id = 4, Description = "Model 1"},
                new ModelEntity() { Id = 5, Description = "Model 1"},
                new ModelEntity() { Id = 6, Description = "Model 2"}
            };

            this._model = model;
        }

        public async override Task<object> GetDataAsync()
        {
            return lstModel;
        }

        public async override Task<bool> RemoveAsync()
        {
            ModelEntity modelFound = lstModel.FirstOrDefault(a => a.Id == this.Id);
            var ret = false;
            if (modelFound != null)
            {
                ret = lstModel.Remove(modelFound);
            }

            return ret;
        }

        public async override Task<bool> Save()
        {
            ModelEntity ModelFound = lstModel.Find(a => a.Id == this._model.Id);
            if (ModelFound == null)
            {
                ModelEntity newModel = new ModelEntity();
                newModel.Id = GeraId();
                newModel.Description = this._model.Description;
                lstModel.Add(newModel);
            }
            else
            {
                ModelFound.Description = this._model.Description;
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
