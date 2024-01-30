using SistemaGestion_API.Models;

namespace SistemaGestion_API.Repositorio.Interfaces
{
	public interface IAsignacionesRepositorio : IRepositorio<Asignaciones> 
	{
		Task<Asignaciones> Actualizar(Asignaciones entidad);
	}
}
