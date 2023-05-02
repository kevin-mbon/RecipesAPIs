using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RecipesAPIs.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        [BsonElement("title")]
        public string Title { get; init; }
        [BsonElement("description")]
        public string Description { get; init; }
        public IEnumerable<string> Direction { get; init; }
        public IEnumerable<string> Ingridients { get; init; }
        public DateTime Update { get; init; }

    }
}
