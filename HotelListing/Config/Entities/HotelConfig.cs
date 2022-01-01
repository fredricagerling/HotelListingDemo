using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Config.Entities
{
    public class HotelConfig : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Det fægreste hotellet",
                    Address = "Oslo",
                    CountryId = 3,
                    Rating = 3.5
                },
                new Hotel
                {
                    Id = 2,
                    Name = "First Hotel",
                    Address = "Gothenburg",
                    CountryId = 1,
                    Rating = 4.6
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Ölhotellet",
                    Address = "Fredrikshavn",
                    CountryId = 2,
                    Rating = 4.1
                }
            );
        }
    }
}
