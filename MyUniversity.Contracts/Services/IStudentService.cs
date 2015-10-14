using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.ViewModels;

namespace MyUniversity.Contracts.Services
{
    public interface IStudentService : IBaseService<StudentModel, Guid>
    {
        IEnumerable<StudentModel> GetItems(Expression<Func<StudentModel, bool>> predicate = null);
        IEnumerable<StudentModel> GetAll();
        StudentModel GetItem(Guid id);
        void Update(StudentModel model);
        void Delete(Guid id);
        StudentViewModel GetViewModel(Guid id);
        Guid CreateViewModel(StudentViewModel viewModel);
        void UpdateViewModel(Guid id, StudentViewModel viewModel);
    }
}