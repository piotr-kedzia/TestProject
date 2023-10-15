

using FactoryAPI.Helpers;
using FactoryAPI.Models;
using FactoryAPI.Repository;
using Microsoft.Identity.Client;

namespace FactoryAPI.Services
{

    public interface IItemService
    {

        Task<ItemDto> GetSingle(int id);
        Task<IEnumerable<ItemDto>> GetAll(IItemRepository _itemRepository);

    }

    public class ItemService : IItemService
    {

        private readonly IItemRepository _itemRepository;
        public ItemService(IItemRepository ItemRepository)
        {
            _itemRepository = ItemRepository;
            
        }

        public IItemRepository Get_itemRepository()
        {
            return _itemRepository;
        }

        public async Task<IEnumerable<ItemDto>> GetAll(IItemRepository _itemRepository)
        {
            IEnumerable<Item> items = await _itemRepository.GetAllItems();
            var itemsDto = new List<ItemDto>();
            foreach(var item in items)
            {
                itemsDto.Add(ItemMap.ItemToDto(item));  
            }
            return itemsDto;
        }

        public async Task<ItemDto> GetSingle(int id)
        {

            return new ItemDto();

        }





    }


}
