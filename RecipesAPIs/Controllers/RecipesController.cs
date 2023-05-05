using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipesAPIs.Models;
using RecipesAPIs.Services;
using Microsoft.AspNetCore.JsonPatch;
using RecipesAPIs;
using System.Collections.Immutable;
using System.Net.Mime;

namespace RecipesAPIs.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeServices recipeServices;

        public RecipesController(IRecipeServices recipeServices)
        {
            this.recipeServices = recipeServices;
        }
      
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Recipe>> GetRecipesByCount([FromQuery] int count = 3)
        {
            if (count <= 0)
            {
                throw new ArgumentException("INVALID COUNT ", nameof(count));
            }

            var recipes = recipeServices.GetRecipe(count);

            if (!recipes.Any())
            {
                return NotFound();
            }

            return Ok(recipes);
        }

        //return recipeServices.Get();
    
    /// <summary>
    /// return recipe by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Recipe> Get(string id)
        {
            var recipe = recipeServices.Get(id);
            if (recipe == null)
            {
                return NotFound($"recipe of this id = {id} not found");

            }
            return recipe;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Recipe> Post([FromBody] Recipe recipe)
        {
            recipeServices.Create(recipe);
            return CreatedAtAction(nameof(Get), new { id = recipe.Id }, recipe);

        }
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Recipe> UpdadeRecipe(string id, [FromBody] JsonPatchDocument<Recipe> recipeUpdate)
        {
            var recipe = recipeServices.Get(id);
            if (recipe == null)
            {
                return NotFound();
            }
            recipeUpdate.ApplyTo(recipe);
            recipeServices.Update(id, recipe);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Recipe> PutRecipes(string id, Recipe recipe)
        {
            var existingRecipe = recipeServices.Get(id);
            if (existingRecipe == null)
            {
                return NotFound($"give id = {id}not found");
            }

            recipeServices.Update(id, recipe);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteRecipes(string id)
        {
            var recipe = recipeServices.Get(id);
            if (recipe == null)
            {
                return NotFound($"THIS ID = {id} NOT FOUUND");
            }
            recipeServices.Remove(recipe.Id);
            return Ok($"recipe of this id = {id} succefully deleted");
        }
    }
}


