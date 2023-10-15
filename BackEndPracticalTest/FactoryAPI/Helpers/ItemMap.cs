using FactoryAPI.Models;

namespace FactoryAPI.Helpers
{
    public static class ItemMap
    {
        public static ItemDto ItemToDto(Item item)
        {
            var itemDto = new ItemDto();
            itemDto.Id = item.Id;
            itemDto.Name = item.Name;
            itemDto.Description = item.Description;
            itemDto.Price = item.Price;

            return itemDto;
        }
        public static Item DtoToItem(ItemDto itemDto, Item item = null)
        {
            item ??= new Item();
            item.Id = itemDto.Id;
            item.Name = itemDto.Name;
            item.Description = itemDto.Description;
            item.Price = itemDto.Price;

            return item;
        }

    }
}
