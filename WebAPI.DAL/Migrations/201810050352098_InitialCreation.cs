namespace WebAPI.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Logradouro = c.String(maxLength: 100, unicode: false),
                        Numero = c.Int(nullable: false),
                        Bairro = c.String(maxLength: 100, unicode: false),
                        Cidade = c.String(maxLength: 100, unicode: false),
                        Cep = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profile", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 150, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 150, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Birthday = c.DateTime(),
                        Photo = c.String(maxLength: 255, unicode: false),
                        Group_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Group", t => t.Group_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Announcement",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 150, unicode: false),
                        Photo = c.String(maxLength: 100, unicode: false),
                        Description = c.String(maxLength: 255, unicode: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Actived = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        MarketplaceId = c.Guid(nullable: false),
                        ProfileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Image", t => t.Id)
                .ForeignKey("dbo.Marketplace", t => t.MarketplaceId)
                .ForeignKey("dbo.Profile", t => t.ProfileId)
                .Index(t => t.Id)
                .Index(t => t.MarketplaceId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Photo = c.String(maxLength: 255, unicode: false),
                        Description = c.String(maxLength: 255, unicode: false),
                        CreationDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        GalleryId = c.Guid(nullable: false),
                        ProfileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gallery", t => t.GalleryId)
                .ForeignKey("dbo.Profile", t => t.ProfileId)
                .Index(t => t.GalleryId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Gallery",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 150, unicode: false),
                        Photo = c.String(maxLength: 255, unicode: false),
                        Creation_Date = c.DateTime(),
                        Update_Date = c.DateTime(),
                        ProfileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profile", t => t.ProfileId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Marketplace",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 150, unicode: false),
                        CreationDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        ProfileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profile", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Bandeira = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profile", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.FriendShip",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RequestedById = c.Guid(nullable: false),
                        RequestedToId = c.Guid(nullable: false),
                        RequestTime = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Profile_Id = c.Guid(),
                        Profile_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profile", t => t.RequestedById)
                .ForeignKey("dbo.Profile", t => t.RequestedToId)
                .ForeignKey("dbo.Profile", t => t.Profile_Id)
                .ForeignKey("dbo.Profile", t => t.Profile_Id1)
                .Index(t => t.RequestedById)
                .Index(t => t.RequestedToId)
                .Index(t => t.Profile_Id)
                .Index(t => t.Profile_Id1);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100, unicode: false),
                        CreationDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        Actived = c.Boolean(nullable: false),
                        ProfileId = c.Guid(nullable: false),
                        Profile_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profile", t => t.ProfileId)
                .ForeignKey("dbo.Profile", t => t.Profile_Id)
                .Index(t => t.ProfileId)
                .Index(t => t.Profile_Id);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        NotificationIssuerId = c.Guid(nullable: false),
                        NotifiedProfileId = c.Guid(nullable: false),
                        Message = c.String(maxLength: 100, unicode: false),
                        NotificationTime = c.DateTime(),
                        WasRead = c.Boolean(nullable: false),
                        Profile_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profile", t => t.NotificationIssuerId)
                .ForeignKey("dbo.Profile", t => t.NotifiedProfileId)
                .ForeignKey("dbo.Profile", t => t.Profile_Id)
                .Index(t => t.NotificationIssuerId)
                .Index(t => t.NotifiedProfileId)
                .Index(t => t.Profile_Id);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AuthorId = c.Guid(nullable: false),
                        Content = c.String(maxLength: 100, unicode: false),
                        ImageUri = c.String(maxLength: 100, unicode: false),
                        PostTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profile", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Reaction",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ReactionOwnerId = c.Guid(nullable: false),
                        ReactionPostId = c.Guid(nullable: false),
                        Text = c.String(maxLength: 100, unicode: false),
                        IconUri = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profile", t => t.ReactionOwnerId)
                .ForeignKey("dbo.Post", t => t.ReactionPostId)
                .Index(t => t.ReactionOwnerId)
                .Index(t => t.ReactionPostId);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Bandeira = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profile", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "Id", "dbo.Profile");
            DropForeignKey("dbo.State", "Id", "dbo.Profile");
            DropForeignKey("dbo.Reaction", "ReactionPostId", "dbo.Post");
            DropForeignKey("dbo.Reaction", "ReactionOwnerId", "dbo.Profile");
            DropForeignKey("dbo.Post", "AuthorId", "dbo.Profile");
            DropForeignKey("dbo.Notification", "Profile_Id", "dbo.Profile");
            DropForeignKey("dbo.Notification", "NotifiedProfileId", "dbo.Profile");
            DropForeignKey("dbo.Notification", "NotificationIssuerId", "dbo.Profile");
            DropForeignKey("dbo.Group", "Profile_Id", "dbo.Profile");
            DropForeignKey("dbo.Group", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.Profile", "Group_Id", "dbo.Group");
            DropForeignKey("dbo.FriendShip", "Profile_Id1", "dbo.Profile");
            DropForeignKey("dbo.FriendShip", "Profile_Id", "dbo.Profile");
            DropForeignKey("dbo.FriendShip", "RequestedToId", "dbo.Profile");
            DropForeignKey("dbo.FriendShip", "RequestedById", "dbo.Profile");
            DropForeignKey("dbo.Country", "Id", "dbo.Profile");
            DropForeignKey("dbo.Announcement", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.Marketplace", "Id", "dbo.Profile");
            DropForeignKey("dbo.Announcement", "MarketplaceId", "dbo.Marketplace");
            DropForeignKey("dbo.Announcement", "Id", "dbo.Image");
            DropForeignKey("dbo.Image", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.Gallery", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.Image", "GalleryId", "dbo.Gallery");
            DropIndex("dbo.State", new[] { "Id" });
            DropIndex("dbo.Reaction", new[] { "ReactionPostId" });
            DropIndex("dbo.Reaction", new[] { "ReactionOwnerId" });
            DropIndex("dbo.Post", new[] { "AuthorId" });
            DropIndex("dbo.Notification", new[] { "Profile_Id" });
            DropIndex("dbo.Notification", new[] { "NotifiedProfileId" });
            DropIndex("dbo.Notification", new[] { "NotificationIssuerId" });
            DropIndex("dbo.Group", new[] { "Profile_Id" });
            DropIndex("dbo.Group", new[] { "ProfileId" });
            DropIndex("dbo.FriendShip", new[] { "Profile_Id1" });
            DropIndex("dbo.FriendShip", new[] { "Profile_Id" });
            DropIndex("dbo.FriendShip", new[] { "RequestedToId" });
            DropIndex("dbo.FriendShip", new[] { "RequestedById" });
            DropIndex("dbo.Country", new[] { "Id" });
            DropIndex("dbo.Marketplace", new[] { "Id" });
            DropIndex("dbo.Gallery", new[] { "ProfileId" });
            DropIndex("dbo.Image", new[] { "ProfileId" });
            DropIndex("dbo.Image", new[] { "GalleryId" });
            DropIndex("dbo.Announcement", new[] { "ProfileId" });
            DropIndex("dbo.Announcement", new[] { "MarketplaceId" });
            DropIndex("dbo.Announcement", new[] { "Id" });
            DropIndex("dbo.Profile", new[] { "Group_Id" });
            DropIndex("dbo.Address", new[] { "Id" });
            DropTable("dbo.State");
            DropTable("dbo.Reaction");
            DropTable("dbo.Post");
            DropTable("dbo.Notification");
            DropTable("dbo.Group");
            DropTable("dbo.FriendShip");
            DropTable("dbo.Country");
            DropTable("dbo.Marketplace");
            DropTable("dbo.Gallery");
            DropTable("dbo.Image");
            DropTable("dbo.Announcement");
            DropTable("dbo.Profile");
            DropTable("dbo.Address");
        }
    }
}
