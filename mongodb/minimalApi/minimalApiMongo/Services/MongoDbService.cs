using MongoDB.Driver;

namespace minimalApiMongo.Services
{
    public class MongoDbService
    {
        /// <summary>
        /// Armazena a configuração da aplicação
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Armazena uma referência ao MongoDb
        /// </summary>
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Recebe a config da aplicação como parâmetro
        /// </summary>
        /// <param name="configuration"> obj configuration </param>
        public MongoDbService(IConfiguration configuration)
        {
            //Atribui a config recebida em _configuration
            _configuration = configuration;

            //Obtem a string de conexão através do _configuration
            var connectionString = _configuration.GetConnectionString("DbConnection");

            //Cria um obj MongoUrl que recebe como parâmetro a string de conexão
            var mongoUrl = MongoUrl.Create(connectionString);

            //Cria um client MongoLClient para se conectar ao MongoDb
            var mongoClient = new MongoClient(mongoUrl);


            //Obtem a referência ao bd com o nome especificado na string de conexão
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            // "mongodb://localhost:27017/ProductDatabase_Manha"
        }

        /// <summary>
        /// Propriedade para acessar o banco de dados
        /// Retorna a referência ao banco de dados
        /// </summary>
        public IMongoDatabase GetDatabase => _database;


    }
}
