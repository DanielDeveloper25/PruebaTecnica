using Microsoft.EntityFrameworkCore;
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

		public async Task<List<Usuario>> GetUsuariosPorProyectoId(int proyectoId)
		{
			return await _context.Usuarios
				.Join(_context.Asignaciones,
				  u => u.Id,
				  a => a.UsuarioId,
				  (usuario, asignacion) => new { usuario, asignacion })
				.Where(x => x.asignacion.ProyectoId == proyectoId)
				.Select(x => x.usuario)
				.ToListAsync();
		}
	}
}
