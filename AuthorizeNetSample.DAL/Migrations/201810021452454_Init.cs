namespace AuthorizeNetSample.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastFourDigits = c.String(nullable: false, maxLength: 4),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        CardNumHash = c.String(nullable: false),
                        ExpDate = c.String(nullable: false, maxLength: 4),
                        CustomerId = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(nullable: false),
                        City = c.String(),
                        State = c.String(),
                        ZIP = c.String(nullable: false, maxLength: 6),
                        Country = c.String(),
                        Phone = c.String(),
                        CustomerId = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                        LastModified = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.CreditCards", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                        Id = c.Int(nullable: false, identity: true),
                        TransactionId = c.String(),
                        AuthKey = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerId = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditCards", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Addresses", "Id", "dbo.CreditCards");
            DropForeignKey("dbo.Payments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Addresses", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Payments", new[] { "CustomerId" });
            DropIndex("dbo.Addresses", new[] { "CustomerId" });
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropIndex("dbo.CreditCards", new[] { "CustomerId" });
            DropTable("dbo.Payments");
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
            DropTable("dbo.CreditCards");
        }
    }
}
