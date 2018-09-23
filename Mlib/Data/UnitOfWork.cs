using Mlib.Data.Models;
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
        }
        public IRepository GetRepository<T>(T entity) where T : class, IDataEntity
        {
            if (entity is Playlist)
            {
                return Playlists;
            }
            if (entity is Artist)
            {
                return Artists;

            }
            if (entity is Album)
            {
                return Albums;
            }
            if (entity is Track)
            {
                return Tracks;
            }
            return null;
        }
        public bool AddOrUpdate<T>(T entity, bool commit) where T : class, IDataEntity
        {
            var repo = GetRepository(entity);
            var isValid = repo != null && ValidateRequiredProperties(entity);

            if (isValid)
            {
                var databaseEntity = repo?.Get(entity.Id);
                var exists = databaseEntity != null;
                try
                {
                    repo.Add(entity);

                    if (exists)
                    {
                        repo.Edit(databaseEntity);
                    }
                    if (commit)
                    {
                        Commit();
                        DbContextChanged?.Invoke(repo, null);
                    }
                }
                catch
                {

                    return false;
                }
            }
            return isValid;
        }
        public bool Delete<T>(T entity, bool commit) where T : class, IDataEntity
        {
            var repo = GetRepository(entity);

            if (repo != null && repo?.Get(entity.Id) != null)
            {
                try
                {
                    repo.Delete(entity);
                    if (commit)
                    {
                        Commit();
                        DbContextChanged?.Invoke(repo, null);
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
