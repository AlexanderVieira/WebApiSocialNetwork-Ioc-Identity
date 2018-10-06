using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.BLL.Entities;

namespace WebAPI.Models
{
    public class AnnouncementBindingModel
    {
        public Guid Id { get; set; }       
        public String Title { get; set; }        
        public String PhotoUrl { get; set; }        
        public String Description { get; set; }        
        public Decimal Price { get; set; }        
        public bool Actived { get; set; }        
        public DateTime? CreationDate { get; set; }        
        public DateTime? UpdateDate { get; set; }
        public virtual ImageBindingModel Image { get; set; }        
        public Guid MarketplaceId { get; set; }
        public virtual MarketplaceBindingModel Marketplace { get; set; }        
        public Guid ProfileId { get; set; }
        public virtual ProfileBindingModel Profile { get; set; }
    }
}