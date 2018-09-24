namespace Mlib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "Artist_Name", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tracks", "Artist_Name");
            AddForeignKey("dbo.Tracks", "Artist_Name", "dbo.Artists", "Name");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "Artist_Name", "dbo.Artists");
            DropIndex("dbo.Tracks", new[] { "Artist_Name" });
            DropColumn("dbo.Tracks", "Artist_Name");
        }
    }
}
