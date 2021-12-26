using HotelListing.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Country>().HasData(
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

            builder.Entity<Hotel>().HasData(


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
