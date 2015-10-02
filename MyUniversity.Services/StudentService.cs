using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using MyUniversity.Contracts.Constants;
using MyUniversity.Contracts.Models;
using MyUniversity.Contracts.Services;
using MyUniversity.Contracts.ViewModels;
using MyUniversity.Dal.Entities;
using MyUniversity.Dal.Repositories.Contracts;

namespace MyUniversity.Services
{
    public class StudentService : BaseService<StudentModel, Student, Guid, IStudentRepository>, IStudentService
    {
        private readonly IStudentProfileRepository studentProfileRepository;

        public StudentService(IStudentRepository studentRepository,
            IStudentProfileRepository studentProfileRepository,
            IUnitOfWork unitOfWork)
            : base(studentRepository, unitOfWork)
        {
            this.studentProfileRepository = studentProfileRepository;
        }

        public IEnumerable<StudentModel> GetItems(Expression<Func<StudentModel, bool>> predicate = null)
        {
            var result = TranferToModels(Repository.GetItems(TranferToEntityExpFunc(predicate)));
            return result;
        }

        public IEnumerable<StudentModel> GetAll()
        {
            var result = TranferToModels(Repository.GetAll());
            return result;
        }

        public virtual StudentModel GetItem(Guid id)
        {
            var result = TranferToModel(Repository.GetById(id));
            return result;
        }

        public void Update(StudentModel model)
        {
            var entity = Repository.GetById(model.Id);
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.DateOfBirth = model.DateOfBirth;
            entity.UpdatedBy = EntityConstant.UpdatedBy;
            entity.UpdatedOn = DateTime.Now;
            Repository.Update(entity);
            UnitOfWork.Commit();
        }

        public void Delete(Guid id)
        {
            var entity = Repository.GetById(id);
            if (entity != null)
            {
                entity.UpdatedBy = EntityConstant.UpdatedBy;
                entity.UpdatedOn = DateTime.Now;
                Repository.Delete(id);
                UnitOfWork.Commit();
            }
        }

        public Guid CreateViewModel(StudentViewModel viewModel)
        {
            var studentModel = viewModel.Student;
            var student = TranferToEntity(studentModel);
            student.CreatedBy = EntityConstant.CreatedBy;
            student.CreatedOn = DateTime.Now;

            var studentProfileModel = viewModel.StudentProfile;
            var studentProfile = Mapper.Map<AccountProfile>(studentProfileModel);
            studentProfile.CreatedBy = student.CreatedBy;
            studentProfile.CreatedOn = student.CreatedOn;
            studentProfile.Deactive = false;

            student.Profile = studentProfile;
            Repository.Insert(student);
            UnitOfWork.Commit();

            return student.Guid;
        }

        public void UpdateViewModel(Guid id, StudentViewModel viewModel)
        {
            var studentModel = viewModel.Student;
            
            var student = Repository.GetById(id);
            student.FirstName = studentModel.FirstName;
            student.LastName = studentModel.LastName;
            student.DateOfBirth = studentModel.DateOfBirth;
            student.UpdatedBy = EntityConstant.UpdatedBy;
            student.UpdatedOn = DateTime.Now;

            var studentProfileModel = viewModel.StudentProfile;
            var studentProfile = studentProfileRepository.GetById(id);
            studentProfile.AccountLogin = studentProfileModel.AccountLogin;
            studentProfile.AccountPassword = studentProfileModel.AccountPassword;
            studentProfile.Deactive = studentProfileModel.Deactive;
            studentProfile.UpdatedBy = student.UpdatedBy;
            studentProfile.UpdatedOn = student.UpdatedOn;

            Repository.Update(student);
            studentProfileRepository.Update(studentProfile);
            UnitOfWork.Commit();
        }

        public StudentViewModel GetViewModel(Guid id)
        {
            var viewmodel = new StudentViewModel
            {
                Student = GetItem(id),
                StudentProfile = Mapper.Map<StudentProfileModel>(studentProfileRepository.GetById(id))
            };
            return viewmodel;
        }

        public void SeedSampleData()
        {
            var students = new List<Student>
            {
                new Student
                {
                    FirstName = "Angela",
                    LastName = "Phuong Trinh",
                    DateOfBirth = new DateTime(1988, 09, 30),
                    CreatedBy = EntityConstant.CreatedBy,
                    CreatedOn = DateTime.Now,
                },
                new Student
                {
                    FirstName = "David",
                    LastName = "Beckham",
                    DateOfBirth = new DateTime(1952, 01, 15),
                    CreatedBy = EntityConstant.CreatedBy,
                    CreatedOn = DateTime.Now,
                },
                new Student
                {
                    FirstName = "Ha",
                    LastName = "Tang Thanh",
                    DateOfBirth = new DateTime(1984, 09, 03),
                    CreatedBy = EntityConstant.CreatedBy,
                    CreatedOn = DateTime.Now,
                },
                new Student
                {
                    FirstName = "Thanh",
                    LastName = "Tran",
                    DateOfBirth = new DateTime(1986, 04, 03),
                    CreatedBy = EntityConstant.CreatedBy,
                    CreatedOn = DateTime.Now,
                },
            };

            Repository.Insert(students);
            UnitOfWork.Commit();

            var savedStudent = Repository.GetAll();
            foreach (var student in savedStudent)
            {
                student.Profile = new AccountProfile
                {
                    AccountLogin = student.FirstName,
                    AccountPassword = student.LastName,
                    Deactive = false,
                    CreatedBy = EntityConstant.CreatedBy,
                    CreatedOn = DateTime.Now
                };
                Repository.Update(student);
                UnitOfWork.Commit();
            }

        }
        
    }
}
