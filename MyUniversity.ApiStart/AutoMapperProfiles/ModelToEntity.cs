using System;
using System.Linq.Expressions;
using AutoMapper;
using MyUniversity.Contracts.Models;
using MyUniversity.Dal.Entities;

namespace MyUniversity.ApiStart.AutoMapperProfiles
{
    internal class ModelToEntity : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<StudentModel, Student>();
            Mapper.CreateMap<Expression<Func<StudentModel, bool>>, Expression<Func<Student, bool>>>();
            
            Mapper.CreateMap<StudentProfileModel, AccountProfile>();
            Mapper.CreateMap<Expression<Func<StudentProfileModel, bool>>, Expression<Func<AccountProfile, bool>>>();

        }
    }
}
