using Microsoft.EntityFrameworkCore;

namespace webApi.Models;

public class ItemsContext : DbContext
{
    public ItemsContext(DbContextOptions<ItemsContext> options)
        : base(options)
    {
    }

    public DbSet<Items> TodoItems { get; set; } = null!;
}