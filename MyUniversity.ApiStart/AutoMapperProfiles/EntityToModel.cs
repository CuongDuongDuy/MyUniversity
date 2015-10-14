using AutoMapper;
using MyUniversity.Contracts.Models;
using MyUniversity.Dal.Entities;

namespace MyUniversity.ApiStart.AutoMapperProfiles
{
    internal class EntityToModel : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Student, StudentModel>();
            Mapper.CreateMap<AccountProfile, StudentModel>();
        }
    }
}
