using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalApiMongo.Domains;
using minimalApiMongo.Services;
using MongoDB.Driver;

namespace minimalApiMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _orders;
        private readonly IMongoCollection<Client> _clients;
        private readonly IMongoCollection<Product> _products;
        public OrderController(MongoDbService mongoDbService)
        {
            _clients = mongoDbService.GetDatabase.GetCollection<Client>("client");
            _orders = mongoDbService.GetDatabase.GetCollection<Order>("order");
            _products = mongoDbService.GetDatabase.GetCollection<Product>("product");
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var order = await _orders.Find(z => true).ToListAsync();

                foreach (var item in order)
                {
                    item.Client = await _clients.Find(z => z.Id == item.ClientId).FirstOrDefaultAsync();

                    foreach (var product in item.ProductId)
                    {
                        var singleProduct = await _products.Find(z => z.Id == product).FirstOrDefaultAsync();
                        item.Product.Add(singleProduct);
                    }

                }

                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            try
            {
                var order = await _orders.Find(z => z.Id == id).FirstOrDefaultAsync();
                order.Client = await _clients.Find(z => z.Id == order.ClientId).FirstOrDefaultAsync();


                foreach (var product in order.ProductId)
                {
                    var singleProduct = await _products.Find(z => z.Id == product).FirstOrDefaultAsync();

                    if (singleProduct != null)
                    {
                        order.Product.Add(singleProduct);
                    }
                    else
                    {
                        return BadRequest($"Product with ID {product} not found.");
                    }
                }

                

                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {
            try
            {
                var client = await _clients.Find(z => z.Id == order.ClientId).FirstOrDefaultAsync();
                if (client is null)
                {
                    return NotFound("Esse id de cliente não existe");
                }
                foreach (var item in order.ProductId)
                {
                    var product = await _products.Find(z => z.Id == item).FirstOrDefaultAsync();
                    if (product is null)
                    {
                        return NotFound("Esse id de produto não existe");
                    }
                }

                await _orders.InsertOneAsync(order);

                order.Client = client;

                return Ok(order);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _orders.DeleteOneAsync(z => z.Id == id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound("Pedido não encontrado");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Order order)
        {
            try
            {
                var client = await _clients.Find(z => z.Id == order.ClientId).FirstOrDefaultAsync();
                if (client is null)
                {
                    return NotFound("Esse id de cliente não existe");
                }
                foreach (var item in order.ProductId)
                {
                    var product = await _products.Find(z => z.Id == item).FirstOrDefaultAsync();
                    if (product is null)
                    {
                        return NotFound("Esse id de produto não existe");
                    }
                }

                await _orders.ReplaceOneAsync(z => z.Id == id, order);

                return Ok("Pedido atualizado com sucesso");
            }
            catch (Exception e)
            {
                return NotFound("Pedido não encontrado");
            }
        }
    }
}
