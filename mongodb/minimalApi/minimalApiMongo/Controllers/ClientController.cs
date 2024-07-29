using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalApiMongo.Domains;
using minimalApiMongo.Services;
using MongoDB.Driver;

namespace minimalApiMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMongoCollection<Client> _clients;
        private readonly IMongoCollection<User> _users;
        public ClientController(MongoDbService mongoDbService)
        {
            _clients = mongoDbService.GetDatabase.GetCollection<Client>("client");
            _users = mongoDbService.GetDatabase.GetCollection<User>("user");
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var client = await _clients.Find(z => true).ToListAsync();

                foreach (var item in client)
                {
                    item.User =  await _users.Find(z=>true).FirstOrDefaultAsync();
                }

                return Ok(client);
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
                var client = await _clients.Find(z => z.Id == id).FirstOrDefaultAsync();
                client.User = await _users.Find(z=> z.Id == client.UserId).FirstOrDefaultAsync();

                return Ok(client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Client client)
        {
            try
            {
                var user = await _users.Find(z => z.Id == client.UserId).FirstOrDefaultAsync();

                if(user is null)
                {
                    return NotFound("Esse id de usuário não existe");
                }

                await _clients.InsertOneAsync(client);

                client.User = _users.Find(z => z.Id == user.Id).FirstOrDefault();

                return Ok(client);
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
                await _clients.DeleteOneAsync(z=> z.Id == id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound("Cliente não encontrado");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Client client)
        {
            try
            {
                var user = _users.Find(z => z.Id == client.UserId).FirstOrDefault();
                if(user is null)
                {
                    return NotFound("Usuário não encontrado");
                }

                await _clients.ReplaceOneAsync(z=> z.Id == id, client);

                return Ok("Usuário atualizado com sucesso");
            }
            catch (Exception e)
            {
                return NotFound("Cliente não encontrado");  
            }
        }
    }
}
