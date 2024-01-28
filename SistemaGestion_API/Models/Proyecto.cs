using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGestion_API.Models
{
	public class Proyecto
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public DateTime FechaCreacion { get; set; }
		public DateTime FechaActualizacion { get; set; }

	}
}
