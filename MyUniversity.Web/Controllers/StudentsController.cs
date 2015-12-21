﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyUniversity.Contracts.Models;
using PagedList;

namespace MyUniversity.Web.Controllers
{
    public class StudentsController : BaseController
    {

        public async Task<ViewResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = string.IsNullOrEmpty(sortOrder) || sortOrder == "firstName_asc" ? "firstName_desc" : "firstName_asc";
            ViewBag.LastNameSortParm = !string.IsNullOrEmpty(sortOrder) && sortOrder == "lastName_asc" ? "lastName_desc" : "lastName_asc";
            ViewBag.EnrollmentDateSortParm = !string.IsNullOrEmpty(sortOrder) && sortOrder == "enrollmentDate_asc" ? "enrollmentDate_desc" : "enrollmentDate_asc";

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
                students = students.Where(s => s.LastName.Contains(searchString) || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstName_asc":
                    students = students.OrderBy(s => s.FirstName);
                    break;
                case "firstName_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;
                case "lastName_asc":
                    students = students.OrderBy(s => s.LastName);
                    break;
                case "lastName_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "enrollmentDate_asc":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "enrollmentDate_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.FirstName);
                    break;
            }

            var pageSize = 10;
            var pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }


        //// GET: Student/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return System.Web.UI.WebControls.View(student);
        //}

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "LastName, FirstMidName, EnrollmentDate")]StudentModel student)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Students.Add(student);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (RetryLimitExceededException)
        //    {
        //        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
        //    }
        //    return View(student);
        //}


        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Students.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return System.Web.UI.WebControls.View(student);
        //}

        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditPost(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var studentToUpdate = db.Students.Find(id);
        //    if (TryUpdateModel(studentToUpdate, "",
        //       new string[] { "LastName", "FirstMidName", "EnrollmentDate" }))
        //    {
        //        try
        //        {
        //            db.SaveChanges();

        //            return RedirectToAction("Index");
        //        }
        //        catch (RetryLimitExceededException)
        //        {
        //            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
        //        }
        //    }
        //    return System.Web.UI.WebControls.View(studentToUpdate);
        //}

    }
}
