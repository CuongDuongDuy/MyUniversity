using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
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
        [ValidateAntiForgeryToken]
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
            if (course == null) return HttpNotFound();
            return View(course);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var requestUri = string.Format("api/courses/{0}/department", id);
            var course = await GetHttpResponMessageResultAsyc<CourseModel>(requestUri);
            if (course == null) return HttpNotFound();
            var departments = await GetHttpResponMessageResultAsyc<List<DepartmentModel>>("api/departments");
            ViewBag.DepartmentId = new SelectList(departments, "Id", "Name", course.DepartmentId);
            return View(course);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, CourseModel courseModel)
        {
            var requestUri = string.Format("api/courses/{0}", id);
            if (!ModelState.IsValid) throw new HttpException((int)HttpStatusCode.BadRequest, "Error");
            var guid = await PutJsonAsyc(requestUri, courseModel);
            return RedirectToAction("Details", "Courses", new {id = guid});
        }
    }
}