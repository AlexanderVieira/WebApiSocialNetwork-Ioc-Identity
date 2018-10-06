using System.Data.Entity.ModelConfiguration;
using WebAPI.BLL.Entities;

namespace WebAPI.DAL.Configuration
{
    public class ProfileConfig : EntityTypeConfiguration<Profile>
    {
        public ProfileConfig()
        {
            HasKey(p => p.Id);

            Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(150);

            Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(150);

            Property(p => p.PhotoUrl)
                .HasMaxLength(255);            
        }
    }
}
