using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;

namespace MyUniversity.Web.Controllers
{
    public class EnrollmentsController : BaseController
    {
        [HttpGet]
        [ActionName("GetByStudent")]
        public async Task<ActionResult> Index(Guid studentId)
        {
            var uri = string.Format("api/students/{0}", studentId);
            var studentModel =
                await
                    GetHttpResponMessageResultAsyc<StudentModel>(uri, "Enrollments", "Enrollments.Course", "Enrollments.InstructorProfile", "Enrollments.InstructorProfile.Person");
            ViewBag.StudentFullName = studentModel.Person.FullName;
            return View(studentModel.Enrollments);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id, bool? concurrencyError)
        {
            var uri = string.Format("api/enrollments/{0}", id);
            var viewModel = await GetHttpResponMessageResultAsyc<EnrollmentModel>(uri);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
             if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "Concurrency issue. Please modify again and click Save button.";
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Mark")] EnrollmentModel enrollmentModel)
        {
            if (!ModelState.IsValid) return RedirectToAction("BadRequest", "Error");
            try
            {
                var updated =
                    await PutJsonAsyc(string.Format("api/enrollments/{0}", enrollmentModel.Id), enrollmentModel);
                switch (updated.Type)
                {
                    case ResultType.DbUpdateConcurrencyException:
                        return RedirectToAction("Edit", new {id = enrollmentModel.Id, concurrencyError = true});
                    case ResultType.DataException:
                        ModelState.AddModelError(string.Empty,
                            "Unable to edit. Try again, and if the problem persists contact your system administrator.");
                        return View(enrollmentModel);
                }
            }
            catch (RetryLimitExceededException)
            {
                //TODO
            }
            return RedirectToAction("Edit", "Enrollments", new {id = enrollmentModel.Id, concurrencyError = false});
        }

        [HttpGet]
        public async Task<ViewResult> Create(Guid studentId)
        {
            var getDepartmentsTask = GetHttpResponMessageResultAsyc<List<DepartmentModel>>("api/teachers","Person");

            var departments = await getDepartmentsTask;

            ViewBag.SelectedDepartment = new SelectList(departments, "Id", "Person.FullName");
            var viewModel = new EnrollmentModel
            {
                StudentId = studentId
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DepartmentModel departmentModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            var guid = await PostJsonAsyc("api/departments", departmentModel);
            return RedirectToAction("Details", "Departments", new { id = guid });
        }
    }
}