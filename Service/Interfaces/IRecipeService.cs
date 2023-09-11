using Service.DTOs;
using Service.Exceptions;

namespace Service.Interfaces
{
    public interface IRecipeService
    {
        Task<ResultService<ICollection<RecipeDTO>>> FindAll();
        Task<ResultService<RecipeDTO>> FindById(int id);
        Task<ResultService<RecipeDTO>> Create(ResponseRecipeDTO recipeResponse);
        Task<ResultService<RecipeDTO>> Update(RecipeDTO recipe);
        Task<ResultService> Delete(int id);
    }
}
