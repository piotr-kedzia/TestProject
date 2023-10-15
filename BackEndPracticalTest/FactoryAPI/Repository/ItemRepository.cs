using Dapper;
using FactoryAPI.Models;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;


namespace FactoryAPI.Repository
{

    public interface IItemRepository
    {

        Task Create(Item item);
        Task Update(Item item);
        Task Delete(int id);
        Task<Item> GetSingleItem(int id);
        Task<IEnumerable<Item>> GetAllItems();
    }


    public class ItemRepository : IItemRepository
    {
        private readonly IConfiguration _config;


        public ItemRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task Create(Item item)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
            var newItem = await connection.ExecuteAsync("INSERT INTO Items (Name, Description, Price) VALUES (@Name, @Description,@Price)", item);
        }

        public async Task Update(Item item)
        {

        }

        public async Task Delete(int id)
        {

        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
            return await connection.QueryAsync<Item>("SELECT * FROM Items");
        }

        public Task<Item> GetSingleItem(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<Item> GetSingleITem(int id)
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
        //    return await connection.QueryAsync<Item>("SELECT * FROM Items WHERE ID is = @ID", new { Id = id });

        //}

        //public Task<Item> GetSingleItem(int id)
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
        //    throw new NotImplementedException();
        //}
    }
}

