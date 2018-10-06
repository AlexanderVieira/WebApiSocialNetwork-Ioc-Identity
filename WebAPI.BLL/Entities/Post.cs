using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.Entities
{
    public class Post
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AuthorId { get; set; }
        public string Content { get; set; }
        public string ImageUri { get; set; }
        public DateTime? PostTime { get; set; }
        public virtual Profile Author { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; }

        public Post()
        {
            Id = Guid.NewGuid();
        }

        public Post(string content, string imageUri, DateTime postTime, Profile author)
        {            
            Content = content;
            ImageUri = imageUri;
            PostTime = postTime;
            Author = author;
        }
    }
}
