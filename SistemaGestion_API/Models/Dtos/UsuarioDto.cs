using System.ComponentModel.DataAnnotations;

namespace SistemaGestion_API.Models.Dtos
{
	public class UsuarioDto
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Nombre { get; set; }

		[Required]
		public string Email { get; set; }
		public string ImagenUrl { get; set; }
	}
}
