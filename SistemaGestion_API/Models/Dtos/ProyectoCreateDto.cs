using System.ComponentModel.DataAnnotations;

namespace SistemaGestion_API.Models.Dtos
{
	public class ProyectoCreateDto
	{

		[Required]
		[MaxLength(50)]
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public string ImagenUrl { get; set; }
	}
}
