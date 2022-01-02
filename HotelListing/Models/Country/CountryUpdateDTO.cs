using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class CountryUpdateDTO : CountryCreateDTO
    {
        public IList<HotelUpdateDTO> Hotels { get; set; }

    }
}
