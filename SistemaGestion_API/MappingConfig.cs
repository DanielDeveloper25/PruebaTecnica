using AutoMapper;
using SistemaGestion_API.Models;
using SistemaGestion_API.Models.Dtos;

namespace SistemaGestion_API
{
	public class MappingConfig : Profile
	{
		public MappingConfig() 
		{
			CreateMap<Proyecto, ProyectoDto>().ReverseMap();
			CreateMap<Proyecto, ProyectoCreateDto>().ReverseMap();
			CreateMap<Proyecto, ProyectoUpdateDto>().ReverseMap();

			CreateMap<Usuario, UsuarioDto>().ReverseMap();
			CreateMap<Usuario, UsuarioCreateDto>().ReverseMap();
			CreateMap<Usuario, UsuarioUpdateDto>().ReverseMap();

			CreateMap<Asignaciones, AsignacionesDto>().ReverseMap();
			CreateMap<Asignaciones, AsignacionesCreateDto>().ReverseMap();
			CreateMap<Asignaciones, AsignacionesUpdateDto>().ReverseMap();
		}
	}
}
