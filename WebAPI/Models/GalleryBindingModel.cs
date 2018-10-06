using System;
using System.Collections.Generic;
using WebAPI.BLL.Entities;

namespace WebAPI.Models
{
    public class GalleryBindingModel
    {
        public Guid Id { get; set; }        
        public String Title { get; set; }        
        public string PhotoUrl { get; set; }        
        public DateTime? CreationDate { get; set; }        
        public DateTime? UpdateDate { get; set; }        
        public Guid ProfileId { get; set; }
        public virtual ProfileBindingModel Profile { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}