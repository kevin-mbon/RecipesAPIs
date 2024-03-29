using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using RecipesAPIs.Models;
using RecipesAPIs.Services;
using System.Reflection;
using Amazon.Runtime.Internal;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.Configure<RecipesStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(RecipesStoreDatabaseSettings)));
builder.Services.AddSingleton<IRecipetoreDatabaseSetting>(sp =>
sp.GetRequiredService<IOptions<RecipesStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
new MongoClient(builder.Configuration.GetValue<string>("RecipesStoreDatabaseSettings:ConnectionString"))).AddSwaggerGenNewtonsoftSupport();

builder.Services.AddScoped<IRecipeServices, RecipeServices>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>

c.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "Recipes",
    Description = "to carry domestic work",
    Contact = new OpenApiContact
    {
        Name = "Kevin",
        Email = "kevin@mail.com"
    }

}
//xml documentation 

//c.IncludeXmlComments(xmlPath);
//var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

//c.CustomOperationIds(apiDiscription =>
//    {
//        return apiDiscription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;

//    }

));





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();


}
else {
    app.UseExceptionHandler("/error");
}
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
