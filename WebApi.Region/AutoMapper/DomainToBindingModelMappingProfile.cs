using AutoMapper;
using WebApi.Region.Models;
using WebAPI.Domain.Entities;

namespace WebApi.Region.AutoMapper
{
    public class DomainToBindingModelMappingProfile : global::AutoMapper.Profile
    {
        public override string ProfileName
        {
            get { return "DomainToBindingModelMappings"; }
        }
        protected override void Configure()
        {            
            Mapper.CreateMap<Country, CountryBindingModel>();
            Mapper.CreateMap<State, StateBindingModel>();
            Mapper.CreateMap<Address, AddressBindingModel>();
        }
    }
}