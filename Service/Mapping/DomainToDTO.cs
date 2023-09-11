using AutoMapper;
using Domain.Entities;
using Service.DTOs;

namespace Service.Mapping
{
    public class DomainToDTO : Profile
    {
        public DomainToDTO()
        {
            CreateMap<Recipe, RecipeDTO>();
            CreateMap<Category, CategoryDTO>();
        }
    }
}
