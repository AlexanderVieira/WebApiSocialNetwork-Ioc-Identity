using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.BLL.Entities;

namespace WebAPI.Models
{
    public class ImageBindingModel
    {
        public Guid Id { get; set; }       
        public string PhotoUrl { get; set; }        
        public String Description { get; set; }        
        public DateTime? CreationDate { get; set; }        
        public DateTime? UpdateDate { get; set; }        
        public Guid GalleryId { get; set; }
        public virtual Gallery Gallery { get; set; }
        public virtual Announcement Announcement { get; set; }        
        public Guid ProfileId { get; set; }
        public virtual ProfileBindingModel Profile { get; set; }
    }
}