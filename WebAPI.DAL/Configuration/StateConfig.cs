using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;

namespace WebAPI.DAL.Configuration
{
    public class StateConfig : EntityTypeConfiguration<State>
    {
        public StateConfig()
        {
            HasKey(s => s.Id);

            Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(150);            

            Property(s => s.Flag)
                .HasMaxLength(255);
        }
    }
}
