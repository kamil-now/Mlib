using Mlib.Domain.Infrastructure.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Domain.Infrastracture
{
    public class DatabaseService : IDatabaseService
    {
        private readonly SQLiteAsyncConnection database;

        public DatabaseService()
        {
            database = new SQLiteAsyncConnection(ConnectionStrings.DatabaseFilePath);
            InitializeDatabase();
        }
        private void InitializeDatabase()
        {
        }

        public Task<int> SaveItemAsync(IDatabaseEntity item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(IDatabaseEntity item)
        {
            return database.DeleteAsync(item);
        }
    }
}
