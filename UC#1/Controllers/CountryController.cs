using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UC_1.Models;

namespace UC_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private const string URL = "https://restcountries.com/v3.1/all";
        private static readonly List<Country> countriesData;
        private static List<Country> EmptyCountryList => new List<Country>();

        static  CountryController()
        {
            if (countriesData == null)
            {
                countriesData = GetCountriesData().Result;
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<List<Country>> GetCountries(
            string nameFilter = null,
            int populationFilter = 0,
            string nameSort = null,
            int numberOfPages = 0)
        {
            var result = new List<Country>();

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                result = GetCountriesFirteredByName(nameFilter);
            }

            if (populationFilter != 0)
            {
                var popFilteredCountries = GetCountriesFirteredByPopulation(populationFilter);
                if (result.Count == 0)
                {
                    result = popFilteredCountries;
                }
                else if(popFilteredCountries.Count > 0)
                {
                    result = result.Where(x => popFilteredCountries.Contains(x)).ToList();
                }
            }
            if (!string.IsNullOrWhiteSpace(nameSort))
            {
                var sortedCountries = GetCountriesSortedByName(nameSort);
                if (result.Count == 0)
                {
                    result = sortedCountries;
                }
                else if (sortedCountries.Count > 0)
                {
                    result = sortedCountries.Where(x => result.Contains(x)).ToList();
                }
            }
            if (numberOfPages != 0)
            {
                if (result.Count == 0)
                {
                    result = GetNumberOfCountries(numberOfPages);
                }
                else
                {
                    result = GetNumberOfCountries(numberOfPages, result);
                }
            }

            return result;
        }

        [HttpGet]
        [Route("[action]")]
        public List<Country> GetCountriesFirteredByName(string nameFilter) 
        {
            if (string.IsNullOrEmpty(nameFilter))
            {
                return EmptyCountryList;
            }
            return countriesData.Where(x => x.Name.Common.Contains(nameFilter, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        [HttpGet]
        [Route("[action]")]
        public List<Country> GetCountriesFirteredByPopulation(int popFilter)
        {
            if (popFilter < 1)
            {
                return EmptyCountryList;
            }
            return countriesData.Where(x => x.Population < (popFilter * 1000000)).ToList();
        }

        [HttpGet]
        [Route("[action]")]
        public List<Country> GetCountriesSortedByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
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

            return EmptyCountryList;
        }

        [HttpPost]
        [Route("[action]")]
        public List<Country> GetNumberOfCountries(int numberOfCountries, List<Country> countries = null)
        {
            if (numberOfCountries < 1)
            {
                return EmptyCountryList;
            }
            if (numberOfCountries > countries.Count)
            {
                numberOfCountries = countries.Count;
            }
            return countries == null ? countriesData.GetRange(0, numberOfCountries) : countries.GetRange(0, numberOfCountries);
        }

        private static async Task<List<Country>> GetCountriesData()
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
                return countries ?? EmptyCountryList;
            }
        }
    }
}
