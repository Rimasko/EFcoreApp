using System;
using System.Collections.Generic;
using EFcoreApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFcoreApp.ViewModel
{
    public class FilterViewModel
    {
        public SelectList Companies { get; private set; } // список компаний
        public int? SelectedCompany { get; private set; }   // выбранная компания
        public string SelectedName { get; private set; }    // введенное имя

        public FilterViewModel(List<Company> companies, int? company, String name)
        {
            companies.Insert(0, new Company { Id = 0, Name = "Все" });
            Companies = new SelectList(companies, "Id", "Name", company);
            SelectedCompany = company;
            SelectedName = name;
        }
    }
}
