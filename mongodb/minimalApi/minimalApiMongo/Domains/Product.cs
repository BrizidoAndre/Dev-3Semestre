using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace minimalApiMongo.Domains
{
    public class Product
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


        [BsonElement("price")]
        public float? Price { get; set; }
    
        //adiciona um dicionário para atributos adicionais, ou seja, caso o registro 
        public Dictionary<string, string> AdditionalAttributes { get; set; }
    
        /// <summary>
        /// Ao ser instanciado um obj da classe Product, o atributo "AdditionalAttributes"
        /// já virá com um novo dicionário e portanto habilitado para adicionar + attributos
        /// </summary>
        public Product()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
