using HotelListing.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Config.Entities
{
    public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = Roles.User,
                    NormalizedName = Roles.User.ToUpper(),
                },
                new IdentityRole
                {
                    Name = Roles.Administrator,
                    NormalizedName = Roles.Administrator.ToUpper(),

                }
            );
        }
    }
}
