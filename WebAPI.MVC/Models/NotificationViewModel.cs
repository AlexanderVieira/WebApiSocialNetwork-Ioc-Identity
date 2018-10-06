using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.MVC.Models
{
    public class NotificationViewModel
    {        
        public Guid Id { get; set; }        
        public Guid NotificationIssuerId { get; set; }       
        public Guid NotifiedProfileId { get; set; }
        public string Message { get; set; }
        public DateTime NotificationTime { get; set; }
        public bool WasRead { get; set; }
        public virtual ProfileViewModel NotificationIssuer { get; set; }
        public virtual ProfileViewModel NotifiedProfile { get; set; }
    }
}