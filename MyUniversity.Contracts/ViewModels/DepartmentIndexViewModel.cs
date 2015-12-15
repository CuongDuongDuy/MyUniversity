using System.Collections.Generic;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.ViewModels
{
    public class DepartmentIndexViewModel
    {
        public IEnumerable<DepartmentModel> Departments { get; set; }
    }
}
