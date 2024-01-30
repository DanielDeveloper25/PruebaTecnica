using System.ComponentModel.DataAnnotations;

namespace SistemaGestion_API.Models.Dtos
{
	public class AsignacionesCreateDto
	{
		[Required]
		public int UsuarioId { get; set; }
		[Required]
		public int ProyectoId { get; set; }
	}
}
