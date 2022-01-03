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
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            var countries = _mapper.Map<List<CountryDTO>>(await _unitOfWork.Countries.GetAllAsync(requestParams));
            return Ok(countries);
        }

        [HttpGet("{id}", Name = "GetCountry")]
        [Authorize]
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = _mapper.Map<CountryDTO>(
                await _unitOfWork.Countries.GetAsync(
                    x => x.Id == id,
                    includes: x => x.Include(x => x.Hotels)));

            return Ok(country);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> CreateCountry([FromBody] CountryCreateDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post in {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }

            var country = _mapper.Map<Country>(countryDTO);
            await _unitOfWork.Countries.AddAsync(country);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] CountryUpdateDTO countryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Update in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }

            var country = await _unitOfWork.Countries.GetAsync(h => h.Id == id);
            if (country == null)
            {
                _logger.LogError($"Invalid update in {nameof(UpdateCountry)}");
                return BadRequest($"Hotel with id of {id} does not exist.");
            }
            _mapper.Map(countryDTO, country);
            _unitOfWork.Countries.Update(country);
            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid Delete in {nameof(DeleteCountry)}");
                return BadRequest(ModelState);
            }

            var country = await _unitOfWork.Countries.GetAsync(h => h.Id == id);

            if (country == null)
            {
                _logger.LogError($"Invalid delete in {nameof(DeleteCountry)}");
                return BadRequest($"Hotel with id of {id} does not exist.");
            }

            await _unitOfWork.Countries.DeleteAsync(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
