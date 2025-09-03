using System.Diagnostics.CodeAnalysis;

namespace CapiWear_API.DTOs
{
    public class ProductDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int Category_id { get; set; }

        public string ImageUrl { get; set; }
    }
}