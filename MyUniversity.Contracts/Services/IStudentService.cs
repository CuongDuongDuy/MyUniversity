using System;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.Services
{
    public interface IStudentService : IBaseService<StudentModel, Guid>
    {
    }
}