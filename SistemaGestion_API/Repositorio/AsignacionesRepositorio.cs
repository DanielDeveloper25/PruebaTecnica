using SistemaGestion_API.Datos;
using SistemaGestion_API.Models;
using SistemaGestion_API.Repositorio.Interfaces;

namespace SistemaGestion_API.Repositorio
{
	public class AsignacionesRepositorio : Repositorio<Asignaciones>, IAsignacionesRepositorio
	{
		public readonly ApplicationDbContext _context;
		public AsignacionesRepositorio(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
		public async Task<Asignaciones> Actualizar(Asignaciones entidad)
		{
			entidad.FechaActualizacion = DateTime.Now;
			_context.Asignaciones.Update(entidad);
			await _context.SaveChangesAsync();
			return entidad;
		}
	}
}
