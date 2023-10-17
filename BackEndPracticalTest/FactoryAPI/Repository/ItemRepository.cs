using Dapper;
using FactoryAPI.Models;
using Microsoft.Data.SqlClient;



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

        //Dapper uses a SQL syntax
        public async Task Create(Item item)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
            await connection.ExecuteAsync("INSERT INTO Items (Name, Description, Price) VALUES (@Name, @Description,@Price)", item);
        }
        public async Task Update(Item item)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
            await connection.ExecuteAsync("UPDATE Items SET Name = @Name, Description = @Description, Price = @Price WHERE ID = @ID)", item);
        }
        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
            await connection.ExecuteAsync("DELETE FROM Items WHERE ID = @ID", new { ID = id });
        }
        public async Task<IEnumerable<Item>> GetAllItems()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
            return await SelectAllItems(connection);   
        }
        public async Task<Item> GetSingleItem(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("FactoryDatabase"));
            return await connection.QueryFirstAsync<Item>("SELECT * FROM Items WHERE ID = @ID", new { ID = id });
        }
        //Extrated repeatable part of method
        public async Task<IEnumerable<Item>> SelectAllItems(SqlConnection connection)
        {
            return await connection.QueryAsync<Item>("SELECT * FROM Items");
        }
    }
}

