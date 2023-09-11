

using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRecipeRepository
    {
        Task<Recipe> FindById(int id);
        Task<Recipe> FindByName(string name);
        Task<ICollection<Recipe>> FindAll();
        Task<Recipe> Create(Recipe recipe);
        Task<Recipe> Update(Recipe recipe);
        Task<bool> Delete(int id);
        Task<bool> IsRecipeUnique(string name);
    }
}
