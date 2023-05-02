using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipesAPIs.Models;
using RecipesAPIs.Services;
using RecipesAPIs;

namespace RecipesAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeServices recipeServices;

        public RecipesController(IRecipeServices recipeServices)
        {
            this.recipeServices = recipeServices;
        }
        [HttpGet]
        public ActionResult<List<Recipe>> Get()
        {
            return recipeServices.Get();
        }
        [HttpGet("{id}")]
        public ActionResult<Recipe> Get(string id)
        {
            var recipe = recipeServices.Get(id);
            if(recipe == null)
            {
                return NotFound($"recipe of this id = {id} not found");

            }
            return recipe;
        }
        [HttpPost]
        public ActionResult<Recipe> Post([FromBody]Recipe recipe)
        {
            recipeServices.Create(recipe);
            return CreatedAtAction(nameof(Get), new { id = recipe.Id }, recipe);

        }
        [HttpPut("{id}")]
        public ActionResult<Recipe> PutRecipes(string id, Recipe recipe)
        {
            var existingRecipe = recipeServices.Get(id);
            if(existingRecipe == null)
            {
                return NotFound($"give id = {id}not found");
            }
            recipeServices.Update(id, recipe);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteRecipes(string id)
        {
            var recipe = recipeServices.Get(id);
            if(recipe == null)
            {
                return NotFound($"THIS ID = {id} NOT FOUUND");
            }
            recipeServices.Remove(recipe.Id);
            return Ok($"recipe of this id = {id} succefully deleted");
        }
    }
}
