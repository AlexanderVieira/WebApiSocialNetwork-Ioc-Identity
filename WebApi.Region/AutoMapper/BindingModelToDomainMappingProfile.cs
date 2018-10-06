using AutoMapper;
using WebApi.Region.Models;
using WebAPI.Domain.Entities;

namespace WebApi.Region.AutoMapper
{
    public class BindingModelToDomainMappingProfile : global::AutoMapper.Profile
    {
        public override string ProfileName
        {
            get { return "BindingModelToDomainMappings"; }
        }
        protected override void Configure()
        {           
            Mapper.CreateMap<CountryBindingModel, Country>();
            Mapper.CreateMap<StateBindingModel, State>();
            Mapper.CreateMap<AddressBindingModel, Address>();
        }
    }
}