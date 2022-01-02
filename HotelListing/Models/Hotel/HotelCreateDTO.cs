using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Models
{
    public class HotelCreateDTO
    {

        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Hotel name is too long!")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Address name is too long!")]
        public string Address { get; set; }

        [Required]
        [Range(1,5)]
        public double Rating { get; set; }

        //[Required]
        public int CountryId { get; set; }
    }
}
