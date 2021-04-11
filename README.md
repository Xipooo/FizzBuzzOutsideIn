# Steps to Follow

## Phase 1

* Terminal:
  
    ```bash
    dotnet new sln -o FizzBuzzOutsideIn
    cd FizzBuzzOutsideIn
    dotnet new webapi -o FizzBuzz.WebApi
    dotnet new xunit -o FizzBuzz.Tests
    dotnet sln add ./FizzBuzz.WebApi/FizzBuzz.WebApi.csproj
    dotnet sln add ./FizzBuzz.Tests/FizzBuzz.Tests.csproj
    dotnet add ./FizzBuzz.Tests/FizzBuzz.Tests.csproj reference ./FizzBuzz.WebApi/FizzBuzz.WebApi.csproj
    cd FizzBuzz.Tests
    dotnet add package Microsoft.AspNetCore.TestHost
    dotnet add package FluentAssertions
    ```

* Remove `WeatherForecastController.cs` & `WeatherForecast.cs`
* Run tests : Green

## Phase 2

Use Case Workflow:

* Use Case: Submit number at api/fizzbuzz.
  * Submit GET api/fizzbuzz/n
  * Return fizzbuzz answer

User Stories and Acceptance Criteria:

* As Developer Dave I want to submit a number to the api so that I can program my application to perform an AJAX request at the endpoint.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with no parameter THEN the response should return a status code 404.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with a positive number parameter THEN the response should return a status code 200.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with multiple numbers for parameters THEN the response should return a status code 404.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with a negative number for a parameter THEN the response should return a status code 400.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with a non-number for a parameter THEN the response should return a status code 400.
* As Developer Dave I want to receive a fizzbuzz answer in the body of the response so that I can program my application to display the response to my user.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with no parameter THEN the response body should contain an error message indicating a parameter is required.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with a positive number parameter that is a multiple of 3 and 5 THEN the response body should return fizzbuzz in the body of the response.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with a positive number parameter that is a multiple of just 3 THEN the response body should return fizz in the body of the response.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with a positive number parameter that is a multiple of just 5 THEN the response body should return buzz in the body of the response.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with a positive number parameter that is not a multiple of 5 or 3 THEN the response body should return the number in the body of the response.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with multiple numbers for parameters THEN the response body should contain an error message indicating there were too many parameters
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with a negative number for the parameter THEN the response body should contain an error message indicating the value is invalid
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with a non-number for a parameter THEN the response body should contain an error message indicating the value is invalid.

## Phase 3

### Red/Green

* Add the following code to the `UnitTest1.Test1Async()` method:

  ```c#
  namespace FizzBuzz.Tests
  {
      public class UnitTest1
      {
          [Fact]
          public async Task Test1Async()
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
      }
  }
  ```

* Run test - green

### Refactor

* Rename `UnitTest1.Test1Async()` to `FizzBuzz_Route.Should_Return_StatusCode_404_When_No_Parameter_Is_Passed_Async()`
* Rename `UnitTest1.cs` to `FizzBuzz_Route`
* Change assertion to check for 200 to show test failure message

## Phase 4

### Red

* Add the following test:

  ```c#
  [Fact]
  public async Task TestNameAsync()
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
  ```

* Run test to show red

### Green

* Create `FizzBuzzController.cs` and add the following code:

```c#
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzz.WebApi.Controllers
{
    [ApiController]
    [Route("api/fizzbuzz")]
    public class FizzBuzz : ControllerBase
    {
        [HttpGet("{number}")]
        public IActionResult Get(int number){
            return Ok();
        }
    }
}
```

* Run tests to validate it passes

### Refactor

* Rename `TestNameAsync()` to `Should_Return_StatusCode_200_When_Positive_Number_Is_Passed_Async()`
