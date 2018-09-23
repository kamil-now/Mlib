namespace Mlib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class constuctors : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TrackPlaylists", newName: "PlaylistTracks");
            DropPrimaryKey("dbo.PlaylistTracks");
            AddPrimaryKey("dbo.PlaylistTracks", new[] { "Playlist_Name", "Track_TrackId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PlaylistTracks");
            AddPrimaryKey("dbo.PlaylistTracks", new[] { "Track_TrackId", "Playlist_Name" });
            RenameTable(name: "dbo.PlaylistTracks", newName: "TrackPlaylists");
        }
    }
}
