using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.BLL.Entities
{
    public class Announcement
    {
        [Key, ForeignKey("Image")]
        public Guid Id { get; set; }

        [Column("Title")]
        public String Title { get; set; }

        [Column("Photo")]
        public String PhotoUrl { get; set; }

        [Column("Description")]
        public String Description { get; set; }

        [Column("Price")]
        public Decimal Price { get; set; }

        [Column("Actived")]
        public bool Actived { get; set; }

        [Column("CreationDate")]
        public DateTime? CreationDate { get; set; }

        [Column("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        public virtual Image Image { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid MarketplaceId { get; set; }
        public virtual Marketplace Marketplace { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }

        public Announcement()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }
    }
}
