using System;
using System.Linq.Expressions;
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
            Mapper.CreateMap<Expression<Func<StudentModel, bool>>, Expression<Func<StudentProfile, bool>>>();

        }
    }
}
