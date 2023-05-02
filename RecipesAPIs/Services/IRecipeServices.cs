using RecipesAPIs.Models;

namespace RecipesAPIs.Services
{
    public interface IRecipeServices
    {
        List<Recipe> Get();
        Recipe Get(string id);
        Recipe Create (Recipe recipe);
        void Update(string id, Recipe recipe);
        void Remove(string id);
    }
}
