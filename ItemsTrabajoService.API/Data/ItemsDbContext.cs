using ItemsTrabajoService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemsTrabajoService.API.Data
{
    public class ItemsDbContext : DbContext
    {
        public ItemsDbContext(DbContextOptions<ItemsDbContext> options) : base(options)
        {
        }
        public DbSet<ItemTrabajo> ItemsTrabajo { get; set; }
    }
}
