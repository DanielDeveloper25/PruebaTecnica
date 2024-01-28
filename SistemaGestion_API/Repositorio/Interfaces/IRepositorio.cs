using System.Linq.Expressions;

namespace SistemaGestion_API.Repositorio.Interfaces
{
	public interface IRepositorio<T> where T : class
	{
		Task crear(T entidad);
		Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null);
		Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = false);
		Task Remover (T entidad);
		Task Guardar();
	}
}
