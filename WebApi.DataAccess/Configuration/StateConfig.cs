using System.Data.Entity.ModelConfiguration;
using WebAPI.Domain.Entities;

namespace WebAPI.DataAccess.Configuration
{
    public class StateConfig : EntityTypeConfiguration<State>
    {
        public StateConfig()
        {
            HasKey(s => s.Id);

            Property(s => s.Name)                
                .HasMaxLength(150);            

            Property(s => s.Flag)
                .HasMaxLength(255);
        }
    }
}
