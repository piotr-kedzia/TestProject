using System.ComponentModel.DataAnnotations;

namespace FactoryAPI.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        [Range(1, 9999)]
        public int Price { get; set; }

    }
}
