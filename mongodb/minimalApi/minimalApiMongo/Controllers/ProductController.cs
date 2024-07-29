using Microsoft.AspNetCore.Mvc;
using minimalApiMongo.Domains;
using minimalApiMongo.Services;
using MongoDB.Driver;

namespace minimalApiMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Armazena os dados de acesso da collection
        /// </summary>
        private readonly IMongoCollection<Product> _product;

        /// <summary>
        /// Construtor que recebe como dependência o obj da classe MongoDbService
        /// </summary>
        /// <param name="mongoDbService">Objeto da classe MongoDbService</param>
        public ProductController(MongoDbService mongoDbService)
        {
            //obtem a collection "product"
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
        }


        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                var products = await _product.Find(FilterDefinition<Product>.Empty).ToListAsync();

                return Ok(products);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message); ;
            }
        }

        [HttpGet("{_id}")]
        public async Task<ActionResult<List<Product>>> GetOne(string _id)
        {
            try
            {
                var product = await _product.Find(z=> z.Id == _id).FirstOrDefaultAsync();

                return product is not null ? Ok(product) : NotFound();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message); ;
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostOne(Product product)
        {
            try
            {

                await _product.InsertOneAsync(product);

                return StatusCode(201,"Post realizado");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }


        [HttpDelete("{_id}")]
        public async Task<ActionResult> Delete(string _id)
        {
            try
            {

                await _product.DeleteOneAsync(x => x.Id == _id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }



        [HttpPut("{_id}")]
        public async Task<ActionResult> Update(string _id, Product product)
        {
            try
            {

                await _product.ReplaceOneAsync(z=> z.Id == _id, product);

                return Ok("Update realizado com sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }
    }
}
