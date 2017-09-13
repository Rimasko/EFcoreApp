using EFcoreApp.Context;
using EFcoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFcoreApp.ViewModel;

namespace EFcoreApp.Controllers
{
    public class UserController : Controller 
    {
        private MobileContext context;
        public UserController(MobileContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index(int ? company, string name,SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<User> users = context.Users.Include(x => x.Company);

            

            if (company != null && company != 0)
            {
                users = users.Where(p => p.CompanyId == company);
            }
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }


            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["AgeSort"] = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            ViewData["CompSort"] = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.AgeDesc:
                    users = users.OrderByDescending(s => s.Age);
                    break;
                case SortState.AgeAsc:
                    users = users.OrderBy(s => s.Age);
                    break;
                case SortState.CompanyAsc:
                    users = users.OrderBy(s => s.Company);
                    break;
                case SortState.CompanyDesc:
                    users = users.OrderByDescending(s => s.Company);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }
            List<Company> companies = context.Companies.ToList();
            companies.Insert(0, new Company { Name = "Все", Id = 0 });
            IndexViewModel indexView = new IndexViewModel
            {
                Users = await users.AsNoTracking().ToListAsync(),
                Companies = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(companies, "Id", "Name"),
                Name = name,
                SortViewModel = new Models.SortViewModel(sortOrder)

            };
            return View(indexView);

        }

        /*public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<User> users = context.Users.Include(x => x.Company);

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["AgeSort"] = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            ViewData["CompSort"] = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.AgeDesc:
                    users = users.OrderByDescending(s => s.Age);
                    break;
                case SortState.AgeAsc:
                    users = users.OrderBy(s => s.Age);
                    break;
                case SortState.CompanyAsc:
                    users = users.OrderBy(s => s.Company);
                    break;
                case SortState.CompanyDesc:
                    users = users.OrderByDescending(s => s.Company);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }
            IndexViewModel indexView = new IndexViewModel
            {
                Users = await users.AsNoTracking().ToListAsync(),
                SortViewModel = new SortViewModel(sortOrder)
            };
            return View(indexView);

        }*/

        public async Task<IActionResult> Pages(int page = 1)
        {
            int pageSize = 4;
            IQueryable<User> users = context.Users.Include(c => c.Company);
            var count = await users.CountAsync();
            var items = await users.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PagesActionViewModel pages = new PagesActionViewModel
            {
                Users = items,
                PageViewModel = pageViewModel
            };
            return View(pages);
        }

        public async Task<IActionResult> Allfunc(int? company, string name, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 5;
            //filtr
            IQueryable<User> users = context.Users.Include(x => x.Company);

            if (company != null && company != 0)
            {
                users = users.Where(x => x.CompanyId == company);
                    }
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }
            //sorting

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.AgeAsc:
                    users = users.OrderBy(s => s.Age);
                    break;
                case SortState.AgeDesc:
                    users = users.OrderByDescending(s => s.Age);
                    break;
                case SortState.CompanyAsc:
                    users = users.OrderBy(s => s.Company.Name);
                    break;
                case SortState.CompanyDesc:
                    users = users.OrderByDescending(s => s.Company.Name);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }
            // пагинация
            var count = await users.CountAsync();
            var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            AllInViewModel viewModel = new AllInViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new ViewModel.SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(context.Companies.ToList(), company, name),
                Users = items
            };
            return View(viewModel);


        }
    }
}
