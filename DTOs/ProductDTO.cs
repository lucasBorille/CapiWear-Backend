namespace CapiWear_API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Category_id { get; set; }

        public string ImageUrl { get; set; }
    }
}