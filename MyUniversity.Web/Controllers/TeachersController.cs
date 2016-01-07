using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using PagedList;

namespace MyUniversity.Web.Controllers
{
    public class TeachersController : BaseController
    {

        public async Task<ViewResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = string.IsNullOrEmpty(sortOrder) || sortOrder == "firstName_asc"
                ? "firstName_desc"
                : "firstName_asc";
            ViewBag.LastNameSortParm = !string.IsNullOrEmpty(sortOrder) && sortOrder == "lastName_asc"
                ? "lastName_desc"
                : "lastName_asc";
            ViewBag.EnrollmentDateSortParm = !string.IsNullOrEmpty(sortOrder) && sortOrder == "enrollmentDate_asc"
                ? "enrollmentDate_desc"
                : "enrollmentDate_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var allTeachers = await GetHttpResponMessageResultAsyc<List<TeacherModel>>("api/teachers");

            var teachers = from s in allTeachers select s;
            if (!string.IsNullOrEmpty(searchString))
            {
                teachers =
                    teachers.Where(
                        s => s.Person.FullName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstName_asc":
                    teachers = teachers.OrderBy(s => s.Person.FirstName);
                    break;
                case "firstName_desc":
                    teachers = teachers.OrderByDescending(s => s.Person.FirstName);
                    break;
                case "lastName_asc":
                    teachers = teachers.OrderBy(s => s.Person.LastName);
                    break;
                case "lastName_desc":
                    teachers = teachers.OrderByDescending(s => s.Person.LastName);
                    break;
                case "hireDate_asc":
                    teachers = teachers.OrderBy(s => s.HireDate);
                    break;
                case "hireDate_desc":
                    teachers = teachers.OrderByDescending(s => s.HireDate);
                    break;
                default:
                    teachers = teachers.OrderBy(s => s.Person.FirstName);
                    break;
            }

            var pageSize = 10;
            var pageNumber = (page ?? 1);
            return View(teachers.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var teacher =
                await
                    GetHttpResponMessageResultAsyc<TeacherModel>(string.Format("api/teachers/{0}", id), "Enrollments",
                        "Enrollments.Course");
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var getDepartmentsTask = GetHttpResponMessageResultAsyc<List<DepartmentModel>>("api/departments");

            var departments = await getDepartmentsTask;

            ViewBag.DepartmentId = new SelectList(departments.OrderBy(x => x.Name), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "IdentityNumber, LastName, FirstMidName, DateOfBirth, Address, EnrollmentDate, EffectiveDate, ExpiryDate, DepartmentId")]
        public async Task<ActionResult> Create(TeacherModel teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var guid = await PostJsonAsyc("api/teachers", teacher);
                    return RedirectToAction("Details", new {id = guid});
                }
                return RedirectToAction("BadRequest", "Error");
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("",
                    "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(teacher);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id, bool? concurrencyError)
        {
            var teacher = await GetHttpResponMessageResultAsyc<TeacherModel>(string.Format("api/teachers/{0}", id));
            if (teacher == null)
            {
                return HttpNotFound();
            }
            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "Concurrency issue. Please modify again and click Save button.";
            }
            var departments = await GetHttpResponMessageResultAsyc<List<DepartmentModel>>("api/departments");
            ViewBag.DepartmentId = new SelectList(departments, "Id", "Name", teacher.DepartmentId);
            return View(teacher);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id)
        {
            var teacherToUpdate = await GetHttpResponMessageResultAsyc<TeacherModel>(string.Format("api/teachers/{0}", id));
            if (TryUpdateModel(teacherToUpdate, "",
                new string[]
                {
                    "EffectiveDate", "ExpiryDate", "EnrollmentDate", "DepartmentId",
                    "Person.IdentityNumber", "Person.LastName", "Person.FirstName", "Person.DateOfBirth", "Person.Address"
                }))
            {
                try
                {
                    var updated = await PutJsonAsyc(string.Format("api/teachers/{0}", id), teacherToUpdate);
                    switch (updated.Type)
                    {
                        case ResultType.DbUpdateConcurrencyException:
                            return RedirectToAction("Edit", new {id = teacherToUpdate.Id, concurrencyError = true});
                        case ResultType.DataException:
                            ModelState.AddModelError(string.Empty,
                                "Unable to edit. Try again, and if the problem persists contact your system administrator.");
                            return View(teacherToUpdate);
                    }
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("",
                        "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(teacherToUpdate);
        }

    }
}
