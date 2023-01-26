using AutoMapper;
using DirectoryWebApi.Models.Dtos;
using DirectoryWebApi.Models.Entities;

namespace DirectoryWebApi.Models.Mapping
{
    public class MappingProfil :Profile
    {
        public MappingProfil()
        {
            CreateMap<RehberDto, Rehber>();
        }
    }
}
