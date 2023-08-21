This is a .NET wrapper library around the API provided by REST Countries https://restcountries.com (Get information about countries via a RESTful API).

This library could be used for:
Get countries filtered by name and/or population, sorted ascend or descend by the name, and paginated(get a range of countries).
There are 5 public methods (endpoints). 4 of them could be used separately and 1 is combined all of the previous in one.

Currently, there are:
GetCountriesFirteredByName - user could search for countries names that contain provided letters from attributes;
GetCountriesFirteredByPopulation - user gets all countries where the population is less than a provided number from attributes in the millions of people;
GetCountriesSortedByName - user could get all countries sorted by provided name;
GetNumberOfCountries - user could get a range of countries by giving a number of countries in the range.

Examples of using:

Get all countries.
List<Country> countries = await API.GetCountries();

Get countries with filtering by name.
List<Country> countries = await API.GetCountries(string name);

Get countries with filtering by name and by population.
List<Country> countries = await API.GetCountries(string name, int population);

Get countries with filtering by name, population, and sorted.
List<Country> countries = await API.GetCountries(string name, int population, string sort);

Get countries with filtering by name, sorted, and paginated.
List<Country> countries = await API.GetCountries(string name, string sort, int numberOfCountries);

Get countries with filtering by name, population, sorted, and paginated.
List<Country> countries = await API.GetCountries(string name, int population, string sort, int numberOfCountries);

Search by country name(Common name).
List<Country> result = await API.GetCountriesSortedByName(string name);

Search by country population.
List<Country> result = await API.GetCountriesFirteredByPopulation(int population);

Sort all countries by name(Common name).
List<Country> result = await API.GetCountriesSortedByName(string name);

Get range of countries.
List<Country> result = await API.GetNumberOfCountries(int numberOfCountries);

Setup

Clone GitHub repository to your local machine. Restore Nuget package. Run.