using System.ComponentModel.DataAnnotations;

namespace ECommerceProductAPI.Model.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public double Price { get; set; }
        public string Specification { get; set; } = null;
    }
}
