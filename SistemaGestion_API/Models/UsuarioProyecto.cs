using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestion_API.Models
{
	public class UsuarioProyecto
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey("Usuario")]
		public int UsuarioId { get; set; }

		[ForeignKey("Proyecto")]
		public int ProyectoId { get; set; }
	}
}
