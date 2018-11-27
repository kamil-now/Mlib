using Mlib.Data.Models;
using Mlib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Data
{
    public class UnitOfWork
    {
        public EventHandler DbContextChanged;
        private readonly DbContext dbContext;

        public PlaylistRepository Playlists { get; }
        public TrackRepository Tracks { get; }
        public ArtistRepository Artists { get; }
        public AlbumRepository Albums { get; }
        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
            Playlists = new PlaylistRepository(dbContext);
            Tracks = new TrackRepository(dbContext);
            Artists = new ArtistRepository(dbContext);
            Albums = new AlbumRepository(dbContext);
        }
        public IRepository GetRepository<T>() where T : class, IDataEntity
        {
            Type type = typeof(T);
            switch (type)
            {
                case Type playlist when playlist == typeof(Playlist): return Playlists;
                case Type track when track == typeof(Track): return Tracks;
                case Type artist when artist == typeof(Artist): return Artists;
                case Type album when album == typeof(Album): return Albums;

            }
            return null;
        }
        public bool AddOrUpdate<T>(T entity, bool commit) where T : class, IDataEntity
        {
            var repo = GetRepository<T>();
            var isValid = repo != null && ValidateRequiredProperties(entity);

            if (isValid)
            {
                var databaseEntity = repo?.Get(entity.Id);
                var exists = databaseEntity != null;
                try
                {

                    EntityState state;
                    if (exists)
                    {
                        repo.Edit(databaseEntity);
                        state = EntityState.Modified;
                    }
                    else
                    {
                        repo.Add(entity);
                        state = EntityState.Added;
                    }
                    if (commit)
                    {
                        Commit();
                        DbContextChanged?.Invoke(repo, new DbContextChangedEventArgs(entity, state));
                    }

                }
                catch
                {

                    return false;
                }
            }
            return isValid;
        }
        public bool Delete<T>(T entity, bool commit) where T : class, IDataEntity, new()
        {
            var repo = GetRepository<T>();

            if (repo != null && repo?.Get(entity.Id) != null)
            {
                try
                {
                    repo.Delete(entity);
                    if (commit)
                    {
                        Commit();
                        DbContextChanged?.Invoke(repo, new DbContextChangedEventArgs(entity, EntityState.Deleted));
                    }
                }
                catch
                {

                    return false;
                }
                return true;
            }
            return false;
        }
        public bool ValidateRequiredProperties<T>(T entity) where T : class, IDataEntity
        {
            var props = entity.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(RequiredAttribute)));
            return props.Where(p => p.GetValue(entity) == null).Count() == 0;
        }
        public void Commit()
        {
            dbContext.SaveChanges();
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }
        public void RejectChanges()
        {
            foreach (var entry in dbContext.ChangeTracker.Entries()
                  .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
