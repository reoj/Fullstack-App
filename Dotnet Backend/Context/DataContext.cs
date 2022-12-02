using DotnetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace users_items_backend.Context
{
    /// <summary>
    /// Implements the DataBase context for all Models
    /// </summary>
    public class DataContext : DbContext, IDataContext
    {

        #region Default Constructor
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        #endregion

        #region Database Sets
        public DbSet<InventoryItem> Items => Set<InventoryItem>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Exchange> Exchanges => Set<Exchange>();

        #endregion
    }
}
