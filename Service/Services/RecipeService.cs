using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Service.DTOs;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResultService<RecipeDTO>> Create(ResponseRecipeDTO recipeResponse)
        {
            var recipe = _mapper.Map<RecipeDTO>(recipeResponse);
            if (recipe == null) return ResultService.Fail<RecipeDTO>(400, "A receita deve ser informada");
            var result = new RecipeDTOValidator().Validate(recipe);
            if (!result.IsValid) return ResultService.RequestError<RecipeDTO>(400, "Problemas na validação", result);

            var isUnique = await _repository.IsRecipeUnique(recipe.Title);
            if (!isUnique) return ResultService.Fail<RecipeDTO>(400, "Já existe uma receita com esse nome");

            var newRecipe = _mapper.Map<Recipe>(recipe);
            var data = await _repository.Create(newRecipe);
            return ResultService.Ok<RecipeDTO>(201, _mapper.Map<RecipeDTO>(data));
        }

        public async Task<ResultService> Delete(int id)
        {
            if (id <= 0) return ResultService.Fail(406, "Valor informado inválido");
            var recipe = await _repository.FindById(id);
            if (recipe == null) return ResultService.Fail<RecipeDTO>(404, "Receita não encontrada");

            await _repository.Delete(id);
            return ResultService.Ok(200, "A receita foi excluída com sucesso");
        }

        public async Task<ResultService<ICollection<RecipeDTO>>> FindAll()
        {
            var recipes = await _repository.FindAll();
            return ResultService.Ok<ICollection<RecipeDTO>>(200, _mapper.Map<ICollection<RecipeDTO>>(recipes));
        }

        public async Task<ResultService<RecipeDTO>> FindById(int id)
        {
            if (id <= 0) return ResultService.Fail<RecipeDTO>(406, "Valor informado inválido");
            var recipe = await _repository.FindById(id);
            if (recipe == null) return ResultService.Fail<RecipeDTO>(404, "Receita não encontrada");
            return ResultService.Ok<RecipeDTO>(200, _mapper.Map<RecipeDTO>(recipe));
        }

        public async Task<ResultService<RecipeDTO>> Update(RecipeDTO recipe)
        {
            if (recipe == null) return ResultService.Fail<RecipeDTO>(404, "A receita deve ser informada");
            var result = new RecipeDTOValidator().Validate(recipe);
            if (!result.IsValid) return ResultService.RequestError<RecipeDTO>(400, "Problemas na validação", result);

            var newRecipe = await _repository.FindById(recipe.Id);
            if (newRecipe == null) return ResultService.Fail<RecipeDTO>(404, "Receita não encontrada");

            newRecipe = _mapper.Map<RecipeDTO, Recipe>(recipe, newRecipe);

            var data = await _repository.Update(newRecipe);
            return ResultService.Ok<RecipeDTO>(200, _mapper.Map<RecipeDTO>(data));
        }
    }
}
