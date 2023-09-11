using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Service.DTOs;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResultService<CategoryDTO>> Create(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<CategoryDTO>(categoryDTO);
            if (category == null) return ResultService.Fail<CategoryDTO>(400, "A categoria deve ser informada");
            var result = new CategoryDTOValidator().Validate(category);
            if (!result.IsValid) return ResultService.RequestError<CategoryDTO>(400, "Problemas na validação", result);

            var isUnique = await _repository.IsCategoryUnique(category.Name);
            if (!isUnique) return ResultService.Fail<CategoryDTO>(400, "Já existe uma categoria com esse nome");

            var newCategory = _mapper.Map<Category>(category);
            var data = await _repository.Create(newCategory);
            return ResultService.Ok<CategoryDTO>(201, _mapper.Map<CategoryDTO>(data));
        }

        /*     public async Task<ResultService> Delete(int id)
             {
                 if (id <= 0) return ResultService.Fail(406, "Valor informado inválido");
                 var category = await _repository.FindById(id);
                 if (category == null) return ResultService.Fail<CategoryDTO>(404, "Categoria não encontrada");
                 await _repository.Delete(id);
                 return ResultService.Ok(200, "A categoria foi excluída com sucesso");
             }*/

        public async Task<ResultService> Delete(int id)
        {
            if (id <= 0) return ResultService.Fail(406, "Valor informado inválido");

            var category = await _repository.FindById(id);
            if (category == null) return ResultService.Fail(404, "Categoria não encontrada");

            var deletionResult = await _repository.Delete(id);

            if (deletionResult)
            {
                return ResultService.Ok(200, "A categoria foi excluída com sucesso");
            }
            else
            {
                return ResultService.Fail(500, "Erro ao excluir a categoria do banco de dados");
            }
        }

        public async Task<ResultService<ICollection<CategoryDTO>>> FindAll()
        {
            var categories = await _repository.FindAll();
            return ResultService.Ok<ICollection<CategoryDTO>>(200, _mapper.Map<ICollection<CategoryDTO>>(categories));
        }

        public async Task<ResultService<CategoryDTO>> FindById(int id)
        {
            if (id <= 0) return ResultService.Fail<CategoryDTO>(406, "Valor informado inválido");
            var category = await _repository.FindById(id);
            if (category == null) return ResultService.Fail<CategoryDTO>(404, "Categoria não encontrada");
            return ResultService.Ok<CategoryDTO>(200, _mapper.Map<CategoryDTO>(category));
        }

        public async Task<ResultService<CategoryDTO>> Update(CategoryDTO category)
        {
            if (category == null) return ResultService.Fail<CategoryDTO>(404, "A categoria deve ser informada");
            var result = new CategoryDTOValidator().Validate(category);
            if (!result.IsValid) return ResultService.RequestError<CategoryDTO>(400, "Problemas na validação", result);
            var newCategory = await _repository.FindById((int)category.Id);
            if (newCategory == null) return ResultService.Fail<CategoryDTO>(404, "Categoria não encontrada");
            newCategory = _mapper.Map<CategoryDTO, Category>(category, newCategory);

            var data = await _repository.Update(newCategory);
            return ResultService.Ok<CategoryDTO>(200, _mapper.Map<CategoryDTO>(data));
        }
    }
}
