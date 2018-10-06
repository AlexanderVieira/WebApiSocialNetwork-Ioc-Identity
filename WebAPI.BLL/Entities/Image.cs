using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.BLL.Entities
{
    public class Image
    {        
        [Key]
        public Guid Id { get; set; }

        [Column("Photo")]
        public string PhotoUrl { get; set; }        

        [Column("Description")]
        public String Description { get; set; }

        [Column("CreationDate")]
        public DateTime? CreationDate { get; set; }

        [Column("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GalleryId { get; set; }
        public virtual Gallery Gallery { get; set; }
        public virtual Announcement Announcement { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }

        public Image()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }
    }
}
