using MongoDB.Bson;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
namespace RecipesAPIs.Models
{
    public record Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty; 
        [BsonElement("title")]
        [Required]
        public string Title { get; init; }
        [BsonElement("description")]
        public string Description { get; init; }
        [Required]
        public IEnumerable<string> Direction { get; init; }
        [Required]
        public IEnumerable<string> Ingridients { get; init; }
        public DateTime Update { get; init; }
        public override string ToString()
        {
            return $"Id: {Id} Title {Title} ";
        }
        

    }
}
