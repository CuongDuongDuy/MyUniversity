using AutoMapper;
using MyUniversity.ApiStart.AutoMapperProfiles;

namespace MyUniversity.ApiStart
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new EntityToModel());
                cfg.AddProfile(new ModelToEntity());

            });
        }
    }
}
