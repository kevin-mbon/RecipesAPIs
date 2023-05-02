using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RecipesAPIs.Models;
using RecipesAPIs.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<RecipesStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(RecipesStoreDatabaseSettings)));
builder.Services.AddSingleton<IRecipetoreDatabaseSetting>(sp =>
sp.GetRequiredService<IOptions<RecipesStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
new MongoClient(builder.Configuration.GetValue<string>("RecipesStoreDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IRecipeServices, RecipeServices>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
