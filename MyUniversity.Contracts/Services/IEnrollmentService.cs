using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyUniversity.Contracts.Models;

namespace MyUniversity.Contracts.Services
{
    public interface IEnrollmentService
    {
        IEnumerable<EnrollmentModel> GetEnrollments();
        EnrollmentModel GetById(Guid id);
        IEnumerable<EnrollmentModel> GetByStudentId(Guid studentId);
        ModificationServiceResult Update(Guid id, EnrollmentModel departmentModel);
    }
}
