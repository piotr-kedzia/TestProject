using FactoryAPI.Exceptions;
using FactoryAPI.Helpers;
using FactoryAPI.Models;
using FactoryAPI.Repository;


namespace FactoryAPI.Services
{
    public interface IItemService
    {

        Task<ItemDto> GetSingle(int id);
        Task<IEnumerable<ItemDto>> GetAll();
        Task<ItemDto> Update(Item item);
        Task Delete(int id);
        Task Create(ItemDto item);
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


        public async Task<IEnumerable<ItemDto>> GetAll()
        {
            IEnumerable<Item> items = await _itemRepository.GetAllItems();

            var itemsDto = new List<ItemDto>();
            foreach(var item in items)
            {
                itemsDto.Add(ItemMap.ItemToDto(item));  
            }
            return itemsDto;
        }

        // in this services item is mapping to itemDto
        public async Task<ItemDto> GetSingle(int id)
        {
            try
            {
                Item item = await _itemRepository.GetSingleItem(id);
                var itemDto = ItemMap.ItemToDto(item);
                return itemDto;
            }
            catch (Exception ex)
            {
                throw new NotFoundCustomException($"Not Found. {ex.Message}");
            }
        }
        public async Task<ItemDto> Update(Item item)
        {

            await _itemRepository.Update(item);
            var itemDto = ItemMap.ItemToDto(item);
            return itemDto;

        }
        public async Task Delete(int id)
        {
            try
            {
                await _itemRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new NotFoundCustomException($"Not Found. {ex.Message}");
            }
        }

        public async Task Create(ItemDto itemDto)
        {
            try
            {
                var item = ItemMap.DtoToItem(itemDto);
                await _itemRepository.Create(item);
            }
            catch (Exception ex) { throw new NullCustomException($"Name parameter is empty. {ex.Message}"); }
        }
    }


}
