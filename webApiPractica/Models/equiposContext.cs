using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace webApiPractica.Models
{
    public class equiposContext : DbContext
    {
        public equiposContext(DbContextOptions<equiposContext> options) : base (options)
        { 
        
        
        }

        public DbSet<equipos> equipos { get; set; }

        public DbSet<carreras> carreras { get; set; }

        public DbSet<estados_equipos> estados_equipo { get; set; }

        public DbSet<estado_reservas> estados_reserva { get; set; }

        public DbSet<facultades> facultades { get; set; }

        public DbSet<marca> marcas { get; set; }
        public DbSet<reserva> reservas { get; set; }

        public DbSet<tipo_equipos> tipo_equipo { get; set; }

        public DbSet<usuario> usuarios { get; set; }



    }
}
