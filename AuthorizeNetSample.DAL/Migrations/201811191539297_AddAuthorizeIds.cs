namespace AuthorizeNetSample.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthorizeIds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "AuthorizeId", c => c.String());
            AddColumn("dbo.CreditCards", "AuthorizeId", c => c.String());
            AddColumn("dbo.Customers", "AuthorizeId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "AuthorizeId");
            DropColumn("dbo.CreditCards", "AuthorizeId");
            DropColumn("dbo.Addresses", "AuthorizeId");
        }
    }
}
