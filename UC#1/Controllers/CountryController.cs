using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Text.Json;
using System.Xml.Linq;
using UC_1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UC_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private const string URL = "https://restcountries.com/v3.1/all";
        private static readonly List<Country> countriesData;

        public CountryController()
        {
            if (countriesData == null)
            {
                GetCountriesData();
            }
        }

        // GET: api/<CountryController>
        [HttpGet]
        public async Task<List<Country>> GetCountries(
            string nameFilter = "not",
            int populationFilter = 0,
            string nameSort = "not",
            int numberOfPages = 0)
        {
            return countriesData ?? new List<Country>();
        }

        public List<Country> GetCountriesFirteredByName(string nameFilter) 
        {
            if (string.IsNullOrEmpty(nameFilter))
            {
                return countriesData;
            }
            return countriesData.Where(x => x.Name.Common.Contains(nameFilter, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public List<Country> GetCountriesFirteredByPopulation(int popFilter)
        {
            if (popFilter < 1)
            {
                return countriesData;
            }
            return countriesData.Where(x => x.Population < (popFilter * 1000000)).ToList();
        }

        public List<Country> GetCountriesSortedByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                if (name.ToLower() == "ascend")
                {
                    return countriesData.OrderBy(x => x.Name.Common).ToList();
                }
                else if (name.ToLower() == "descend")
                {
                    return countriesData.OrderByDescending(x => x.Name.Common).ToList();
                }
            }

            return countriesData;
        }

        private async void GetCountriesData()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(URL);
                if (!response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error occures during HTTP request:{response.StatusCode}");
                }

                var countryDataString = await response.Content.ReadAsStringAsync();
                var countries = JsonSerializer.Deserialize<List<Country>>(countryDataString);
                if (countries != null)
                {
                    countriesData.AddRange(countries);
                }
            }
        }
    }
}
