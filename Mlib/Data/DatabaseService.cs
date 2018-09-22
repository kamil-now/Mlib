using Mlib.Data.Models;
using Mlib.Infrastructure;
using Mlib.Interfaces;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mlib.Data
{
    public class DatabaseService : IDatabaseService
    {
        private readonly SQLiteAsyncConnection asyncDb;
        private readonly SQLiteConnection db;

        public DatabaseService()
        {
            asyncDb = new SQLiteAsyncConnection(ConnectionStrings.DatabaseFilePath);
            db = new SQLiteConnection(ConnectionStrings.DatabaseFilePath);
            InitializeDatabase();
        }
        private void InitializeDatabase()
        {
            asyncDb.CreateTableAsync<Track>().Wait();
            asyncDb.CreateTableAsync<Playlist>().Wait();
            asyncDb.CreateTableAsync<PlaylistData>().Wait();

        }
        Task<List<Track>> GetAllTracksAsync() => asyncDb.Table<Track>().ToListAsync();
        
        public void SavePlaylist(Playlist playlist)
        {
            SaveItemAsync(playlist);
            playlist.Tracks.ForEach(n => SaveItemAsync(n));

            
        }
        public Task<int> SaveItemAsync(IDatabaseEntity item)
        {

            if (item.ID != 0)
            {
                return asyncDb.UpdateAsync(item);
            }
            else
            {
                return asyncDb.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(IDatabaseEntity item)
        {
            return asyncDb.DeleteAsync(item);
        }

        public void Update(IDatabaseEntity entity)
        {
            db.UpdateWithChildren(entity);
        }
    }
}
