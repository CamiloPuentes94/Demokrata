using Demokrata.Modelo;
using Microsoft.EntityFrameworkCore;

namespace Demokrata.Data
{
    public class aplicacionDbContext: DbContext
    {

        public aplicacionDbContext(DbContextOptions<aplicacionDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
