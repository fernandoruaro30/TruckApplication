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
    public class Model: General
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<Trucks> Trucks { get; set; }
        [NotMapped]
        protected TruckApplicationContext _context { get; set; }

        public async override Task<object> GetDataAsync()
        {
            var models = (from a in _context.Model
                            select new ModelEntity
                            {
                                Id = a.Id,
                                Description = a.Description

                            });

            if (this.Id > 0)
            {
                models = (from a in models
                            where a.Id == this.Id
                          select a);
            }

            return models.ToList();
        }

        public async override Task<bool> RemoveAsync()
        {
            Model model = _context.Model.FirstOrDefault(a=>a.Id == this.Id);
            var ret = 0;
            if (model != null)
            {
                _context.Model.Remove(model);
                ret = await _context.SaveChangesAsync();
            }

            return ret > 0;
        }

        public async override Task<bool> Save()
        {
            Model ModelFound = _context.Model.Find(this.Id);
            if (ModelFound == null)
            {
                _context.Model.Add(this);
            }
            else
            {
                ModelFound.Description = this.Description;
            }

            await _context.SaveChangesAsync();

            return true;
        }
        public Model()
        {

        }

        public Model(TruckApplicationContext context)
        {
            _context = context;
            this.Trucks = new HashSet<Trucks>();
        }
        public Model(TruckApplicationContext context,int id)
        {
            _context = context;
            this.Id = id;
        }
        public Model(TruckApplicationContext context,Entity.ModelEntity model)
        {
            _context = context;
            Id = model.Id;
            Description = model.Description;
            this.Trucks = new HashSet<Trucks>();
        }
    }
}
