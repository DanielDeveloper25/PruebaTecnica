using SistemaGestion_API.Datos;
using SistemaGestion_API.Models;
using SistemaGestion_API.Repositorio.Interfaces;

namespace SistemaGestion_API.Repositorio
{
	public class ProyectoRepositorio : Repositorio<Proyecto>, IProyectoRepositorio
	{
		private readonly ApplicationDbContext _context;
        public ProyectoRepositorio(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Proyecto> Actualizar(Proyecto entidad)
		{
			entidad.FechaActualizacion = DateTime.Now;
			_context.Proyectos.Update(entidad);
			await _context.SaveChangesAsync();
			return entidad;
		}
	}
}
