using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;

namespace WebAPI.DAL.Configuration
{
    class GalleryConfig : EntityTypeConfiguration<Gallery>
    {
        public GalleryConfig()
        {
            HasKey(ga => ga.Id);

            Property(ga => ga.Title)
                .IsRequired()
                .HasMaxLength(150);

            Property(ga => ga.PhotoUrl)
                .HasMaxLength(255);

        }
    }
}
