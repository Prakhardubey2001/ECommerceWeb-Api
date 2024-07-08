namespace ECommerce.CartAPI.Models.DTO
{
    public class CartDTO
    {
        public CartHeaderDTO CartHeader { get; set; }
        public IEnumerable<CartItemsDTO> CartDetails { get; set; }
    }
}
