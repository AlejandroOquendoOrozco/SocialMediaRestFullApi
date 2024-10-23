using AutoMapper;
using SocialMedia.Core.DTO;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infraestructure.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile(){

            CreateMap<Publicacion,PublicacionDTO>();
            CreateMap<PublicacionDTO,Publicacion>();
            CreateMap<Seguridad, SeguridadDTO>().ReverseMap();
           

        }
    }
}