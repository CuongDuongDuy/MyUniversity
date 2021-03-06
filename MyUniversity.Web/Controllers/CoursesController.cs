﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Contracts.ViewModels;

namespace MyUniversity.Web.Controllers
{
    public class CoursesController : BaseController
    {
        public async Task<ActionResult> Index(Guid? selectedDepartment)
        {
            var getCoursesTask = GetHttpResponMessageResultAsyc<List<CourseModel>>("api/courses", "department");
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
        public ActionResult Details(Guid id)
        {
            return View(id);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id, bool? concurrencyError)
        {
            var requestUri = string.Format("api/courses/{0}", id);
            var course = await GetHttpResponMessageResultAsyc<CourseModel>(requestUri,"department");
            if (course == null) return HttpNotFound();
            var departments = await GetHttpResponMessageResultAsyc<List<DepartmentModel>>("api/departments");
            ViewBag.DepartmentId = new SelectList(departments, "Id", "Name", course.DepartmentId);
            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "Concurrency issue. If you still want to edit this "
                                                  + "record, click the Save button again. Otherwise "
                                                  + "click the Back to List hyperlink.";
            }
            return View(course);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, CourseModel courseModel)
        {
            var requestUri = string.Format("api/courses/{0}", id);
            if (!ModelState.IsValid) throw new HttpException((int)HttpStatusCode.BadRequest, "Error");
            var updated = await PutJsonAsyc(requestUri, courseModel);
            switch (updated.Type)
            {
                case ResultType.DbUpdateConcurrencyException:
                    return RedirectToAction("Edit", new { id = id, concurrencyError = true });
                case ResultType.DataException:
                    ModelState.AddModelError(string.Empty,
                        "Unable to edit. Try again, and if the problem persists contact your system administrator.");
                    return View(courseModel);
            }
            return RedirectToAction("Details", "Courses", new { id = updated.Value });
        }
    }
}