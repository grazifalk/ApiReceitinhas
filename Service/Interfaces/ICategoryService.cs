using Service.DTOs;
using Service.Exceptions;

namespace Service.Interfaces
{
    public interface ICategoryService
    {
        Task<ResultService<ICollection<CategoryDTO>>> FindAll();
        Task<ResultService<CategoryDTO>> FindById(int id);
        Task<ResultService<CategoryDTO>> Create(CategoryDTO category);
        Task<ResultService<CategoryDTO>> Update(CategoryDTO category);
        Task<ResultService> Delete(int id);
    }
}
