using Microsoft.EntityFrameworkCore;
using InventoryManagement.Models;

namespace InventoryManagement.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Inventory> Items { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
    }
}
