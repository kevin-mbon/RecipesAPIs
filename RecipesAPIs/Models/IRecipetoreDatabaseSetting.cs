namespace RecipesAPIs.Models
{
    public interface IRecipetoreDatabaseSetting
    {
         string RecipeCollectionName { get; set; }
         string ConnectionString { get; set; }
         string  DatabaseName { get; set; }
    }
}
