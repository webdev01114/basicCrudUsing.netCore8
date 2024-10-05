using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceShoppingApp.Models
{
    public class Cart
    {
        [Key]
        public int cId { get; set; }
        [Required]
        public int cUserId { get; set; }
        [Required]
        public int cProductId { get; set; }


        [NotMapped]
        public string CProductName { get; set; }
        [NotMapped]
        public string CProductDesc { get; set; }
        [NotMapped]
        public int CProductPrice { get; set; }
        [NotMapped]
        public string CProductImage { get; set; }
    }
}
