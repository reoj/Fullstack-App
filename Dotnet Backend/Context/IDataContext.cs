using DotnetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace users_items_backend.Context
{
    public interface IDataContext
    {
        DbSet<Exchange> Exchanges { get; }
        DbSet<InventoryItem> Items { get; }
        DbSet<User> Users { get; }
    }
}