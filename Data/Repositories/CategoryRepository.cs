using Data.Context;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ContextDb _db;
        public CategoryRepository(ContextDb db)
        {
            this._db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<Category> Create(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null) return false;
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Category>> FindAll()
        {
            var categories = await _db.Categories.Include(c => c.Recipes).ToListAsync();
            return categories;
        }

        public async Task<Category> FindById(int id)
        {
            var category = await _db.Categories.Include(x => x.Recipes).FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }

        public async Task<Category> FindByName(string name)
        {
            var category = await _db.Categories.Include(x => x.Recipes).FirstOrDefaultAsync(x => x.Name == name);
            return category;
        }

        public async Task<bool> IsCategoryUnique(string name)
        {
            return !await _db.Categories.AnyAsync(category => category.Name == name);
        }

        public async Task<Category> Update(Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            return category;
        }
    }
}
