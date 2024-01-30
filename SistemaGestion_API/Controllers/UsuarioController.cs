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
	public class UsuarioController : ControllerBase
	{
		public readonly ILogger<UsuarioController> _logger;
		public readonly IUsuarioRepositorio _usuarioRepo;
		public readonly IMapper _mapper;
		protected APIResponse _response;
		public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
		{
			_logger = logger;
			_usuarioRepo = usuarioRepositorio;
			_mapper = mapper;
			_response = new();
		}

		[HttpGet("GetUsuarios")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetUsuarios()
		{
			try
			{
				_logger.LogInformation("Obtener los usuarios");

				IEnumerable<Usuario> usuarios = await _usuarioRepo.ObtenerTodos();

				_response.Resultado = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
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

		[HttpGet(Name = "GetUsuario")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetUsuario(int id)
		{
			try
			{
				if (id == 0)
				{
					_logger.LogError("Error al obtener el usuario con id:" + id);
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsExitoso = false;
					return BadRequest(_response);
				}
				var usuario = await _usuarioRepo.Obtener(u => u.Id == id);

				if (usuario == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					_response.IsExitoso = false;
					return NotFound(_response);
				}

				_response.Resultado = _mapper.Map<UsuarioDto>(usuario);
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

		[HttpPost("CrearUsuario")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CrearUsuario([FromBody] UsuarioCreateDto createDto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				if (await _usuarioRepo.Obtener(u => u.Email == createDto.Email) != null)
				{
					ModelState.AddModelError("UsuarioExistente", "Ya existe un usuario con este correo");
					return BadRequest(ModelState);
				}

				if (createDto == null)
				{
					return BadRequest(createDto);
				}

				Usuario usuario = _mapper.Map<Usuario>(createDto);

				usuario.FechaCreacion = DateTime.Now;
				usuario.FechaActualizacion = DateTime.Now;
				await _usuarioRepo.crear(usuario);

				_response.Resultado = usuario;
				_response.StatusCode = HttpStatusCode.Created;

				return CreatedAtRoute("GetUsuario", new { id = usuario.Id }, _response);
			}
			catch (Exception ex)
			{
				_response.IsExitoso = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[HttpDelete("DeleteUsuario")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteUsuario(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				var usuario = await _usuarioRepo.Obtener(u => u.Id == id);

				if (usuario == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				await _usuarioRepo.Remover(usuario);
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

		[HttpPost("UpdateUsuario")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioUpdateDto UpdateDto)
		{
			if (UpdateDto == null || id != UpdateDto.Id)
			{
				_response.IsExitoso = false;
				_response.StatusCode = HttpStatusCode.BadRequest;
				return BadRequest(_response);
			}

			Usuario usuario = _mapper.Map<Usuario>(UpdateDto);

			await _usuarioRepo.Actualizar(usuario);

			_response.StatusCode = HttpStatusCode.NoContent;

			return Ok(_response);
		}

		[HttpGet("{usuarioId}/proyectos")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> GetProyectosPorUsuarioId(int usuarioId)
		{
			if (usuarioId == 0)
			{
				_logger.LogError("Error al obtener el usuario con id:" + usuarioId);
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsExitoso = false;
				return BadRequest(_response);
			}
			var usuario = await _usuarioRepo.Obtener(u => u.Id == usuarioId);

			if (usuario == null)
			{
				_response.StatusCode = HttpStatusCode.NotFound;
				_response.IsExitoso = false;
				return NotFound(_response);
			}

			var proyectos = await _usuarioRepo.GetProyectosPorUsuarioId(usuarioId);

			return Ok(proyectos);
		}
	}
}
