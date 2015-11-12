using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            var response = await Client.GetAsync("api/courses/department");
            var courses = response.IsSuccessStatusCode
                ? await response.Content.ReadAsAsync<List<CourseModel>>()
                : new List<CourseModel>();

            response = await Client.GetAsync("api/departments");
            var departments = response.IsSuccessStatusCode
                ? await response.Content.ReadAsAsync<List<DepartmentModel>>()
                : new List<DepartmentModel>();
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