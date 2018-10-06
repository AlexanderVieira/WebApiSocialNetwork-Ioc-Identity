using AutoMapper;
using WebApi.Models;
using WebAPI.BLL.Entities;
using WebAPI.Models;

namespace WebAPI.AutoMapper
{
    public class BindingModelToDomainMappingProfile : global::AutoMapper.Profile
    {
        public override string ProfileName
        {
            get { return "BindingModelToDomainMappings"; }
        }
        protected override void Configure()
        {
            Mapper.CreateMap<ProfileBindingModel, BLL.Entities.Profile>();
            Mapper.CreateMap<FriendShipBindingModel, FriendShip>();            
            Mapper.CreateMap<GroupBindingModel, Group>();
            Mapper.CreateMap<GalleryBindingModel, Gallery>();
            Mapper.CreateMap<ImageBindingModel, Image>();
            Mapper.CreateMap<MarketplaceBindingModel, Marketplace>();
            Mapper.CreateMap<AnnouncementBindingModel, Announcement>();
            Mapper.CreateMap<CountryBindingModel, Country>();
            Mapper.CreateMap<StateBindingModel, State>();
            Mapper.CreateMap<AddressBindingModel, Address>();
            Mapper.CreateMap<PostBindingModel, Post>();
            Mapper.CreateMap<NotificationBindingModel, Notification>();
        }
    }
}