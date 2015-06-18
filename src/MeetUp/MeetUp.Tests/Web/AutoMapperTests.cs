using AutoMapper;
using MeetUp.Web;
using NUnit.Framework;

namespace MeetUp.Tests.Web
{
    [TestFixture]
    public class AutoMapperTests
    {
       [Test]
        public void MapperConfigurationIsValid_All()
        {
            // Arrange
            // Act
            AutoMapperConfiguration.Configure();

            // Assert
            Mapper.AssertConfigurationIsValid();
        }  

    }
}
