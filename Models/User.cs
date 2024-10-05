using System.ComponentModel.DataAnnotations;

namespace EcommerceShoppingApp.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }

        [Required]
        public string ?fName { get; set; }
        [Required]
        public string ?lName { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string ?userType {  get; set; }
    }
}
