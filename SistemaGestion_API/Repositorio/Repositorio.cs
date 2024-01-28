using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SistemaGestion_API.Datos;
using SistemaGestion_API.Repositorio.Interfaces;

namespace SistemaGestion_API.Repositorio
{
	public class Repositorio<T> : IRepositorio<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		internal DbSet<T> dbSet;
        public Repositorio(ApplicationDbContext context)
        {
            _context = context;
			this.dbSet = _context.Set<T>();
        }
        public async Task crear(T entidad)
		{
			await dbSet.AddAsync(entidad);
			await Guardar();
		}

		public async Task Guardar()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = false)
		{
			IQueryable<T> query = dbSet;
			if(!tracked)
			{
				query = query.AsNoTracking();
			}
			if(filtro != null)
			{
				query = query.Where(filtro);
			}
			return await query.FirstOrDefaultAsync();
		}

		public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
		{
			IQueryable<T> query = dbSet;
			if (filtro != null)
			{
				query = query.Where(filtro);
			}
			return await query.ToListAsync();
		}

		public async Task Remover(T entidad)
		{
			dbSet.Remove(entidad);
			await Guardar();
		}
	}
}
