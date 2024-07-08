using System.ComponentModel.DataAnnotations;

namespace ECommerce.CartAPI.Models
{
    public class CartHeader
    {
        [Key]
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        //public bool Status { get; set; } = false;
        public double CartTotal { get; set; } = 0;
    }
}
