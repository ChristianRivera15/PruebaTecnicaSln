using Microsoft.EntityFrameworkCore;
using UsuariosService.API.Models;

namespace UsuariosService.API.Data
{
    public class UsuariosDbContext : DbContext
    {
        public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
