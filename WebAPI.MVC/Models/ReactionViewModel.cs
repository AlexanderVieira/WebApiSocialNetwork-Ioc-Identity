using System;

namespace WebAPI.MVC.Models
{
    public class ReactionViewModel
    {
        public Guid Id { get; set; }        
        public Guid ReactionOwnerId { get; set; }        
        public Guid ReactionPostId { get; set; }
        public string Text { get; set; }
        public string IconUri { get; set; }
        public virtual ProfileViewModel ReactionOwner { get; set; }
        public virtual PostViewModel ReactionPost { get; set; }
    }
}