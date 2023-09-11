using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;

namespace WebApiReceitinhas.Controllers
{
  //  [Authorize(Policy = "CombinedPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        /// <summary>
        /// Retorna uma lista com todas as receitas cadastradas.
        /// </summary>
        /// <response code="200">Retorna uma lista com todas as receitas cadastradas.</response>
        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            var result = await _recipeService.FindAll();
            if (result.Code == 200) return Ok(result);
            return BadRequest(result.Data);
        }

        /// <summary>
        /// Realiza busca por ID para localizar uma receita específica.
        /// </summary>
        /// <response code="200">Retorna uma receita específica.</response>
        /// <response code="404">Receita não encontrada.</response>
        /// <response code="406">Caractere inaceitável.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id)
        {
            var result = await _recipeService.FindById(id);
            if (result.Code == 406) return Problem(
                      statusCode: 406, title: "Caractere inaceitavel");
            else if (result.Code == 404) return NotFound(result);
            return Ok(result.Data);
        }

        /// <summary>
        /// Cria uma nova receita.
        /// </summary>
        /// <remarks>
        /// Exemplo com campos obrigatórios:
        ///
        ///     POST /Recipe/register
        ///     {
        ///        "title": "Brigadeiro",
        ///        "photo": "https://cdn.sodiedoces.com.br/wp-content/uploads/2021/11/26110739/brigadeiro_615x500px.png",
        ///        "preparationMethod": "Coloque todos os ingredientes em uma panela. Misture em fogo baixo até soltar do fundo da panela",
        ///        "preparationTime": "20 minutos",
        ///        "cost": 10.00,
        ///        "difficulty": "Easy",
        ///        "categoryId": 1,
        ///        "ingredients": [
        ///        "1 lata de leite condensado",
        ///        "4 colheres de sopa de achocolatado",
        ///        "1 colher de sopa rasa de manteiga"
        ///        ]
        ///     }
        /// </remarks>
        /// <response code="201">Retorna a receita recém criada.</response>
        /// <response code="400">Solicitação inválida. Esse erro ocorre quando algum campo obrigatório não foi devidamente preenchido.</response>
        [HttpPost("register")]
        public async Task<ActionResult> Create([FromBody] ResponseRecipeDTO recipeDTO)
        {
            var result = await _recipeService.Create(recipeDTO);
            if (result.Code == 400) return BadRequest(result);
            return Created("Created", result.Data);
        }

        /// <summary>
        /// Atualiza uma receita.
        /// </summary>
        /// <remarks>
        /// Exemplo com campos obrigatórios:
        ///
        ///     PUT /Recipe
        ///     {
        ///        "id": 1,
        ///        "title": "Brigadeiro de Colher",
        ///        "photo": "https://cdn.sodiedoces.com.br/wp-content/uploads/2021/11/26110739/brigadeiro_615x500px.png",
        ///        "preparationMethod": "Coloque todos os ingredientes em um pote alto. Leve ao microondas por 3 minutos. Misture. Leve ao microondas por mais 2 minutos.",
        ///        "preparationTime": "10 minutos",
        ///        "cost": 10.00,
        ///        "difficulty": "Easy",
        ///        "categoryId": 1,
        ///        "ingredients": [
        ///        "1 lata de leite condensado",
        ///        "4 colheres de sopa de achocolatado",
        ///        "1 colher de sopa rasa de manteiga"
        ///        ]
        ///     }
        /// </remarks>
        /// <response code="200">Retorna a receita recém atualizada.</response>
        /// <response code="400">Solicitação inválida. Esse erro ocorre quando algum campo obrigatório não foi devidamente preenchido.</response>
        /// <response code="404">Receita não encontrada.</response>
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] RecipeDTO recipeDTO)
        {
            var result = await _recipeService.Update(recipeDTO);
            if (result.Code == 404) return NotFound(result);
            else if (result.Code == 400) return BadRequest(result);
            return Ok(result.Data);
        }

        /// <summary>
        /// Exclui uma receita específica.
        /// </summary>
        /// <remarks>
        /// Informe o ID da receita que deseja deletar.
        /// </remarks>
        /// <response code="200">Retorna uma mensagem informando que a receita foi excluída com sucesso.</response>
        /// <response code="400">Solicitação inválida.</response>
        /// <response code="404">Receita não encontrada.</response>
        /// <response code="406">Caractere inaceitável.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _recipeService.Delete(id);
            if (result.Code == 406) return Problem(
                        statusCode: 406, title: "Caractere inaceitavel");
            else if (result.Code == 404) return NotFound(result);
            return Ok(result.Message);
        }
    }
}
