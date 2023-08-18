using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Text.Json;
using UC_1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UC_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        // GET: api/<CountryController>
        [HttpGet]
        public async Task<List<Country>> GetCountries(
            string nameFilter = "not",
            int populationFilter = 0,
            string nameSort = "not",
            int numberOfPages = 0)
        {
            var countryList = new List<Country>();

            using (var client = new HttpClient())
            {
                var url = "https://restcountries.com/v3.1/all";
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error occures during HTTP request:{response.StatusCode}");

                    return countryList;
                }

                var countryDataString = await response.Content.ReadAsStringAsync();
                var countries = JsonSerializer.Deserialize<List<Country>>(countryDataString);
                if (countries != null)
                {
                    countryList.AddRange(countries);
                }
            }

            return countryList;
        }

        public List<Country> GetCountriesFirteredByName(List<Country> countries, string nameFilter) 
        {
            return countries.Where(x => x.Name.Common.Contains(nameFilter, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        public List<Country> GetCountriesFirteredByPopulation(List<Country> countries, int popFilter)
        {
            return countries.Where(x => x.Population < (popFilter * 1000000)).ToList();
        }
    }
}
