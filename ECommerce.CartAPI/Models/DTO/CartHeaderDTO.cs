namespace ECommerce.CartAPI.Models.DTO
{
    public class CartHeaderDTO
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        
        public double CartTotal { get; set; } = 0;
    }
}
