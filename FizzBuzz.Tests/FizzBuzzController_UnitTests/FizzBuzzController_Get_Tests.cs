using Xunit;
using FizzBuzz.WebApi.Controllers;
using Moq;
using FizzBuzz.WebApi.Services;

namespace FizzBuzz.Tests.FizzBuzzController_UnitTests
{
    public class FizzBuzzController_Get_Tests
    {
        [Fact]
        public void Should_Call_IFizzBuzzService_GetAnswer_When_Called_With_Parameter()
        {
            //Given
            var fizzBuzzServiceMock = new Mock<IFizzBuzzService>();
            fizzBuzzServiceMock.Setup(service => service.GetAnswer(-1)).Returns("Invalid");
            var SUT = new FizzBuzzController(fizzBuzzServiceMock.Object);

            //When
            var result = SUT.Get(-1);

            //Then
            fizzBuzzServiceMock.Verify(service => service.GetAnswer(-1));
        }
    }
}