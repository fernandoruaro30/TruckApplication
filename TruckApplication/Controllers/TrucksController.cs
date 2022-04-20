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
    public class TrucksController : Controller
    {
        private TruckApplicationContext _context;

        public TrucksController(TruckApplicationContext context)
        {
            _context = context;
        }
        // GET: Trucks
        public async Task<IActionResult> Index()
        {
            Trucks truck = new Trucks(_context);
            List<TruckEntity> lstTrucks = (List<TruckEntity>)await truck.GetDataAsync();

            return View(lstTrucks);
        }

        // GET: Trucks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Trucks truck = new Trucks(_context,id.Value);
            var truckData = ((List<TruckEntity>)await truck.GetDataAsync()).FirstOrDefault();
            if (truckData == null)
            {
                return NotFound();
            }

            return View(truckData);
        }

        // GET: Trucks/Create
        public async Task<IActionResult> Create()
        {
            Model model = new Model(_context);
            ViewBag.listOfmodels = (List<ModelEntity>)await model.GetDataAsync();
            ViewBag.yearOfFactory = DateTime.Now.Year;
            ViewBag.yearOfModel = DateTime.Now.Year;
            return View();
        }

        // POST: Trucks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModelId,YearOfFactory,YearOfModel")] Models.Entity.TruckEntity truckData)
        {
            if (ModelState.IsValid)
            {
                Trucks truck = new Trucks(_context,truckData);
                await truck.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(truckData);
        }

        // GET: Trucks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Trucks truck = new Trucks(_context,id.Value);
            var truckData = ((List<TruckEntity>)await truck.GetDataAsync()).FirstOrDefault();

            if (truckData == null)
            {
                return NotFound();
            }
            Model model = new Model(_context);
            ViewBag.listOfmodels = (List<ModelEntity>)await model.GetDataAsync();
            ViewBag.ModelId = truckData.ModelId;
            return View(truckData);
        }

        // POST: Trucks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModelId,YearOfFactory,YearOfModel")] Models.Entity.TruckEntity trucksData)
        {
            if (id != trucksData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Trucks truck = new Trucks(_context,trucksData);
                    await truck.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TruckExists(trucksData.Id))
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
            return View(trucksData);
        }

        // GET: Trucks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Trucks truck = new Trucks(_context,id.Value);
            var truckData = ((List<TruckEntity>)await truck.GetDataAsync()).FirstOrDefault();
            if (truckData == null)
            {
                return NotFound();
            }

            return View(truckData);
        }

        // POST: Trucks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Trucks truck = new Trucks(_context,id);
            var truckData = await truck.RemoveAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TruckExists(int id)
        {
            Trucks truck = new Trucks(_context,id);
            var truckData = (List<TruckEntity>)await truck.GetDataAsync();
            return truckData.Any();
        }
    }
}
