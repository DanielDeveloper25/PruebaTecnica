using System.ComponentModel.DataAnnotations;

namespace SistemaGestion_API.Models.Dtos
{
	public class ProyectoDto
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
