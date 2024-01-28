using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestion_API.Datos;
using SistemaGestion_API.Models;
using SistemaGestion_API.Models.Dtos;

namespace SistemaGestion_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProyectoController : ControllerBase
	{
		public readonly ILogger<ProyectoController> _logger;
		public readonly ApplicationDbContext _context;
		public ProyectoController(ILogger<ProyectoController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet]
		public ActionResult<IEnumerable<ProyectoDto>> GetProyectos()
		{
			_logger.LogInformation("Obtener los proyectos");
			return Ok(_context.Proyectos.ToList());
		}

		[HttpGet("id:int", Name = "GetProyecto")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<ProyectoDto> GetProyecto(int id)
		{
			if (id == 0)
			{
				_logger.LogError("Error al obtener la villa con id:" + id);
				return BadRequest();
			}
			var proyecto = _context.Proyectos.FirstOrDefault(p => p.Id == id);

			if (proyecto == null)
			{
				return NotFound();
			}
			return Ok(proyecto);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<ProyectoDto> CrearProyecto([FromBody] ProyectoDto proyectoDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (_context.Proyectos.FirstOrDefault(p => p.Nombre.ToLower() == proyectoDto.Nombre.ToLower()) != null)
			{
				ModelState.AddModelError("ProyectoExistente", "Ya existe un proyecto con este nombre");
				return BadRequest(ModelState);
			}

			if (proyectoDto == null)
			{
				return BadRequest(proyectoDto);
			}

			if (proyectoDto.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}

			Proyecto proyecto = new()
			{
				Nombre = proyectoDto.Nombre,
				Descripcion = proyectoDto.Descripcion,
				ImagenUrl = proyectoDto.ImagenUrl
			};
			_context.Proyectos.Add(proyecto);
			_context.SaveChanges();

			return CreatedAtRoute("GetProyecto", new { id = proyectoDto.Id }, proyectoDto);
		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult DeleteProyecto(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			var proyecto = _context.Proyectos.FirstOrDefault(p => p.Id == id);

			if (proyecto == null)
			{
				return NotFound();
			}
			_context.Proyectos.Remove(proyecto);
			_context.SaveChanges();

			return NoContent();
		}

		[HttpPost("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateVilla(int id, [FromBody] ProyectoDto proyectoDto)
		{
			if (proyectoDto == null || id != proyectoDto.Id)
			{
				return BadRequest();
			}

			Proyecto proyecto = new()
			{
				Id = proyectoDto.Id,
				Nombre = proyectoDto.Nombre,
				Descripcion = proyectoDto.Descripcion,
				ImagenUrl = proyectoDto.ImagenUrl
			};

			_context.Proyectos.Update(proyecto);
			_context.SaveChanges();

			return NoContent();
		}
	}
}
