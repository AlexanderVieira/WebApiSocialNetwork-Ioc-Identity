using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.Entities
{
    public class Group
    {        
        [Key]
        public Guid Id { get; set; }

        [Column("Name")]
        public String Name { get; set; }

        [Column("CreationDate")]
        public DateTime? CreationDate { get; set; }

        [Column("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [Column("Actived")]
        public bool Actived { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<Profile> Friends { get; set; }             
        
        public Group()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            this.Friends = new HashSet<Profile>();
        }
    }
}
