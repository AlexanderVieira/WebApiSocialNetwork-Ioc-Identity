using System;
using System.Collections.Generic;
using WebApi.Models;
using WebAPI.BLL.Entities;

namespace WebAPI.Models
{
    public class ProfileBindingModel
    {
        public Guid Id { get; set; }        
        public String FirstName { get; set; }       
        public String LastName { get; set; }        
        public String Email { get; set; }        
        public DateTime? Birthday { get; set; }        
        public String PhotoUrl { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
        public Guid StateId { get; set; }
        public virtual State State { get; set; }
        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual Marketplace Marketplace { get; set; }
        public virtual ICollection<FriendShipBindingModel> Friends { get; set; }
        public virtual ICollection<FriendShipBindingModel> FriendShips { get; set; }
        public virtual ICollection<GroupBindingModel> Groups { get; set; }
        public virtual ICollection<GalleryBindingModel> Galleries { get; set; }
        public virtual ICollection<ImageBindingModel> Images { get; set; }
        public virtual ICollection<AnnouncementBindingModel> Announcements { get; set; }
        public virtual ICollection<NotificationBindingModel> Notifications { get; set; }
        public virtual ICollection<PostBindingModel> Posts { get; set; }
        public virtual ICollection<ReactionBindingModel> Reactions { get; set; }
    }
}