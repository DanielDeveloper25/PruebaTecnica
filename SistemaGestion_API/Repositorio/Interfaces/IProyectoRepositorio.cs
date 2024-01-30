using SistemaGestion_API.Models;

namespace SistemaGestion_API.Repositorio.Interfaces
{
	public interface IProyectoRepositorio : IRepositorio<Proyecto>
	{
		Task<Proyecto> Actualizar (Proyecto entidad);
		Task<List<Usuario>> GetUsuariosPorProyectoId(int proyectoId);
	}
}
