namespace ECommerce.CartAPI.Models.DTO
{
    public class CheckOutDTO
    {
        public int CartHeaderId { get; set; }
        public IEnumerable<ProductDTO> ProductDtos { get; set; }
        public string UserId { get; set; }
        public double CartTotal { get; set; }
    }
}
