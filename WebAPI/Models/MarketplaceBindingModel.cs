using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.BLL.Entities;

namespace WebAPI.Models
{
    public class MarketplaceBindingModel
    {
        public Guid Id { get; set; }        
        public String Title { get; set; }        
        public DateTime? CreationDate { get; set; }        
        public DateTime? UpdateDate { get; set; }        
        public Guid ProfileId { get; set; }
        public virtual ProfileBindingModel Profile { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
    }
}