using System.Collections.Generic;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.ViewModels
{
    public class CourseIndexViewModel
    {
        public IEnumerable<CourseModel> Course { get; set; }
    }
}
