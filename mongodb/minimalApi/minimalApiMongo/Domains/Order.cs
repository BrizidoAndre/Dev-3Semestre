using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Reflection.Metadata.Ecma335;

namespace minimalApiMongo.Domains
{
    public class Order
    {
        //define o nome do campo no MongoDb como "_id" e o tipo como "ObjectId"
        [BsonElement("_id")]
        //define que esta prop é Id do objeto
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? Id { get; set; }


        [BsonElement("date")]
        public DateTime Date { get; set; }


        [BsonElement("status")]
        public string? Status { get; set; }


        [BsonElement("userId")]
        public string? UserId { get; set; }

        [BsonIgnoreIfDefault]
        public User? User { get; set; }


        [BsonElement("products")]
        public List<Product>? Products { get; set; }


        public Order() { 
            Products = new List<Product>();
        }

    }
}
