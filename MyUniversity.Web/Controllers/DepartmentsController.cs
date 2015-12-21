using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Contracts.ViewModels;
using MyUniversity.Contracts.Helpers;

namespace MyUniversity.Web.Controllers
{
    public class DepartmentsController : BaseController
    {

        public async Task<ViewResult> Index()
        {
            var getDeanTask = GetHttpResponMessageResultAsyc<List<DepartmentModel>>("api/departments", "Dean",
                "Dean.Person");
            var viewModel = new DepartmentIndexViewModel
            {
                Departments = await getDeanTask
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ViewResult> Create()
        {
            var getDeanTask = GetHttpResponMessageResultAsyc<List<TeacherModel>>("api/teachers");

            var deans = await getDeanTask;
            ViewBag.DeanId = new SelectList(deans, "Id", "FullName");
            return View();
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
            return RedirectToAction("Details", "Departments", new {id = guid});
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var requestUri = string.Format("api/departments/{0}", id);
            var departmentModel =
                await GetHttpResponMessageResultAsyc<DepartmentModel>(requestUri, "Dean", "Dean.Person");
            if (departmentModel == null) return HttpNotFound();
            return View(departmentModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id, bool? concurrencyError)
        {
            var requestUri = string.Format("api/departments/{0}", id);
            var departmentModel = await GetHttpResponMessageResultAsyc<DepartmentModel>(requestUri);
            if (departmentModel == null) return HttpNotFound();
            var teachersModels = await GetHttpResponMessageResultAsyc<List<TeacherModel>>("api/teachers");
            ViewBag.DeanId = new SelectList(teachersModels, "Id", "FullName", departmentModel.DeanId);
            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "Concurrency issue. If you still want to deactivate this "
                                                  + "record, click the Save button again. Otherwise "
                                                  + "click the Back to List hyperlink.";
            }
            return View(departmentModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DepartmentModel departmentModel)
        {
            var requestUri = string.Format("api/departments/{0}", departmentModel.Id);
            if (!ModelState.IsValid) throw new HttpException((int) HttpStatusCode.BadRequest, "Error");
            var updated = await PutJsonAsyc(requestUri, departmentModel);
            switch (updated.Type)
            {
                case ResultType.DbUpdateConcurrencyException:
                    return RedirectToAction("Edit", new { id = departmentModel.Id, concurrencyError = true });
                case ResultType.DataException:
                    ModelState.AddModelError(string.Empty,
                        "Unable to deactivate. Try again, and if the problem persists contact your system administrator.");
                    return View(departmentModel);
            }
            return RedirectToAction("Details", "Departments", new {id = updated.Value});
        }

        [HttpGet, ActionName("Delete")]
        public async Task<ActionResult> Deactivate(Guid id, bool? concurrencyError)
        {
            var requestUri = string.Format("api/departments/{0}", id);
            var departmentModel =
                await GetHttpResponMessageResultAsyc<DepartmentModel>(requestUri, "Dean", "Dean.Person");
            if (departmentModel == null) return HttpNotFound();
            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "Concurrency issue. If you still want to deactivate this "
                                                  + "record, click the Deactivate button again. Otherwise "
                                                  + "click the Back to List hyperlink.";
            }
            return View(departmentModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deactivate(DepartmentModel departmentModel)
        {
            if (departmentModel == null) return HttpNotFound();
            var requestUri = string.Format("api/departments/{0}/deactivate?rowVersion={1}", departmentModel.Id, departmentModel.RowVersion.ByteArrayToString());
            var deleted = await DeleteJsonAsyc(requestUri);
            switch (deleted.Type)
            {
                case ResultType.DbUpdateConcurrencyException:
                    return RedirectToAction("Delete", new {id = departmentModel.Id, concurrencyError = true});
                case ResultType.DataException:
                    ModelState.AddModelError(string.Empty,
                        "Unable to deactivate. Try again, and if the problem persists contact your system administrator.");
                    return View(departmentModel);
            }
            return RedirectToAction("Details", "Departments", new { id = departmentModel.Id });
        }

        [HttpGet, ActionName("Restore")]
        public async Task<ActionResult> Reactivate(Guid id, bool? concurrencyError)
        {
            var requestUri = string.Format("api/departments/{0}", id);
            var departmentModel =
                await GetHttpResponMessageResultAsyc<DepartmentModel>(requestUri, "Dean", "Dean.Person");
            if (departmentModel == null) return HttpNotFound();
            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "Concurrency issue. If you still want to reactivate this "
                                                  + "record, click the Reactivate button again. Otherwise "
                                                  + "click the Back to List hyperlink.";
            }
            return View(departmentModel);
        }

        [HttpPost, ActionName("Restore")]
        public async Task<ActionResult> Reactivate(DepartmentModel deparmentModel)
        {
            var requestUri = string.Format("api/departments/{0}/activate?rowVersion={1}", deparmentModel.Id, deparmentModel.RowVersion.ByteArrayToString());
            var restored = await DeleteJsonAsyc(requestUri);
            switch (restored.Type)
            {
                case ResultType.DbUpdateConcurrencyException:
                    return RedirectToAction("Restore", new { id = deparmentModel.Id, concurrencyError = true });
                case ResultType.DataException:
                    ModelState.AddModelError(string.Empty,
                        "Unable to reactivate. Try again, and if the problem persists contact your system administrator.");
                    return View(deparmentModel);
            }
            return RedirectToAction("Details","Departments", new{ id = deparmentModel.Id});
        }
    }
}