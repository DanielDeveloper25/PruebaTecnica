using Microsoft.EntityFrameworkCore;
using SistemaGestion_API.Datos;
using SistemaGestion_API.Models;
using SistemaGestion_API.Repositorio.Interfaces;

namespace SistemaGestion_API.Repositorio
{
	public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
	{
		public readonly ApplicationDbContext _context;
        public UsuarioRepositorio(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<Usuario> Actualizar(Usuario entidad)
		{
			entidad.FechaActualizacion = DateTime.Now;
			_context.Usuarios.Update(entidad);
			await _context.SaveChangesAsync();
			return entidad;
		}

		public async Task<List<Proyecto>> GetProyectosPorUsuarioId(int usuarioId)
		{
			return await _context.Proyectos
				 .Join(_context.Asignaciones,
				   p => p.Id,
				   a => a.ProyectoId,
				   (proyecto, asignacion) => new { proyecto, asignacion })
				 .Where(x => x.asignacion.UsuarioId == usuarioId)
				 .Select(x => x.proyecto)
				 .ToListAsync();
		}
	}
}
