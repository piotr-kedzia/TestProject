using Dapper;
using FactoryAPI.Models;
using FactoryAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using FactoryAPI.Repository;
using FactoryAPI.Services;

namespace FactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ItemsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IItemRepository _itemRepository;
        private readonly IItemService _itemService;


        public ItemsController(IConfiguration config, IItemRepository itemRepository, IItemService itemService)
        {
            _config = config;
            _itemRepository = itemRepository;
            _itemService = itemService;
            
        }



    // Create a new item
    [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemDto item)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
            var newItem = await connection.ExecuteAsync("INSERT INTO Items (Name, Description, Price) VALUES (@Name, @Description,@Price)", item);
            return Ok();
        }

        // Retrieve a single item
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id) 
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
            var item = await connection.QueryAsync<Item>("SELECT * FROM Items WHERE ID is = @ID", new { Id = id});
            return Ok(item); //zwrócic Item
        }

        // Retrieve all items (only if you'll be using an ORM framework)
        [HttpGet]
        public async Task<IActionResult>GetAll(ItemService itemService)
        {
            var getAllItems = itemService.GetAll;
            return Ok(getAllItems);
        }


        // Update an existing item
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ItemDto item)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
            var newItem = await connection.ExecuteAsync("UPDATE Items SET Name = @Name, Description = @Description, Price = @Price WHERE ID = @ID)", item);
            return Ok();
        }

        // Delete an item
        [HttpDelete]
        [Route("{ID}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
            await connection.ExecuteAsync("DELETE FROM Items WHERE ID = @ID", new { ID = id });
            return NoContent();
        }

    }
}
