using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace minimalApiMongo.Domains
{
    public class User
    {
        //define o nome do campo no MongoDb como "_id" e o tipo como "ObjectId"
        [BsonElement("_id")]
        //define que esta prop é Id do objeto
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }


        [BsonElement("email")]
        public string? Email { get; set; }


        [BsonElement("password")]
        public string? Password { get; set; }


        //adiciona um dicionário para atributos adicionais, ou seja, caso o registro 
        public Dictionary<string, string> AdditionalAttributes { get; set; }

        public User()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }

    }
}
