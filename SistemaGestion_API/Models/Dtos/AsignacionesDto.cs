using System.ComponentModel.DataAnnotations;

namespace SistemaGestion_API.Models.Dtos
{
	public class AsignacionesDto
	{
        public int Id { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int ProyectoId { get; set; }
	}
}
