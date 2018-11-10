namespace AuthorizeNetSample.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Street = c.String(nullable: false),
                        City = c.String(),
                        State = c.String(),
                        ZIP = c.String(nullable: false, maxLength: 6),
                        Country = c.String(),
                        Phone = c.String(),
                        CustomerId = c.Guid(),
                        CreditCardId = c.Guid(),
                        DateAdded = c.DateTime(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CreditCards", t => t.CreditCardId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId)
                .Index(t => t.CreditCardId);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastFourDigits = c.String(nullable: false, maxLength: 4),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        CardNumHash = c.String(nullable: false),
                        ExpDate = c.String(nullable: false, maxLength: 4),
                        CustomerId = c.Guid(nullable: false),
                        DateAdded = c.DateTime(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DateAdded = c.DateTime(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TransactionId = c.String(),
                        AuthKey = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerId = c.Guid(nullable: false),
                        DateAdded = c.DateTime(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.AuthorizeConfigs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ClientId = c.String(),
                        ClientSecret = c.String(),
                        AccessToken = c.String(),
                        RefreshToken = c.String(),
                        RedirectUri = c.String(),
                        AccesssTokenExpiresIn = c.DateTime(),
                        RefreshTokenExpiresIn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditCards", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Payments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Addresses", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Addresses", "CreditCardId", "dbo.CreditCards");
            DropIndex("dbo.Payments", new[] { "CustomerId" });
            DropIndex("dbo.CreditCards", new[] { "CustomerId" });
            DropIndex("dbo.Addresses", new[] { "CreditCardId" });
            DropIndex("dbo.Addresses", new[] { "CustomerId" });
            DropTable("dbo.AuthorizeConfigs");
            DropTable("dbo.Payments");
            DropTable("dbo.Customers");
            DropTable("dbo.CreditCards");
            DropTable("dbo.Addresses");
        }
    }
}
