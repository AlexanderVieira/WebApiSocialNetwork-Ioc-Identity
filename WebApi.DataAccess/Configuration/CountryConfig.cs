using System.Data.Entity.ModelConfiguration;
using WebAPI.Domain.Entities;

namespace WebAPI.DataAccess.Configuration
{
    public class CountryConfig : EntityTypeConfiguration<Country>
    {
        public CountryConfig()
        {
            HasKey(c => c.Id);

            Property(c => c.Name)                
                .HasMaxLength(150);

            Property(c => c.Flag)
                .HasMaxLength(255);
        }
    }
}
