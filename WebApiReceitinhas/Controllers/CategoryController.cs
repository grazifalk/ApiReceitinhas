using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Service.Services;

namespace WebApiReceitinhas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Retorna uma lista com todas as categorias cadastradas.
        /// </summary>
        /// <response code="200">Retorna uma lista com todas as categorias cadastradas.</response>
        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            var result = await _categoryService.FindAll();
            if (result.Code == 200) return Ok(result);
            return BadRequest(result.Data);
        }

        /// <summary>
        /// Realiza busca por ID para localizar uma categoria específica.
        /// </summary>
        /// <response code="200">Retorna uma categoria específica.</response>
        /// <response code="404">Categoria não encontrada.</response>
        /// <response code="406">Caractere inaceitável.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id)
        {
            var result = await _categoryService.FindById(id);
            if (result.Code == 406) return Problem(
                      statusCode: 406, title: "Caractere inaceitavel");
            else if (result.Code == 404) return NotFound(result);
            return Ok(result.Data);
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <remarks>
        /// Exemplo com campos obrigatórios:
        ///
        ///     POST /Category/register
        ///     {
        ///        "name": "Doces"
        ///     }
        /// </remarks>
        /// <response code="201">Retorna a categoria recém criada.</response>
        /// <response code="400">Solicitação inválida. Esse erro ocorre quando algum campo obrigatório não foi devidamente preenchido.</response>
        [HttpPost("register")]
        public async Task<ActionResult> Create([FromBody] CategoryDTO categoryDTO)
        {
            var result = await _categoryService.Create(categoryDTO);
            if (result.Code == 400) return BadRequest(result);
            return Created("Created", result.Data);
        }

        /// <summary>
        /// Atualiza uma categoria.
        /// </summary>
        /// <remarks>
        /// Exemplo com campos obrigatórios:
        ///
        ///     PUT /Category
        ///     {
        ///        "id": 14,
        ///        "name": "Doces",
        ///     }
        /// </remarks>
        /// <response code="200">Retorna a categoria recém atualizada.</response>
        /// <response code="400">Solicitação inválida. Esse erro ocorre quando algum campo obrigatório não foi devidamente preenchido.</response>
        /// <response code="404">Categoria não encontrada.</response>
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] CategoryDTO categoryDTO)
        {
            var result = await _categoryService.Update(categoryDTO);
            if (result.Code == 404) return NotFound(result);
            else if (result.Code == 400) return BadRequest(result);
            return Ok(result.Data);
        }

        /// <summary>
        /// Exclui uma categoria específica.
        /// </summary>
        /// <remarks>
        /// Informe o ID da categoria que deseja deletar.
        /// </remarks>
        /// <response code="200">Retorna uma mensagem informando que a categoria foi excluída com sucesso.</response>
        /// <response code="400">Solicitação inválida.</response>
        /// <response code="404">Categoria não encontrada.</response>
        /// <response code="406">Caractere inaceitável.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _categoryService.Delete(id);
            if (result.Code == 406) return Problem(
                        statusCode: 406, title: "Caractere inaceitavel");
            else if (result.Code == 404) return NotFound(result);
            return Ok(result.Message);
        }
    }
}
