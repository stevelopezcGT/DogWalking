namespace DogWalking.DL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialEntitiesCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Breed = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Walks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DogId = c.Int(nullable: false),
                        WalkDate = c.DateTime(nullable: false),
                        DurationMinutes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dogs", t => t.DogId, cascadeDelete: true)
                .Index(t => t.DogId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Walks", "DogId", "dbo.Dogs");
            DropForeignKey("dbo.Dogs", "ClientId", "dbo.Clients");
            DropIndex("dbo.Walks", new[] { "DogId" });
            DropIndex("dbo.Dogs", new[] { "ClientId" });
            DropTable("dbo.Users");
            DropTable("dbo.Walks");
            DropTable("dbo.Dogs");
            DropTable("dbo.Clients");
        }
    }
}
