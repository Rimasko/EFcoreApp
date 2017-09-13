using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFcoreApp.Context;
using EFcoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFcoreApp.Controllers
{
    public class HomeController : Controller
    {
        private MobileContext dbContext;
        public HomeController(MobileContext mobileContext)
        {
            dbContext = mobileContext;
        }

        public async Task<IActionResult> Index()
        {
            return View( await dbContext.Phone.ToListAsync());
        }

        //отобразим форму создания
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Асинхронно добавим телефон
        [HttpPost]
        public async Task<IActionResult> Create(Phone phone)
        {
            dbContext.Phone.Add(phone);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Details(int ?id)
        {
            if (id != null)
            {
                var phone = await dbContext.Phone.FirstOrDefaultAsync(p => p.Id == id);
                if (phone != null)
                {
                    return View(phone);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var phone = await dbContext.Phone.FirstOrDefaultAsync(p => p.Id == id);
                if (phone != null)
                {
                    return View(phone);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Phone phone)
        {
            dbContext.Phone.Update(phone);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Phone phone = await dbContext.Phone.FirstOrDefaultAsync(p => p.Id == id);
                if (phone != null)
                    return View(phone);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Phone phone = await dbContext.Phone.FirstOrDefaultAsync(p => p.Id == id);
                if (phone != null)
                {
                    dbContext.Phone.Remove(phone);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
