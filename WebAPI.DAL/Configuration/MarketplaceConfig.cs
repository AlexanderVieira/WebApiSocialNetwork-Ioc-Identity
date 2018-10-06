using System.Data.Entity.ModelConfiguration;
using WebAPI.BLL.Entities;

namespace WebAPI.DAL.Configuration
{
    public class MarketplaceConfig : EntityTypeConfiguration<Marketplace>
    {
        public MarketplaceConfig()
        {
            HasKey(m => m.Id);

            Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(150);            
        }
    }
}
