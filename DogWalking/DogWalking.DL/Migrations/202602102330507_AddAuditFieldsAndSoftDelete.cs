namespace DogWalking.DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditFieldsAndSoftDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Clients", "CreatedBy", c => c.String());
            AddColumn("dbo.Clients", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Clients", "UpdatedBy", c => c.String());
            AddColumn("dbo.Clients", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Dogs", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Dogs", "CreatedBy", c => c.String());
            AddColumn("dbo.Dogs", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Dogs", "UpdatedBy", c => c.String());
            AddColumn("dbo.Dogs", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Walks", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Walks", "CreatedBy", c => c.String());
            AddColumn("dbo.Walks", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Walks", "UpdatedBy", c => c.String());
            AddColumn("dbo.Walks", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CreatedBy", c => c.String());
            AddColumn("dbo.Users", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Users", "UpdatedBy", c => c.String());
            AddColumn("dbo.Users", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsActive");
            DropColumn("dbo.Users", "UpdatedBy");
            DropColumn("dbo.Users", "UpdatedAt");
            DropColumn("dbo.Users", "CreatedBy");
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.Walks", "IsActive");
            DropColumn("dbo.Walks", "UpdatedBy");
            DropColumn("dbo.Walks", "UpdatedAt");
            DropColumn("dbo.Walks", "CreatedBy");
            DropColumn("dbo.Walks", "CreatedAt");
            DropColumn("dbo.Dogs", "IsActive");
            DropColumn("dbo.Dogs", "UpdatedBy");
            DropColumn("dbo.Dogs", "UpdatedAt");
            DropColumn("dbo.Dogs", "CreatedBy");
            DropColumn("dbo.Dogs", "CreatedAt");
            DropColumn("dbo.Clients", "IsActive");
            DropColumn("dbo.Clients", "UpdatedBy");
            DropColumn("dbo.Clients", "UpdatedAt");
            DropColumn("dbo.Clients", "CreatedBy");
            DropColumn("dbo.Clients", "CreatedAt");
        }
    }
}
