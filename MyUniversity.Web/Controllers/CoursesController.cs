using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.ViewModels;

namespace MyUniversity.Web.Controllers
{
    public class CoursesController : BaseController
    {
        public async Task<ViewResult> Index(Guid? selectedDepartment)
        {
            var task1 = GetHttpResponMessageResultAsyc<List<CourseModel>>("api/courses/department");
            var task2 = GetHttpResponMessageResultAsyc<List<DepartmentModel>>("api/departments");

            var courses = await task1;
            var departments = await task2;

            ViewBag.SelectedDepartment = new SelectList(departments, "Id", "Name", selectedDepartment);

            var selectedDepartmentId = selectedDepartment.GetValueOrDefault();
            var viewmodel = new CourseIndexViewModel
            {
                Course = courses.Where(c => !selectedDepartment.HasValue || c.Department.Id == selectedDepartmentId)
                    .OrderBy(c => c.Id)
            };

            return View(viewmodel);
        }


    }
}