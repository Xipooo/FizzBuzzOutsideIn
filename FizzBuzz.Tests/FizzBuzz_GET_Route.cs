using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using FizzBuzz.WebApi;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net.Http;

namespace FizzBuzz.Tests
{
    public class FizzBuzz_GET_Route
    {
        private HttpClient client =>
            new TestServer(new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()).CreateClient();

        [Fact]
        public async Task Should_Return_StatusCode_404_When_No_Parameter_Is_Passed_Async()
        {
            //GIVEN the service is running
            //WHEN a GET request is submitted to api/fizzbuzz with no parameter
            var result = await client.GetAsync("/api/fizzbuzz");

            //THEN the response should return a status code 404.
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Should_Return_StatusCode_200_When_Positive_Number_Is_Passed_Async()
        {
            //GIVEN the service is running 
            //WHEN a GET request is submitted to api/fizzbuzz with a positive number parameter 
            var result = await client.GetAsync("/api/fizzbuzz/1");

            //THEN the response should return a status code 200.
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Should_Return_StatusCode_404_When_Multiple_Parameters_Passed_Async()
        {
            //GIVEN the service is running
            //WHEN a GET request is submitted to api/fizzbuzz with multiple numbers for parameters 
            var result = await client.GetAsync("/api/fizzbuzz/1/1");

            //THEN the response should return a status code 404.
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Should_Return_StatusCode_400_When_Passed_Negative_Number_Async()
        {
            //GIVEN the service is running 
            //WHEN a GET request is submitted to api/fizzbuzz with a negative number for a parameter
            var result = await client.GetAsync("/api/fizzbuzz/-1");

            //THEN the response should return a status code 400.
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Should_Return_StatusCode_400_When_Passed_Non_Number_Async()
        {
            //GIVEN the service is running 
            //WHEN a GET request is submitted to api/fizzbuzz with a non-number for a parameter
            var result = await client.GetAsync("/api/fizzbuzz/non-number");

            //THEN the response should return a status code 400
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Should_Return_Error_Message_When_Passed_No_Parameter_Async()
        {
            //GIVEN the service is running
            //WHEN a GET request is submitted to api/fizzbuzz with no parameter
            var response = await client.GetAsync("/api/fizzbuzz");
            var result = await response.Content.ReadAsStringAsync();

            //THEN the response body should contain an error message indicating a parameter is required
            result.Should().Be("Error: A parameter is required.");
        }
    }
}

