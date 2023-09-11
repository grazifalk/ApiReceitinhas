using Data.Context;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ContextDb _db;

        public RecipeRepository(ContextDb db)
        {
            this._db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<Recipe> Create(Recipe recipe)
        {
            _db.Recipes.Add(recipe);
            await _db.SaveChangesAsync();
            return recipe;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var recipe = await _db.Recipes.Where(r => r.Id == id).FirstOrDefaultAsync();
                if (recipe == null) return false;
                _db.Recipes.Remove(recipe);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ICollection<Recipe>> FindAll()
        {
            var recipes = await _db.Recipes.ToListAsync();
            return recipes;
        }

        public async Task<Recipe> FindById(int id)
        {
            var recipe = await _db.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            return recipe;
        }


        public async Task<Recipe> FindByName(string name)
        {
            var recipe = await _db.Recipes.Include(r => r.Ingredients).FirstOrDefaultAsync(r => r.Title == name);
            return recipe;
        }

        public async Task<bool> IsRecipeUnique(string name)
        {
            return !await _db.Recipes.AnyAsync(recipe => recipe.Title == name);
        }

        public async Task<Recipe> Update(Recipe recipe)
        {
            _db.Recipes.Update(recipe);
            await _db.SaveChangesAsync();
            return recipe;
        }
    }
}
