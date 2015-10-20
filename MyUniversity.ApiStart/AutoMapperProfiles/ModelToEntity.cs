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
            Mapper.CreateMap<InstructorModel, InstructorProfile>();
            Mapper.CreateMap<CourseModel, Course>();
            Mapper.CreateMap<DepartmentModel, Department>();
            Mapper.CreateMap<EnrollmentModel, Enrollment>();
            Mapper.CreateMap<OfficeAssignmentModel, OfficeAssignment>();
        }
    }
}
