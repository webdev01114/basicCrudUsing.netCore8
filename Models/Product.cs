using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceShoppingApp.Models
{
    public class Product
    {
        [Key]
        public int pId { get; set; }
        [Required]
        public string pName { get; set; }
        [Required]
        public string pDesc { get; set; }
        [Required]
        public string pImg { get; set; }

        [Required]
        public int pPrice { get; set; }

        [NotMapped]
        public IFormFile? Pic { get; set; }

        [NotMapped]
        public int isDisabledAddToCart { get; set; } = 0;

    }
}
