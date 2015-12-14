﻿using AutoMapper;
using MyUniversity.Contracts.Models;
using MyUniversity.Dal.Entities;
using Profile = AutoMapper.Profile;

namespace MyUniversity.ApiStart.AutoMapperProfiles
{
    internal class ModelToEntity : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<StudentModel, StudentProfile>()
                .ForMember(x => x.Person,
                    opt =>
                        opt.ResolveUsing(
                            model =>
                                new Person
                                {
                                    IdentityNumber = model.IdentityNumber,
                                    FirstName = model.FirstName,
                                    LastName = model.LastName,
                                    DateOfBirth = model.DateOfBirth,
                                    Address = model.Address
                                }));

            Mapper.CreateMap<TeacherModel, InstructorProfile>()
                .ForMember(x => x.Person,
                    opt =>
                        opt.ResolveUsing(
                            model =>
                                new Person
                                {
                                    IdentityNumber = model.IdentityNumber,
                                    FirstName = model.FirstName,
                                    LastName = model.LastName,
                                    DateOfBirth = model.DateOfBirth,
                                    Address = model.Address
                                }));
            Mapper.CreateMap<CourseModel, Course>();
            Mapper.CreateMap<DepartmentModel, Department>();
            Mapper.CreateMap<EnrollmentModel, Enrollment>();
            Mapper.CreateMap<OfficeAssignmentModel, OfficeAssignment>();
        }
    }
}
