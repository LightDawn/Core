namespace SampleProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rrr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FName = c.String(nullable: false, maxLength: 50),
                        LName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Urls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Title = c.String(),
                        PageId = c.Int(nullable: false),
                        HttpVerpId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pages", t => t.PageId, cascadeDelete: true)
                .Index(t => t.PageId);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UrlRoles",
                c => new
                    {
                        Url_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Url_Id, t.Role_Id })
                .ForeignKey("dbo.Urls", t => t.Url_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Url_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UrlRoles", new[] { "Role_Id" });
            DropIndex("dbo.UrlRoles", new[] { "Url_Id" });
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.Urls", new[] { "PageId" });
            DropForeignKey("dbo.UrlRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UrlRoles", "Url_Id", "dbo.Urls");
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.Users");
            DropForeignKey("dbo.Urls", "PageId", "dbo.Pages");
            DropTable("dbo.UrlRoles");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Pages");
            DropTable("dbo.Urls");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
        }
    }
}
