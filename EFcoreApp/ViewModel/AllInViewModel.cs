using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFcoreApp.Models;

namespace EFcoreApp.ViewModel
{
    public class AllInViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public PageViewModel PageViewModel { get; set;}
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }

    }
}
