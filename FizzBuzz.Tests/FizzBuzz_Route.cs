using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using FizzBuzz.WebApi;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using FluentAssertions;

namespace FizzBuzz.Tests
{
    public class FizzBuzz_Route
    {
        [Fact]
        public async Task Should_Return_StatusCode_404_When_No_Parameter_Is_Passed_Async()
        {
            //GIVEN the service is running
            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>();
            var testServer = new TestServer(hostBuilder);
            var client = testServer.CreateClient();

            //WHEN a GET request is submitted to api/fizzbuzz with no parameter
            var result = await client.GetAsync("/api/fizzbuzz");

            //THEN the response should return a status code 404.
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Should_Return_StatusCode_200_When_Positive_Number_Is_Passed_Async()
        {
            //GIVEN the service is running 
            var hostBuilder = new WebHostBuilder()
                    .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>();
            var testServer = new TestServer(hostBuilder);
            var client = testServer.CreateClient();

            //WHEN a GET request is submitted to api/fizzbuzz with a positive number parameter 
            var result = await client.GetAsync("/api/fizzbuzz/1");

            //THEN the response should return a status code 200.
            result.StatusCode.Should().Be(200);
        }
    }
}
