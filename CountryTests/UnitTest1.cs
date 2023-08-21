using UC_1.Models;
using  UC_1.Controllers;

namespace CountryTests
{
    public class CountryTests
    {
        private CountryController Controller { get; set; }
        public CountryTests()
        {
            Controller = new CountryController();
        }

        [Fact]
        public void GetCountriesFirteredByName_Should_Return_Correct_Info()
        {
            //Arrange
            var filter = "a";
            var france = "France";
            var spain = "Spain";
            var germany = "Germany";

            //Act
            var result = Controller.GetCountriesFirteredByName(filter);

            //Assert
            var countiesName = result.Select(x => x.Name.Common).ToList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains(france, countiesName);
            Assert.Contains(spain, countiesName);
            Assert.Contains(germany, countiesName);
        }

        [Fact]
        public void GetCountriesFirteredByName_Should_Return_Emty_List()
        {
            //Arrange
            var filter = "aaaa";

            //Act
            var result = Controller.GetCountriesFirteredByName(filter);

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetCountriesFirteredByPopulation_Should_Return_Correct_Data()
        {
            //Arrange
            var filter = 3;

            //Act
            var result = Controller.GetCountriesFirteredByPopulation(filter);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count != 0);
        }

        [Fact]
        public void GetCountriesFirteredByPopulation_Should_Return_Empty_List()
        {
            //Arrange
            var filter = 0;

            //Act
            var result = Controller.GetCountriesFirteredByPopulation(filter);

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void GetCountriesSortedByName_Should_Return_Correct_Ordered_Data()
        {
            //Arrange
            var order = "ascend";
            var firstLetter = "a";

            //Act
            var result = Controller.GetCountriesSortedByName(order);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count > 0);
            Assert.True(result.First().Name.Common.StartsWith(firstLetter, StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void GetCountriesSortedByName_Should_Return_Empty_List()
        {
            //Arrange
            var order = "ffdds";

            //Act
            var result = Controller.GetCountriesSortedByName(order);

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetNumberOfCountries_Should_Return_Correct_Data()
        {
            //Arrange
            var filter = 10;

            //Act
            var result = Controller.GetNumberOfCountries(filter);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count == 10);
        }

        [Fact]
        public void GetNumberOfCountries_Should_Return_Empty_List()
        {
            //Arrange
            var filter = 0;

            //Act
            var result = Controller.GetNumberOfCountries(filter);

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}