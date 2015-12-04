using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Microsoft.Data.OData;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Dal;
using MyUniversity.Dal.Entities;

namespace MyUniversity.Api.Controllers
{
    public class StudentsController : ODataController
    {
        private static readonly ODataValidationSettings ValidationSettings = new ODataValidationSettings();

        private readonly IStudentService studentService;

        
        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [EnableQuery]
        public IHttpActionResult GetStudents(ODataQueryOptions<StudentProfile> queryOptions)
        {
            try
            {
                queryOptions.Validate(ValidationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
            var dbContext = new MyUniversityDbContext();
            var students = dbContext.StudentProfiles;
            queryOptions.ApplyTo()
            return Ok(students);
        }

        // GET: odata/Students(5)
        public IHttpActionResult GetStudentModel([FromODataUri] Guid key, ODataQueryOptions<StudentModel> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(ValidationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
            var student = studentService.GetById(key);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // PUT: odata/Students(5)
        public IHttpActionResult Put([FromODataUri] Guid key, Delta<StudentModel> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(studentModel);

            // TODO: Save the patched entity.

            // return Updated(studentModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Students
        public IHttpActionResult Post(StudentModel studentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(studentModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Students(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Guid key, Delta<StudentModel> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(studentModel);

            // TODO: Save the patched entity.

            // return Updated(studentModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Students(5)
        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
