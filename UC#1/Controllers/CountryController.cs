using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UC_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        // GET: api/<CountryController>
        [HttpGet]
        public async Task<string> GetCountries(
            string nameFilter = "not",
            int populationFilter = 0,
            string nameSort = "not",
            int numberOfPages = 0)
        {
            using (var client = new HttpClient())
            {
                var url = "https://restcountries.com/v3.1/all";
                var response = await client.GetAsync(url);
                var countryData = await response.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<List<object>>(countryData);
                return countryData;
            }
        }
    }
}
