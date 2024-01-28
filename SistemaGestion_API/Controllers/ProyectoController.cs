using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestion_API.Datos;
using SistemaGestion_API.Models;
using SistemaGestion_API.Models.Dtos;
using SistemaGestion_API.Repositorio.Interfaces;

namespace SistemaGestion_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProyectoController : ControllerBase
	{
		public readonly ILogger<ProyectoController> _logger;
		public readonly IProyectoRepositorio _proyectoRepo;
		public readonly IMapper _mapper;
		protected APIResponse _response;
		public ProyectoController(ILogger<ProyectoController> logger, IProyectoRepositorio proyectoRepositorio, IMapper mapper)
		{
			_logger = logger;
			_proyectoRepo = proyectoRepositorio;
			_mapper = mapper;
			_response = new();
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetProyectos()
		{
			try
			{
				_logger.LogInformation("Obtener los proyectos");

				IEnumerable<Proyecto> proyectos = await _proyectoRepo.ObtenerTodos();

				_response.Resultado = _mapper.Map<IEnumerable<ProyectoDto>>(proyectos);
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsExitoso = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[HttpGet("id:int", Name = "GetProyecto")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetProyecto(int id)
		{
			try
			{
				if (id == 0)
				{
					_logger.LogError("Error al obtener la villa con id:" + id);
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsExitoso = false;
					return BadRequest(_response);
				}
				var proyecto = await _proyectoRepo.Obtener(p => p.Id == id);

				if (proyecto == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					_response.IsExitoso = false;
					return NotFound(_response);
				}

				_response.Resultado = _mapper.Map<ProyectoDto>(proyecto);
				_response.StatusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsExitoso = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CrearProyecto([FromBody] ProyectoCreateDto createDto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				if (await _proyectoRepo.Obtener(p => p.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
				{
					ModelState.AddModelError("ProyectoExistente", "Ya existe un proyecto con este nombre");
					return BadRequest(ModelState);
				}

				if (createDto == null)
				{
					return BadRequest(createDto);
				}

				Proyecto proyecto = _mapper.Map<Proyecto>(createDto);

				proyecto.FechaCreacion = DateTime.Now;
				proyecto.FechaActualizacion = DateTime.Now;
				await _proyectoRepo.crear(proyecto);

				_response.Resultado = proyecto;
				_response.StatusCode = HttpStatusCode.Created;

				return CreatedAtRoute("GetProyecto", new { id = proyecto.Id }, _response);
			}
			catch (Exception ex)
			{
				_response.IsExitoso = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteProyecto(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				var proyecto = await _proyectoRepo.Obtener(p => p.Id == id);

				if (proyecto == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				await _proyectoRepo.Remover(proyecto);
				_response.StatusCode = HttpStatusCode.NoContent;

				return Ok(_response);
			}
			catch (Exception ex)  
			{
				_response.IsExitoso = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return BadRequest(_response);
		}

		[HttpPost("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateProyecto(int id, [FromBody] ProyectoUpdateDto UpdateDto)
		{
			if (UpdateDto == null || id != UpdateDto.Id)
			{
				_response.IsExitoso = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}

			Proyecto proyecto = _mapper.Map<Proyecto>(UpdateDto);

			await _proyectoRepo.Actualizar(proyecto);

			_response.StatusCode = HttpStatusCode.NoContent;

			return Ok(_response);
		}
	}
}
