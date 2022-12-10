using Xunit;

namespace Business.Tests
{
    public class AutomapperTest : TestBase
    {
        [Fact]
        public void ValidateConfiguration()
        {
            // Act
            var configuration = UnitTestHelper.CreateMapperConfiguration();

            // Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}