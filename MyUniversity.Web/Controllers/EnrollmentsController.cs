using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;

namespace MyUniversity.Web.Controllers
{
    public class EnrollmentsController : BaseController
    {
        [HttpGet]
        [ActionName("Index")]
        public async Task<ActionResult> Index(Guid studentId)
        {
            var uri = string.Format("api/students/{0}", studentId);
            var studentModel =
                await
                    GetHttpResponMessageResultAsyc<StudentModel>(uri, "Enrollments", "Enrollments.Course", "Enrollments.InstructorProfile", "Enrollments.InstructorProfile.Person");
            ViewBag.StudentFullName = studentModel.Person.FullName;
            ViewBag.StudentId = studentModel.Id;
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
        public async Task<ActionResult> Edit([Bind(Include = "Id, StudentId, Mark, RowVersion")] EnrollmentModel enrollmentModel)
        {
            if (!ModelState.IsValid) return RedirectToAction("BadRequest", "Error");
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
            return RedirectToAction("Index", "Enrollments", new { studentId = enrollmentModel.StudentId });
        }

        [HttpGet]
        public async Task<ViewResult> Create(Guid studentId)
        {
            var getStudentTask = GetHttpResponMessageResultAsyc<StudentModel>("api/students/" + studentId);
            var getTeachersTask = GetHttpResponMessageResultAsyc<List<TeacherModel>>("api/teachers");
            var getCouresTask = GetHttpResponMessageResultAsyc<List<CourseModel>>("api/courses");
            var student = await getStudentTask;
            var teachers = await getTeachersTask;
            var courses = await getCouresTask;
            ViewBag.Teachers = new SelectList(teachers, "Id", "Person.FullName");
            ViewBag.Courses = new SelectList(courses, "Id", "Title");
            var viewModel = new EnrollmentModel
            {
                StudentId = studentId,
                Student = student
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EnrollmentModel enrollmentModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("BadRequest", "Error");
            }
            var guid = await PostJsonAsyc("api/enrollments", enrollmentModel);
            return RedirectToAction("Index", "Enrollments", new {studentId = enrollmentModel.StudentId});
        }
    }
}