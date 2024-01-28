using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGestion_API.Models
{
	public class Usuario
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public string Nombre { get; set; }

		[Required]
		public string Email { get; set; }
		public string ImagenUrl { get; set;}
		public DateTime FechaCreacion { get; set; }
		public DateTime FechaActualizacion { get; set; }
	}
}
