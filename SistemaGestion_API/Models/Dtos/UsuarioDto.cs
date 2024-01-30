using System.ComponentModel.DataAnnotations;

namespace SistemaGestion_API.Models.Dtos
{
	public class UsuarioDto
	{
		public int Id { get; set; }

		[Required]
		public string Nombre { get; set; }

		[Required]
		public string Email { get; set; }
	}
}
