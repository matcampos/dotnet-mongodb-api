using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MongodbApi.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonElement("Name")]
        [JsonProperty("name")]
        public string BookName { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }
    }
}   