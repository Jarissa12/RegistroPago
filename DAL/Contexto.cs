using Microsoft.EntityFrameworkCore;
using RPrestamos.Entidades;

namespace RPrestamos.DAL{
public class Contexto : DbContext
{
    public DbSet<Ocupaciones> Ocupaciones { get; set; }
    public DbSet<Personas> Personas {get ; set;}
    public DbSet<Prestamos> Prestamos { get; set; }

    public DbSet<Pago> Pago {get; set; }

    
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }

}
}
 