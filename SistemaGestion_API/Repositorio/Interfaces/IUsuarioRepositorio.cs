using SistemaGestion_API.Models;

namespace SistemaGestion_API.Repositorio.Interfaces
{
	public interface IUsuarioRepositorio : IRepositorio<Usuario>
	{
		Task<Usuario> Actualizar(Usuario entidad);
		Task<List<Proyecto>> GetProyectosPorUsuarioId(int usuarioId);
	}
}
