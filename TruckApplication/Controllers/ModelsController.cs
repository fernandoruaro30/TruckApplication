using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TruckApplication.Data;
using TruckApplication.Models;
using TruckApplication.Models.Entity;

namespace TruckApplication.Controllers
{
    public class ModelsController : Controller
    {
        private TruckApplicationContext _context;

        public ModelsController(TruckApplicationContext context)
        {
            _context = context;
        }
        // GET: Models
        public async Task<IActionResult> Index()
        {
            Model model = new Model(_context);
            List<ModelEntity> lstModels = (List<ModelEntity>)await model.GetDataAsync();

            return View(lstModels);
        }

        // GET: Models/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Model model = new Model(_context,id.Value);
            var truckModel = ((List<ModelEntity>)await model .GetDataAsync()).FirstOrDefault();
            if (truckModel == null)
            {
                return NotFound();
            }

            return View(truckModel);
        }

        // GET: Models/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Models/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] Models.Entity.ModelEntity Model)
        {
            if (ModelState.IsValid)
            {
                Model model = new Model(_context,Model);
                await model.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(Model);
        }

        // GET: Models/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Model model = new Model(_context,id.Value);
            var truckModel = ((List<ModelEntity>)await model .GetDataAsync()).FirstOrDefault();

            if (truckModel == null)
            {
                return NotFound();
            }
            return View(truckModel);
        }

        // POST: Models/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Models.Entity.ModelEntity Model)
        {
            if (id != Model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Model model = new Model(_context,Model);
                    await model.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ModelExists(Model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Model);
        }

        // GET: Models/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Model model = new Model(_context,id.Value);
            var truckModel = ((List<ModelEntity>)await model.GetDataAsync()).FirstOrDefault();
            if (truckModel == null)
            {
                return NotFound();
            }

            return View(truckModel);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Model model = new Model(_context,id);
            var truckModel = await model.RemoveAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ModelExists(int id)
        {
            Model model = new Model(_context,id);
            var truckModel = (List<ModelEntity>)await model.GetDataAsync();
            return truckModel.Any();
        }
    }
}
