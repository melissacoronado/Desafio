using ApiMutants.Domain.NonEntities;
using AutoMapper;

namespace ApiMutants
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            _ = CreateMap<Request.MutantRequest, Mutants>()
                .ForMember(dest => dest.DNA, opt => opt.MapFrom(src => src.DNA.ToList()))
                .ReverseMap();

            _ = CreateMap<string[], Mutants>()
                .ForMember(dest => dest.DNA, opt => opt.MapFrom(src => src.ToList()))
                .ReverseMap();
        }
    }
}
