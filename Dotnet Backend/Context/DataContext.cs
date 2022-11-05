using exam_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace users_items_backend.Context
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        public DbSet<InventoryItem> Items => Set<InventoryItem>();
        public DbSet<User> Users =>Set<User>();

    }
}
