using AutoMapper;
using MyUniversity.Contracts.Models;
using MyUniversity.Dal.Entities;
using Profile = AutoMapper.Profile;

namespace MyUniversity.ApiStart.AutoMapperProfiles
{
    internal class ModelToEntity : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<StudentModel, StudentProfile>();
                //.ForMember(x => x.Person,
                //    opt =>
                //        opt.ResolveUsing(
                //            model =>
                //                new Person
                //                {
                //                    IdentityNumber = model.IdentityNumber,
                //                    FirstName = model.FirstName,
                //                    LastName = model.LastName,
                //                    DateOfBirth = model.DateOfBirth,
                //                    Address = model.Address,
                //                    RowVersion = model.PersonRowVersion
                //                }));

            Mapper.CreateMap<TeacherModel, InstructorProfile>();
                //.ForMember(x => x.Person,
                //    opt =>
                //        opt.ResolveUsing(
                //            model =>
                //                new Person
                //                {
                //                    IdentityNumber = model.IdentityNumber,
                //                    FirstName = model.FirstName,
                //                    LastName = model.LastName,
                //                    DateOfBirth = model.DateOfBirth,
                //                    Address = model.Address,
                //                    RowVersion = model.PersonRowVersion
                //                }));
            Mapper.CreateMap<PersonModel, Person>();
            Mapper.CreateMap<CourseModel, Course>();
            Mapper.CreateMap<DepartmentModel, Department>();
            Mapper.CreateMap<EnrollmentModel, Enrollment>()
                .ForMember(x => x.InstructorProfile, opt => opt.MapFrom(x => x.Teacher))
                .ForMember(x => x.StudentProfile, opt => opt.MapFrom(x => x.Student))
                .ForMember(x => x.StudentProfileId, opt => opt.MapFrom(x => x.StudentId))
                .ForMember(x => x.InstructorProfileId, opt => opt.MapFrom(x => x.TeacherId));
            Mapper.CreateMap<OfficeAssignmentModel, OfficeAssignment>();
        }
    }
}
