using System.ComponentModel.DataAnnotations;

namespace SistemaGestion_API.Models.Dtos
{
	public class ProyectoUpdateDto
	{
		[Required]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Nombre { get; set; }
		[Required]
		public string Descripcion { get; set; }
		[Required]
		public string ImagenUrl { get; set; }
	}
}
