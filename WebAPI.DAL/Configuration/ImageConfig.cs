using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;

namespace WebAPI.DAL.Configuration
{
    public class ImageConfig : EntityTypeConfiguration<Image>
    {
        public ImageConfig()
        {
            HasKey(i => i.Id);

            Property(i => i.Description)
                .HasMaxLength(255);

            Property(i => i.PhotoUrl)
                .HasMaxLength(255);
        }
    }
}
