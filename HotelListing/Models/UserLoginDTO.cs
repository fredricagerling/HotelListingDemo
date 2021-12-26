using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class UserLoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Password have to be between {2} to {1} characters long", MinimumLength = 5)]
        public string Password { get; set; }
    }
}
