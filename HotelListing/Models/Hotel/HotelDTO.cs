using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Models
{
    public class HotelDTO : HotelCreateDTO
    {
        public int Id { get; set; }

        //[ForeignKey("CountryId")]
        public CountryDTO Country { get; set; }
    }
}
