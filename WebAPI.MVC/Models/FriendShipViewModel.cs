using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.MVC.Models
{
    public class FriendShipViewModel
    {
        public Guid Id { get; set; }
        public Guid RequestedById { get; set; }
        public Guid RequestedToId { get; set; }
        public DateTime? RequestTime { get; set; }
        public StatusEnumViewModel Status { get; set; }
        public virtual ProfileViewModel RequestedBy { get; set; }
        public virtual ProfileViewModel RequestedTo { get; set; }
    }
    public enum StatusEnumViewModel
    {
        Pendent = 0,
        Accepted = 1,
        Rejected = 2,
        Blocked = 3
    };
}