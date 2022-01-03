using AutoMapper;
using HotelListing.Constants;
using HotelListing.Data;
using HotelListing.Models;
using HotelListing.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public HotelController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = _mapper.Map<List<HotelDTO>>(await _unitOfWork.Hotels.GetAllAsync());
            return Ok(hotels);
        }

        [HttpGet("{id}", Name = "GetHotel")]
        [Authorize]
        public async Task<IActionResult> GetHotel(int id)
        {
            var hotel = _mapper.Map<HotelDTO>(
                await _unitOfWork.Hotels.GetAsync(
                    x => x.Id == id, includes: x => x.Include(x => x.Country)));

            return Ok(hotel);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post in {nameof(CreateHotel)}");
                return BadRequest(ModelState);
            }

            var hotel = _mapper.Map<Hotel>(hotelDTO);
            await _unitOfWork.Hotels.AddAsync(hotel);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetHotel", new { id = hotel.Id }, hotel);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelUpdateDTO hotelDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Update in {nameof(UpdateHotel)}");
                return BadRequest(ModelState);
            }

            var hotel = await _unitOfWork.Hotels.GetAsync(h => h.Id == id);
            if (hotel == null)
            {
                _logger.LogError($"Invalid update in {nameof(UpdateHotel)}");
                return BadRequest($"Hotel with id of {id} does not exist.");
            }
            _mapper.Map(hotelDTO, hotel);
            _unitOfWork.Hotels.Update(hotel);
            await _unitOfWork.Save();

            return NoContent();

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid Delete in {nameof(DeleteHotel)}");
                return BadRequest(ModelState);
            }

            var hotel = await _unitOfWork.Hotels.GetAsync(h => h.Id == id);

            if (hotel == null)
            {
                _logger.LogError($"Invalid delete in {nameof(DeleteHotel)}");
                return BadRequest($"Hotel with id of {id} does not exist.");
            }

            await _unitOfWork.Hotels.DeleteAsync(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
