using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TruckApplication.Data;
using TruckApplication.Models.Entity;

namespace TruckApplication.Models
{
    public class Trucks : General
    {
        [Key]
        public int Id { get; set; }
        public int ModelId { get; set; }
        public virtual Model Model { get; set; }
        [Display(Name = "Year of factory")]
        public int YearOfFactory { get; set; }
        [Display(Name = "Year of modal")]
        public int YearOfModel { get; set; }
        [NotMapped]
        protected TruckApplicationContext _context { get; set; }
        public async override Task<object> GetDataAsync()
        {
            var trucks = (from a in _context.Trucks
                          select new TruckEntity
                          {
                              Id = a.Id,
                              ModelId = a.ModelId,
                              Model = new ModelEntity { Id = a.Model.Id, Description = a.Model.Description},
                              YearOfFactory = a.YearOfFactory,
                              YearOfModel = a.YearOfModel

                          });

            if (this.Id > 0)
            {
                trucks = (from a in trucks
                          where a.Id == this.Id
                          select a);
            }

            return trucks.ToList();
        }

        public async override Task<bool> RemoveAsync()
        {
            Trucks truck = _context.Trucks.FirstOrDefault(a => a.Id == this.Id);
            var ret = 0;
            if (truck != null)
            {
                _context.Trucks.Remove(truck);
                ret = await _context.SaveChangesAsync();
            }

            return ret > 0;
        }

        public async override Task<bool> Save()
        {
            Trucks TruckFound = _context.Trucks.Find(this.Id);
            if (TruckFound == null)
            {
                _context.Trucks.Add(this);
            }
            else
            {
                TruckFound.ModelId = this.ModelId;
                TruckFound.YearOfFactory = this.YearOfFactory;
                TruckFound.YearOfModel = this.YearOfModel;
            }

            await _context.SaveChangesAsync();

            return true;
        }
        public Trucks(TruckApplicationContext context)
        {
            _context = context;
            this.Model = new Model(context);
        }
        public Trucks(TruckApplicationContext context,int id)
        {
            _context = context;
            this.Id = id;
        }
        public Trucks(TruckApplicationContext context, Models.Entity.TruckEntity truck)
        {
            _context = context;
            Id = truck.Id;
            ModelId = truck.ModelId;
            Model = (truck.Model != null ? new Model() { Id = truck.Model.Id, Description = truck.Model.Description } : null);
            YearOfFactory = truck.YearOfFactory;
            YearOfModel = truck.YearOfModel;
        }
    }
}
