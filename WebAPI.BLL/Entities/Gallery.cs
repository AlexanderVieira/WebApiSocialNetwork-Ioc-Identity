using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.Entities
{
    public class Gallery
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Title")]
        public String Title { get; set; }

        [Column("Photo")]
        public string PhotoUrl { get; set; }

        [Column("Creation_Date")]
        public DateTime? CreationDate { get; set; }

        [Column("Update_Date")]
        public DateTime? UpdateDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }              
        public virtual ICollection<Image> Images { get; set; }

        public Gallery()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            this.Images = new HashSet<Image>();
        }
    }
}
