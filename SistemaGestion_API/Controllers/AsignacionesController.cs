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
	public class AsignacionesController : ControllerBase
	{
		public readonly ILogger<AsignacionesController> _logger;
		public readonly IAsignacionesRepositorio _asignacionesRepo;
		public readonly IProyectoRepositorio _proyectoRepo;
		public readonly IUsuarioRepositorio _usuarioRepo;
		public readonly IMapper _mapper;
		protected APIResponse _response;
		public AsignacionesController(ILogger<AsignacionesController> logger, IAsignacionesRepositorio asignacionesRepositorio, 
			IProyectoRepositorio proyectoRepo, IUsuarioRepositorio usuarioRepo, IMapper mapper)
		{
			_logger = logger;
			_asignacionesRepo = asignacionesRepositorio;
			_proyectoRepo = proyectoRepo;
			_usuarioRepo = usuarioRepo;
			_mapper = mapper;
			_response = new();
		}

		[HttpGet("GetAsignaciones")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetAsignaciones()
		{
			try
			{
				_logger.LogInformation("Obtener las asignaciones");

				IEnumerable<Asignaciones> asignacion = await _asignacionesRepo.ObtenerTodos();

				_response.Resultado = _mapper.Map<IEnumerable<AsignacionesDto>>(asignacion);
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

		[HttpGet(Name = "GetAsignacion")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetAsignacion(int id)
		{
			try
			{
				if (id == 0)
				{
					_logger.LogError("Error al obtener la asignacion con id:" + id);
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsExitoso = false;
					return BadRequest(_response);
				}
				var asignacion = await _asignacionesRepo.Obtener(a => a.Id == id);

				if (asignacion == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					_response.IsExitoso = false;
					return NotFound(_response);
				}

				_response.Resultado = _mapper.Map<AsignacionesDto>(asignacion);
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

		[HttpPost("CrearAsignacion")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> CrearAsignacion([FromBody] AsignacionesCreateDto createDto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				if (createDto == null)
				{
					return BadRequest(createDto);
				}

				if(await _proyectoRepo.Obtener(p => p.Id == createDto.ProyectoId) == null ||
					_usuarioRepo.Obtener(u => u.Id == createDto.UsuarioId) == null)
				{
					ModelState.AddModelError("ClaveForanea", "No se encontraron los id insertados");
					return BadRequest(ModelState);
				}

				Asignaciones asignacion = _mapper.Map<Asignaciones>(createDto);

				asignacion.FechaCreacion = DateTime.Now;
				asignacion.FechaActualizacion = DateTime.Now;
				await _asignacionesRepo.crear(asignacion);

				_response.Resultado = asignacion;
				_response.StatusCode = HttpStatusCode.Created;

				return CreatedAtRoute("GetAsignacion", new { id = asignacion.Id }, _response);
			}
			catch (Exception ex)
			{
				_response.IsExitoso = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[HttpDelete("DeleteAsignacion")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteAsignacion(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				var asignacion = await _asignacionesRepo.Obtener(a => a.Id == id);

				if (asignacion == null)
				{
					_response.IsExitoso=false;
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				await _asignacionesRepo.Remover(asignacion);
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

		[HttpPost("UpdateAsignacion")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateProyecto(int id, [FromBody] AsignacionesUpdateDto UpdateDto)
		{
			if (UpdateDto == null || id != UpdateDto.Id)
			{
				_response.IsExitoso = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}

			Asignaciones asignacion = _mapper.Map<Asignaciones>(UpdateDto);

			await _asignacionesRepo.Actualizar(asignacion);

			_response.StatusCode = HttpStatusCode.NoContent;

			return Ok(_response);
		}
	}
}
