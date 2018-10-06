using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;

namespace WebAPI.DAL.Configuration
{
    public class AnnouncementConfig : EntityTypeConfiguration<Announcement>
    {
        public AnnouncementConfig()
        {
            HasKey(a => a.Id);

            Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(150);

            Property(a => a.Description)
                .HasMaxLength(255);
            
        }
    }
}
