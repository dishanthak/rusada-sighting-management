using AutoMapper;
using Dishantha.Application.Sightings.DTOs;
using Dishantha.Application.Sightings.Requests;
using Dishantha.Domain.Entities;

namespace Dishantha.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateSightingRequest, Sighting>().ReverseMap();
            CreateMap<UpdateSightingRequest, Sighting>().ReverseMap();
            CreateMap<SightingDTO, Sighting>().ReverseMap();
        }
    }
}
