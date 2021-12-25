using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class CountryCreateDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country name is too long!")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 5, ErrorMessage = "Short name is too long!")]
        public string ShortName { get; set; }
    }
}
