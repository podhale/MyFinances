using AutoMapper;
using MyFinances.API.Dto;
using MyFinances.API.Models;

namespace MyFinances.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddOperationDto, Operation>();
            CreateMap<AddCategoryDto, Category>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameCategory));
        }
    }
}
