using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using WebAPI.BLL.Entities;
using WebAPI.DAL.Configuration;

namespace WebAPI.DAL.ApiContext
{
    public class EFContext : DbContext
    {
        public EFContext() : base("LettuceConnStr")
        {

        }
        
        public DbSet<FriendShip> FriendShips { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Marketplace> Marketplaces { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reaction> Reactions { get; set; }



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

            //modelBuilder.Entity<Profile>()
            //    .HasMany(e => e.FriendShips)
            //    .WithRequired(e => e.RequestedTo)
            //    .HasForeignKey(e => e.RequestedToId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Profile>()
            //    .HasMany(e => e.Friends)
            //    .WithRequired(e => e.RequestedBy)
            //    .HasForeignKey(e => e.RequestedById)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Notification>()
            //    .HasRequired(n => n.NotifiedProfile)
            //    .WithMany(p => p.Notifications)
            //    .HasForeignKey(n => n.NotifiedProfileId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Post>()
            //    .HasRequired(p => p.Author)
            //    .WithMany(p => p.Posts)
            //    .HasForeignKey(p => p.AuthorId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Reaction>()
            //    .HasRequired(r => r.ReactionOwner)
            //    .WithMany(p => p.Reactions)
            //    .HasForeignKey(r => r.ReactionOwnerId)
            //    .WillCascadeOnDelete(false);
            
            //modelBuilder.Configurations.Add(new FriendShipConfig());
            modelBuilder.Configurations.Add(new GroupConfig());
            modelBuilder.Configurations.Add(new ProfileConfig());
            modelBuilder.Configurations.Add(new MarketplaceConfig());
            modelBuilder.Configurations.Add(new AnnouncementConfig());
            modelBuilder.Configurations.Add(new GalleryConfig());
            modelBuilder.Configurations.Add(new ImageConfig());
            modelBuilder.Configurations.Add(new CountryConfig());
            modelBuilder.Configurations.Add(new StateConfig());
            modelBuilder.Configurations.Add(new AddressConfig());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(Entry => Entry
                                               .Entity.GetType().GetProperty("CreationDate") != null))
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