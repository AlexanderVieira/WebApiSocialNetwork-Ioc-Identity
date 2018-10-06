using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ReactionBindingModel
    {
        public Guid Id { get; set; }
        public Guid ReactionOwnerId { get; set; }
        public Guid ReactionPostId { get; set; }
        public string Text { get; set; }
        public string IconUri { get; set; }
        public virtual ProfileBindingModel ReactionOwner { get; set; }
        public virtual PostBindingModel ReactionPost { get; set; }
    }
}