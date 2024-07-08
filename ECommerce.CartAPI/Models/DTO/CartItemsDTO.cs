namespace ECommerce.CartAPI.Models.DTO
{
    public class CartItemsDTO
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; } = 1;
    }
}
