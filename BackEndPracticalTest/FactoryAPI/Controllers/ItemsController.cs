using Dapper;
using FactoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using FactoryAPI.Services;

namespace FactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // model validated by controller
    public class ItemsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IItemService _itemService;

        public ItemsController(IConfiguration config, IItemService itemService)
        {
            _config = config;
            _itemService = itemService;
        }

    // Create a new item
    [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemDto item)
        {

            _itemService.Create(item);
            return Ok();

        }

        // Retrieve a single item
        [HttpGet]
        [Route("{ID}")]
        public async Task<IActionResult> GetSingle([FromRoute] int ID) 
        {
            var getSingle = await _itemService.GetSingle(ID);
            return Ok(getSingle);
        }
        // Retrieve all items (only if you'll be using an ORM framework)
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var getAllItems = await _itemService.GetAll();
            return Ok(getAllItems);
        }
        // Update an existing item
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Item item)
        {
            var uptadeItem = await _itemService.Update(item);
            return Ok(uptadeItem);
        }
        // Delete an item
        [HttpDelete]
        [Route("{ID}")]
        public async Task<IActionResult> Delete([FromRoute] int ID)
        {
            await _itemService.Delete(ID);                       
            return Ok();
        }
    }
}
