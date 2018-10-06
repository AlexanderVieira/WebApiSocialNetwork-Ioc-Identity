using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Models
{
    public class PostBindingModel
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Content { get; set; }
        public string ImageUri { get; set; }
        public DateTime PostTime { get; set; }
        public virtual ProfileBindingModel Author { get; set; }
        public virtual ICollection<ReactionBindingModel> Reactions { get; set; }
    }
}