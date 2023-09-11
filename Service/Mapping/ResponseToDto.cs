using AutoMapper;
using Service.DTOs;

namespace Service.Mapping
{
    public class ResponseToDto : Profile
    {
        public ResponseToDto()
        {
            CreateMap<ResponseRecipeDTO, RecipeDTO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo))
                .ForMember(dest => dest.PreparationMethod, opt => opt.MapFrom(src => src.PreparationMethod))
                .ForMember(dest => dest.PreparationTime, opt => opt.MapFrom(src => src.PreparationTime))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => src.Difficulty))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients));
        }
    }
}