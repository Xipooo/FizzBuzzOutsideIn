using Xunit;
using FizzBuzz.WebApi.Services;
using FluentAssertions;

namespace FizzBuzz.Tests.FizzBuzzService_UnitTests
{
    public class FizzBuzzService_GetAnswer_Tests
    {
        private readonly FizzBuzzService SUT = new FizzBuzzService();
        [Fact]
        public void Should_Return_Invalid_When_Parameter_Is_Negative_Value()
        {
            //Given

            //When
            var result = SUT.GetAnswer(-1);

            //Then
            result.Should().Be("Invalid");
        }

        [Fact]
        public void Should_Return_0_When_Parameter_Is_0()
        {
            //Given

            //When
            var result = SUT.GetAnswer(0);

            //Then
            result.Should().Be("0");
        }

        [Fact]
        public void Should_Return_1_When_Parameter_Is_1()
        {
            //Given

            //When
            var result = SUT.GetAnswer(1);

            //Then
            result.Should().Be("1");
        }

        [Theory]
        [InlineData(15)]
        [InlineData(30)]
        [InlineData(45)]
        [InlineData(60)]
        public void Should_Return_FizzBuzz_When_Parameter_Is_Divisible_By_3_And_5(int value)
        {
            //Given
            
            //When
            var result = SUT.GetAnswer(value);

            //Then
            result.Should().Be("FizzBuzz");
        }
    }
}