using System.ComponentModel.DataAnnotations;

namespace ECommerceProductAPI.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double Price { get; set; }
        public string Specification { get; set; } = null;
    }
}
