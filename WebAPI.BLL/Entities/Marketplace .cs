using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.Entities
{
    public class Marketplace
    {
        [Key, ForeignKey("Profile")]
        public Guid Id { get; set; }

        [Column("Title")]
        public String Title { get; set; }

        [Column("CreationDate")]
        public DateTime? CreationDate { get; set; }

        [Column("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }

        public Marketplace()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            this.Announcements = new HashSet<Announcement>();
        }
    }
}
