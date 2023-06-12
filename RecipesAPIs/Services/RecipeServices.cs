using RecipesAPIs.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace RecipesAPIs.Services
{
    public class RecipeServices : IRecipeServices
    {
        private readonly IMongoCollection<Recipe> _recipe;
        public RecipeServices(IRecipetoreDatabaseSetting settings, IMongoClient mongoClient) {
           var database = mongoClient.GetDatabase(settings.DatabaseName);
            _recipe = database.GetCollection<Recipe>(settings.RecipeCollectionName);
        }
        public Recipe Create(Recipe recipe)
        {
            _recipe.InsertOne(recipe);
            return recipe;
        }

        public List<Recipe> Get()
        {
            return _recipe.Find(recipe => true).ToList();
        }

        public Recipe Get(string id)
        {
            return _recipe.Find(recipe => recipe.Id == id).FirstOrDefault();
        }

        public Task RecipeById(string id,Recipe recipe)
        {
            return _recipe.ReplaceOneAsync(recipe => recipe.Id == id,recipe);
        }

        public void Remove(string id)
        {
            _recipe.DeleteOne(recipe => recipe.Id == id);
        }
       
        //public void GetRecipes(int count)
        //{

        //}

        public void Update(string id, Recipe recipe)
        {
            _recipe.ReplaceOne(recipe => recipe.Id == id, recipe);
        }

        List<Recipe> IRecipeServices.GetRecipe(int count)
        {
            return _recipe.Find(recipe => true).Limit(count).ToList();
        }
    }
}
