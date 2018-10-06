using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.Entities
{
    public class Profile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Column("FirstName")]
        public String FirstName { get; set; }

        [Column("LastName")]
        public String LastName { get; set; }

        [Column("Email")]
        public String Email { get; set; }

        [Column("Birthday")]
        public DateTime? Birthday { get; set; }

        [Column("Photo")]
        public String PhotoUrl { get; set; }
        public virtual Country Country { get; set; } 
        public virtual State State { get; set; }        
        public virtual Address Address { get; set; }
        public virtual Marketplace Marketplace { get; set; }
        public virtual ICollection<FriendShip> Friends { get; set; }
        public virtual ICollection<FriendShip> FriendShips { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; }

        public Profile()
        {
            //Id = Guid.NewGuid();
            this.Friends = new HashSet<FriendShip>();
            this.FriendShips = new HashSet<FriendShip>();
            this.Groups = new HashSet<Group>();
            this.Galleries = new HashSet<Gallery>();
            this.Images = new HashSet<Image>();
            this.Announcements = new HashSet<Announcement>();
            this.Notifications = new HashSet<Notification>();
            this.Posts = new HashSet<Post>();
            this.Reactions = new HashSet<Reaction>();
        }
    }
}