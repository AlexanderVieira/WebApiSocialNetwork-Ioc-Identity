using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class FriendShipBindingModel
    {        
        public Guid Id { get; set; }        
        public Guid RequestedById { get; set; }        
        public Guid RequestedToId { get; set; }
        public DateTime? RequestTime { get; set; }
        public StatusEnumBindingModel Status { get; set; }
        public virtual ProfileBindingModel RequestedBy { get; set; }
        public virtual ProfileBindingModel RequestedTo { get; set; }
    }

    public enum StatusEnumBindingModel
    {
        Pendent = 0,
        Accepted = 1,
        Rejected = 2,
        Blocked = 3
    };
}