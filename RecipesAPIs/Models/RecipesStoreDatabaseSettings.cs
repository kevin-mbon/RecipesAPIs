namespace RecipesAPIs.Models
{
    public class RecipesStoreDatabaseSettings : IRecipetoreDatabaseSetting
    {
      public  string RecipeCollectionName { get; set; }
     public   string ConnectionString { get; set; }
     public   string DatabaseName { get; set; }
    }
}
