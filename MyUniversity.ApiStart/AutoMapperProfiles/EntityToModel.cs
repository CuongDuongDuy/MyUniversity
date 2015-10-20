using AutoMapper;
using MyUniversity.Contracts.Models;
using MyUniversity.Dal.Entities;
using Profile = AutoMapper.Profile;

namespace MyUniversity.ApiStart.AutoMapperProfiles
{
    internal class EntityToModel : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<StudentProfile, StudentModel>();
            Mapper.CreateMap<InstructorProfile, InstructorModel>();
            Mapper.CreateMap<Course, CourseModel>();
            Mapper.CreateMap<Department, DepartmentModel>();
            Mapper.CreateMap<Enrollment, EnrollmentModel>();
            Mapper.CreateMap<OfficeAssignment, OfficeAssignmentModel>();
        }
    }
}
