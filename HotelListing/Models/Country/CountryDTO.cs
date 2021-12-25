using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class CountryDTO : CountryCreateDTO
    {
        public int Id { get; set; }
        public  IList<HotelDTO> Hotels { get; set; }
    }
}
