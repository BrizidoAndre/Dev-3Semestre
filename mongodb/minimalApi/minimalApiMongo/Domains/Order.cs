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


        [BsonElement("clientId")]
        public string? ClientId { get; set; }

        [BsonIgnoreIfDefault]
        public Client? Client { get; set; }


        //Referencias aos produtos do pedido

        //Referência para que eu consiga cadastrar um pedido com os produtos

        [BsonIgnoreIfDefault]
        [BsonElement("productId")]
        public List<string> ProductId { get; set; }

        // referência para que quando eu liste os pedidos, venham os dados de cada produto(lista)
        [BsonIgnoreIfDefault]
        public List<Product>? Product { get; set; }

        public Order()
        {
            Product = new List<Product>();
        }

    }
}
