using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SistemaGestion_API.Models
{
	public class Asignaciones
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]

        public int UsuarioId { get; set; }

		[ForeignKey("UsuarioId")]
		public Usuario usuario { get; set; }

		[Required]
		public int ProyectoId { get; set; }

		[ForeignKey("ProyectoId")]
		public Proyecto Proyecto { get; set; }

		public DateTime FechaCreacion { get; set; }
		public DateTime FechaActualizacion { get; set; }
	}
}
