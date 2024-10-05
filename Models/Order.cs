using System.ComponentModel.DataAnnotations;

namespace EcommerceShoppingApp.Models
{
    public class Order
    {
        [Key]
        public int oId { get; set; }
        [Required]
        public int oUserId { get; set; }
        [Required]
        public int oProductId { get; set; }
        [Required]
        public string oUniqueOrderId { get; set; }
    }
}
