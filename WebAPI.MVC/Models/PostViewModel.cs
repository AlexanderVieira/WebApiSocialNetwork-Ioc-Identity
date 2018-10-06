using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.MVC.Models
{
    public class PostViewModel
    {       
        public Guid Id { get; set; }        
        public Guid AuthorId { get; set; }
        public string Content { get; set; }
        public string ImageUri { get; set; }
        public DateTime PostTime { get; set; }
        public virtual ProfileViewModel Author { get; set; }
        public virtual ICollection<ReactionViewModel> Reactions { get; set; }
    }
}