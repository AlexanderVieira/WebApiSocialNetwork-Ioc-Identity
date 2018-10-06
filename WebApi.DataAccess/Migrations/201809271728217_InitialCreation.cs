namespace WebApi.DataAccess.Migrations
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
                        Street = c.String(maxLength: 100, unicode: false),
                        Number = c.Int(nullable: false),
                        Neighborhood = c.String(maxLength: 100, unicode: false),
                        City = c.String(maxLength: 100, unicode: false),
                        PostalCode = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 150, unicode: false),
                        Flag = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 150, unicode: false),
                        Flag = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.State");
            DropTable("dbo.Country");
            DropTable("dbo.Address");
        }
    }
}
