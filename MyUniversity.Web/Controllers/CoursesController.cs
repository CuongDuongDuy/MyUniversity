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
        [HttpGet]
        public async Task<ActionResult> Index(Guid? selectedDepartment)
        {
            var getCoursesTask = GetHttpResponMessageResultAsyc<List<CourseModel>>("api/courses/department");
            var getDepartmentsTask = GetHttpResponMessageResultAsyc<List<DepartmentModel>>("api/departments");

            var courses = await getCoursesTask;
            var departments = await getDepartmentsTask;

            ViewBag.SelectedDepartment = new SelectList(departments, "Id", "Name", selectedDepartment);

            var selectedDepartmentId = selectedDepartment.GetValueOrDefault();
            var viewmodel = new CourseIndexViewModel
            {
                Course = courses.Where(c => !selectedDepartment.HasValue || c.Department.Id == selectedDepartmentId)
                    .OrderBy(c => c.Id)
            };

            return View(viewmodel);
        }

        [HttpGet]

        public async Task<ActionResult> Create()
        {
            var getDepartmentsTask = GetHttpResponMessageResultAsyc<List<DepartmentModel>>("api/departments");

            var departments = await getDepartmentsTask;
            ViewBag.DepartmentId = new SelectList(departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CourseModel course)
        {
            if (!ModelState.IsValid) return RedirectToAction("BadRequest", "Error");
            var guid = await PostJsonAsyc("api/courses", course);
            return RedirectToAction("Details", "Courses", new { id = guid });
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var requestUri = string.Format("api/courses/{0}/department", id);
            var course = await GetHttpResponMessageResultAsyc<CourseModel>(requestUri);
            return View(course);
        }
    }
}