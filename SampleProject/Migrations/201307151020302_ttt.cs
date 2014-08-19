namespace SampleProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ttt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfiles", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "Test", c => c.String());
        }
    }
}
