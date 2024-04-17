using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CountriesController : ControllerBase
    {
        private readonly DataContex _contex;

        public CountriesController(DataContex contex)
        {
            _contex = contex;

        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _contex.Countries.ToListAsync());
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var country =  await _contex.Countries.FindAsync(id);
            if (country == null) 
            {
                return NotFound();  
            }
            return Ok(country);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Country country)
        {
            _contex.Add(country);
            await _contex.SaveChangesAsync();
            return Ok(country);

        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Country country) 
        {
            _contex.Update(country);
           await _contex.SaveChangesAsync();
            return  NoContent();

        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var country = await _contex.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            _contex.Remove(country);
            await _contex.SaveChangesAsync();
            return NoContent();
        }
    }
}
