using System.Diagnostics;
using System.Net;
using Microsoft.EntityFrameworkCore;
using SistemaGestion_API.Models;

namespace SistemaGestion_API.Datos
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
            
        }
		public DbSet<Proyecto> Proyectos { get; set; }
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Asignaciones> Asignaciones { get; set; }
	}
}
