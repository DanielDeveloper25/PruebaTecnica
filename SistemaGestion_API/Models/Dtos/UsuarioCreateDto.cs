using System.ComponentModel.DataAnnotations;

namespace SistemaGestion_API.Models.Dtos
{
	public class UsuarioCreateDto
	{

		[Required]
		public string Nombre { get; set; }

		[Required]
		public string Email { get; set; }
	}
}
