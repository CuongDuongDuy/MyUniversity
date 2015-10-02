using System;
using System.Net;
using System.Web.Http;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Contracts.ViewModels;

namespace MyUniversity.Api.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [Route("students/{id:guid}")]
        [HttpGet]
        public StudentViewModel FullDetails(Guid id)
        {
            var result = studentService.GetViewModel(id);
            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return result;
        }

        [Route("students/{id:guid}/model")]
        [HttpGet]
        public StudentModel Details(Guid id)
        {
            var result = studentService.GetItem(id);
            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return result;
        }

        [Route("students")]
        [HttpPost]
        public Guid Create(StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var studentId = studentService.CreateViewModel(viewModel);
                return studentId;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [Route("students")]
        [HttpPost]
        public void Edit(Guid id, StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                studentService.UpdateViewModel(id, viewModel);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        [Route("students")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            studentService.Delete(id);
        }
    }
}