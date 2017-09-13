using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFcoreApp.Models;

namespace EFcoreApp.ViewModel
{
    public class SortViewModel
    {
        public SortState NameSort { get;} //значение для сортировки по имени
        public SortState AgeSort { get; }
        public SortState CompanySort { get; }
        public SortState Current { get; }

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            AgeSort = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            CompanySort = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;
            Current = sortOrder;
        }
    }
}
