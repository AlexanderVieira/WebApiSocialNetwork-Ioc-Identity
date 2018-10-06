using AutoMapper;

namespace WebApi.Region.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToBindingModelMappingProfile>();
                x.AddProfile<BindingModelToDomainMappingProfile>();
            });
        }
    }
}