using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RecipesAPIs.Controllers;
using RecipesAPIs.Models;
using RecipesAPIs.Services;
using System.Threading.Tasks;


namespace Recipes.Tests
{
    public class RecipeControllerTest
    {


        public class RecipesControllerTests
        {

            //[Fact]
            public async Task Get_Recipe_Return_Collect_Number_Of_Recipes()
            {
                int count = 5;
                //Arrange
                var fakeRecipes = A.CollectionOfDummy<Recipe>(count).AsEnumerable();
                var dataStore = A.Fake<IRecipeServices>();
                

              //  A.CallTo(() => dataStore.GetRecipe(count)).Returns(Task.FromResult(fakeRecipes));
                var controller = new RecipesController(dataStore);

                //Act

                var actionResult = controller.GetRecipesByCount(count);
                //Assert
                var result = actionResult.Result as OkObjectResult;
                var resultRecipes = result.Value as IEnumerable<Recipe>;
                Assert.Equal(count, resultRecipes.Count());
            }
        }
    }
}

