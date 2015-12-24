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
                //.ForMember(x => x.IdentityNumber, opt => opt.MapFrom(x => x.Person.IdentityNumber))
                //.ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Person.FirstName))
                //.ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Person.LastName))
                //.ForMember(x => x.DateOfBirth, opt => opt.MapFrom(x => x.Person.DateOfBirth))
                //.ForMember(x => x.Address, opt => opt.MapFrom(x => x.Person.Address))
                //.ForMember(x => x.PersonRowVersion, opt => opt.MapFrom(x => x.Person.RowVersion));

            Mapper.CreateMap<InstructorProfile, TeacherModel>();
                //.ForMember(x => x.IdentityNumber, opt => opt.MapFrom(x => x.Person.IdentityNumber))
                //.ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Person.FirstName))
                //.ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Person.LastName))
                //.ForMember(x => x.DateOfBirth, opt => opt.MapFrom(x => x.Person.DateOfBirth))
                //.ForMember(x => x.Address, opt => opt.MapFrom(x => x.Person.Address))
                //.ForMember(x => x.PersonRowVersion, opt => opt.MapFrom(x => x.Person.RowVersion));
            Mapper.CreateMap<Course, CourseModel>();
            Mapper.CreateMap<Department, DepartmentModel>();
            Mapper.CreateMap<Enrollment, EnrollmentModel>();
            Mapper.CreateMap<OfficeAssignment, OfficeAssignmentModel>();
        }
    }
}
