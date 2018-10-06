using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.BLL.Entities;

namespace WebAPI.Models
{
    public class GroupBindingModel
    {
        public Guid Id { get; set; }        
        public String Name { get; set; }        
        public DateTime? CreationDate { get; set; }       
        public DateTime? UpdateDate { get; set; }        
        public bool Actived { get; set; }        
        public Guid ProfileId { get; set; }
        public virtual ProfileBindingModel Profile { get; set; }
        public virtual ICollection<ProfileBindingModel> Friends { get; set; }
    }
}