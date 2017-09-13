
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EFcoreApp.Context;
using EFcoreApp.MapsModels;

namespace EFcoreApp.Controllers
{
    public class GoogleMapsController :Controller
    {
        private MobileContext context;
        public GoogleMapsController(MobileContext mobile)
        {
            context = mobile;
        }
        public async Task<IActionResult> Index()
        {
            return View(await context.Places.ToListAsync());
        }
        public IActionResult Show(int place)
        {
            Place cuurent = context.Places.FirstOrDefault(x => x.Id == place);
            if (cuurent != null)
            {
                return View(cuurent);
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult AddPlace()
        {
            return View();
        }
        public async Task<IActionResult> Delete(int place)
        {
            if (place != null && place != 0)
            {
                var Place = await context.Places.FirstOrDefaultAsync(x => x.Id == place);
                if (Place != null)
                {
                    context.Places.Remove(Place);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            return NotFound();
           
        }

        [HttpPost]
        public async Task<IActionResult> AddPlace(ViewPlace p)
        {
            if (p != null)
            {
                
                Place place = new Place
                {
                    Name = p.Name,
                    Title = p.Title,
                    Description = p.Description,
                    lat = Double.Parse(p.lat.Replace(".",",")),
                    lng = double.Parse(p.lng.Replace(".", ","))
                };
                context.Places.Add(place);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NoContent();
        }
    }
}
