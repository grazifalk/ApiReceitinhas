using AutoMapper;
using Domain.Entities;
using Service.DTOs;

namespace Service.Mapping
{
    public class DTOToDomain : Profile
    {
        public DTOToDomain()
        {
            CreateMap<RecipeDTO, Recipe>();
            CreateMap<CategoryDTO, Category>();
        }
    }
}
