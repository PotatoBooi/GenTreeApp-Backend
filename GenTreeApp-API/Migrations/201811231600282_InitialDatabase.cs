namespace GenTreeApp_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        UUID = c.Guid(nullable: false, identity: true),
                        Body = c.String(),
                        Details_UUID = c.Guid(),
                    })
                .PrimaryKey(t => t.UUID)
                .ForeignKey("dbo.Details", t => t.Details_UUID)
                .Index(t => t.Details_UUID);
            
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        UUID = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Sex = c.String(),
                    })
                .PrimaryKey(t => t.UUID);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        UUID = c.Guid(nullable: false, identity: true),
                        Date = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        Details_UUID = c.Guid(),
                    })
                .PrimaryKey(t => t.UUID)
                .ForeignKey("dbo.Details", t => t.Details_UUID)
                .Index(t => t.Details_UUID);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        UUID = c.Guid(nullable: false, identity: true),
                        Type = c.String(),
                        Url = c.String(),
                        Details_UUID = c.Guid(),
                    })
                .PrimaryKey(t => t.UUID)
                .ForeignKey("dbo.Details", t => t.Details_UUID)
                .Index(t => t.Details_UUID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        UUID = c.Guid(nullable: false, identity: true),
                        Details_UUID = c.Guid(),
                        Relation_UUID = c.Guid(),
                    })
                .PrimaryKey(t => t.UUID)
                .ForeignKey("dbo.Details", t => t.Details_UUID)
                .ForeignKey("dbo.Relation", t => t.Relation_UUID)
                .Index(t => t.Details_UUID)
                .Index(t => t.Relation_UUID);
            
            CreateTable(
                "dbo.Relation",
                c => new
                    {
                        UUID = c.Guid(nullable: false, identity: true),
                        Child_UUID = c.Guid(),
                        Person_UUID = c.Guid(),
                        Tree_UUID = c.Guid(),
                    })
                .PrimaryKey(t => t.UUID)
                .ForeignKey("dbo.Person", t => t.Child_UUID)
                .ForeignKey("dbo.Person", t => t.Person_UUID)
                .ForeignKey("dbo.Tree", t => t.Tree_UUID)
                .Index(t => t.Child_UUID)
                .Index(t => t.Person_UUID)
                .Index(t => t.Tree_UUID);
            
            CreateTable(
                "dbo.Tree",
                c => new
                    {
                        UUID = c.Guid(nullable: false, identity: true),
                        Editable = c.Boolean(nullable: false),
                        User_UUID = c.Guid(),
                    })
                .PrimaryKey(t => t.UUID)
                .ForeignKey("dbo.User", t => t.User_UUID)
                .Index(t => t.User_UUID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UUID = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.UUID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tree", "User_UUID", "dbo.User");
            DropForeignKey("dbo.Relation", "Tree_UUID", "dbo.Tree");
            DropForeignKey("dbo.Relation", "Person_UUID", "dbo.Person");
            DropForeignKey("dbo.Person", "Relation_UUID", "dbo.Relation");
            DropForeignKey("dbo.Relation", "Child_UUID", "dbo.Person");
            DropForeignKey("dbo.Person", "Details_UUID", "dbo.Details");
            DropForeignKey("dbo.Media", "Details_UUID", "dbo.Details");
            DropForeignKey("dbo.Event", "Details_UUID", "dbo.Details");
            DropForeignKey("dbo.Comment", "Details_UUID", "dbo.Details");
            DropIndex("dbo.Tree", new[] { "User_UUID" });
            DropIndex("dbo.Relation", new[] { "Tree_UUID" });
            DropIndex("dbo.Relation", new[] { "Person_UUID" });
            DropIndex("dbo.Relation", new[] { "Child_UUID" });
            DropIndex("dbo.Person", new[] { "Relation_UUID" });
            DropIndex("dbo.Person", new[] { "Details_UUID" });
            DropIndex("dbo.Media", new[] { "Details_UUID" });
            DropIndex("dbo.Event", new[] { "Details_UUID" });
            DropIndex("dbo.Comment", new[] { "Details_UUID" });
            DropTable("dbo.User");
            DropTable("dbo.Tree");
            DropTable("dbo.Relation");
            DropTable("dbo.Person");
            DropTable("dbo.Media");
            DropTable("dbo.Event");
            DropTable("dbo.Details");
            DropTable("dbo.Comment");
        }
    }
}
