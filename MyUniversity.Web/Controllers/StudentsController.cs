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
    public class StudentsController : BaseController
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

            var allStudents = await GetHttpResponMessageResultAsyc<List<StudentModel>>("api/students");

            var students = from s in allStudents select s;
            if (!string.IsNullOrEmpty(searchString))
            {
                students =
                    students.Where(
                        s => s.Person.FullName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstName_asc":
                    students = students.OrderBy(s => s.Person.FirstName);
                    break;
                case "firstName_desc":
                    students = students.OrderByDescending(s => s.Person.FirstName);
                    break;
                case "lastName_asc":
                    students = students.OrderBy(s => s.Person.LastName);
                    break;
                case "lastName_desc":
                    students = students.OrderByDescending(s => s.Person.LastName);
                    break;
                case "enrollmentDate_asc":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "enrollmentDate_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.Person.FirstName);
                    break;
            }

            var pageSize = 10;
            var pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var student =
                await
                    GetHttpResponMessageResultAsyc<StudentModel>(string.Format("api/students/{0}", id), "Enrollments",
                        "Enrollments.Course");
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
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
        public async Task<ActionResult> Create(StudentModel student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var guid = await PostJsonAsyc("api/students", student);
                    return RedirectToAction("Details", new {id = guid});
                }
                return RedirectToAction("BadRequest", "Error");
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("",
                    "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(student);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id, bool? concurrencyError)
        {
            var student = await GetHttpResponMessageResultAsyc<StudentModel>(string.Format("api/students/{0}", id));
            if (student == null)
            {
                return HttpNotFound();
            }
            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "Concurrency issue. Please modify again and click Save button.";
            }
            var departments = await GetHttpResponMessageResultAsyc<List<DepartmentModel>>("api/departments");
            ViewBag.DepartmentId = new SelectList(departments, "Id", "Name", student.DepartmentId);
            return View(student);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id)
        {
            var studentToUpdate = await GetHttpResponMessageResultAsyc<StudentModel>(string.Format("api/students/{0}", id));
            if (TryUpdateModel(studentToUpdate, "",
                new string[]
                {
                    "EffectiveDate", "ExpiryDate", "EnrollmentDate", "DepartmentId",
                    "Person.IdentityNumber", "Person.LastName", "Person.FirstName", "Person.DateOfBirth", "Person.Address"
                }))
            {
                try
                {
                    var updated = await PutJsonAsyc(string.Format("api/students/{0}", id), studentToUpdate);
                    switch (updated.Type)
                    {
                        case ResultType.DbUpdateConcurrencyException:
                            return RedirectToAction("Edit", new {id = studentToUpdate.Id, concurrencyError = true});
                        case ResultType.DataException:
                            ModelState.AddModelError(string.Empty,
                                "Unable to deactivate. Try again, and if the problem persists contact your system administrator.");
                            return View(studentToUpdate);
                    }
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("",
                        "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

    }
}
