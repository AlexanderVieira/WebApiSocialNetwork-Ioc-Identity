using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using WebAPI.DataAccess.Configuration;
using WebAPI.Domain.Entities;

namespace WebAPI.DataAccess.RegionContext
{
    public class CtxRegion : DbContext
    {
        public CtxRegion() : base("RegionConnStr")
        {

        }       
        
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<String>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<String>()
                .Configure(p => p.HasMaxLength(100));
           
            modelBuilder.Configurations.Add(new CountryConfig());
            modelBuilder.Configurations.Add(new StateConfig());
            modelBuilder.Configurations.Add(new AddressConfig());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(Entry => Entry.Entity.GetType().GetProperty("CreationDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreationDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreationDate").IsModified = false;
                }

                if (entry.State == EntityState.Added)
                {
                    entry.Property("UpdateDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdateDate").IsModified = true;
                }

            }
            return base.SaveChanges();
        }

    }
}