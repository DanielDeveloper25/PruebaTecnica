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
		public DbSet<UsuarioProyecto> Asignaciones { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Proyecto>().HasData(
				new Proyecto
				{
					Id = 1,
					Nombre = "Proyecto censo",
					Descripcion = "Este es el proyecto de realizacion de censo",
					ImagenUrl = "",
					FechaCreacion = DateTime.Now,
					FechaActualizacion = DateTime.Now
				},
				new Proyecto
				{
					Id = 2,
					Nombre = "Proyecto embellecimiento",
					Descripcion = "Este es el proyecto de embellecimiento de parques",
					ImagenUrl = "",
					FechaCreacion = DateTime.Now,
					FechaActualizacion = DateTime.Now
				}
			);
		}
	}
}
