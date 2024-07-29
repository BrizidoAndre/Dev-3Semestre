using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using minimalApiMongo.Services;

namespace minimalApiMongo.Domains
{
    public class Client
    {

        //define o nome do campo no MongoDb como "_id" e o tipo como "ObjectId"
        [BsonElement("_id")]
        //define que esta prop é Id do objeto
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? Id { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("userId")]
        public string? UserId { get; set; }


        [BsonIgnoreIfDefault]
        public User? User { get; set; }


        [BsonElement("cpf")]
        public double? CPF { get; set; }


        [BsonElement("phone")]
        public double? Phone { get; set; }


        [BsonElement("address")]
        public string? Address { get; set; }


        //adiciona um dicionário para atributos adicionais, ou seja, caso o registro 
        public Dictionary<string, string> AdditionalAttributes { get; set; }

        public Client()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }

    
    }
}
