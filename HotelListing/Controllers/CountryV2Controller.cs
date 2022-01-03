using AutoMapper;
using HotelListing.Constants;
using HotelListing.Data;
using HotelListing.Models;
using HotelListing.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    //VERSIONING TEST CONTROLLER
    [ApiVersion("2.0", Deprecated = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryV2Controller : ControllerBase
    {
        private DatabaseContext _context;

        public CountryV2Controller(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            
            return Ok(_context.Countries);
        }
    }
}