using AutoMapper;
using WebApi.Models;
using WebAPI.BLL.Entities;
using WebAPI.Models;

namespace WebAPI.AutoMapper
{
    public class DomainToBindingModelMappingProfile : global::AutoMapper.Profile
    {
        public override string ProfileName
        {
            get { return "DomainToBindingModelMappings"; }
        }
        protected override void Configure()
        {
            Mapper.CreateMap<BLL.Entities.Profile, ProfileBindingModel>();
            Mapper.CreateMap<FriendShip, FriendShipBindingModel>();           
            Mapper.CreateMap<Group, GroupBindingModel>();
            Mapper.CreateMap<Gallery, GalleryBindingModel>();
            Mapper.CreateMap<Image, ImageBindingModel>();
            Mapper.CreateMap<Marketplace, MarketplaceBindingModel>();
            Mapper.CreateMap<Announcement, AnnouncementBindingModel>();
            Mapper.CreateMap<Country, CountryBindingModel>();
            Mapper.CreateMap<State, StateBindingModel>();
            Mapper.CreateMap<Address, AddressBindingModel>();
            Mapper.CreateMap<Post, PostBindingModel > ();
            Mapper.CreateMap<Notification, NotificationBindingModel>();
        }
    }
}