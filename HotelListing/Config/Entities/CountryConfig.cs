using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Config.Entities
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                 new Country
                 {
                     Id = 1,
                     Name = "Sweden",
                     ShortName = "Swe"
                 },
                new Country
                {
                    Id = 2,
                    Name = "Denmark",
                    ShortName = "Dk"
                },
                new Country
                {
                    Id = 3,
                    Name = "Norway",
                    ShortName = "Nor"
                }
            );
        }
    }
}
