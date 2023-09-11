using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> FindById(int id);
        Task<Category> FindByName(string name);
        Task<List<Category>> FindAll();
        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task<bool> Delete(int id);
        Task<bool> IsCategoryUnique(string name);
    }
}
