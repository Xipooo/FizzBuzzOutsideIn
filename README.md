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
    dotnet add package Microsoft.AspNetCore.Mvc.Testing
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

* As Developer Dave I want to submit a number to the api and receive a fizzbuzz answer in the body of the response so that I can program my application to display the response to my user.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with no parameter THEN the response should return a status code 404.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with a positive number parameter THEN the response should return a status code 200.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with multiple numbers for parameters THEN the response should return a status code 404.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with a negative number for a parameter THEN the response should return a status code 400.
  * GIVEN the service is running WHEN a GET request is submitted to api/fizzbuzz with a non-number for a parameter THEN the response should return a status code 400.
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

* Rename `UnitTest1.Test1Async()` to `FizzBuzz_GET_Route.Should_Return_StatusCode_404_When_No_Parameter_Is_Passed_Async()`
* Rename `UnitTest1.cs` to `FizzBuzz_GET_Route`
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
    public class FizzBuzzController : ControllerBase
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

## Phase 5

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

    //WHEN a GET request is submitted to api/fizzbuzz with multiple numbers for parameters 
    var result = await client.GetAsync("/api/fizzbuzz/1/1");
    
    //THEN the response should return a status code 404.
    result.StatusCode.Should().Be(404);
}
```

* It will pass

### Refactor 

* Rename test to `Should_Return_StatusCode_404_When_Multiple_Parameters_Passed_Async()`
* Refactor `FizzBuzz_GET_Route` with the following private field:

```c#
private HttpClient client => 
    new TestServer(new WebHostBuilder()
        .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location)).UseStartup<Startup>()).CreateClient();
```

* Remove setup code from the tests
* Rerun tests to validate green

## Phase 6

* Test code:

```c#
[Fact]
public async Task Should_Return_StatusCode_400_When_Passed_Negative_Number_Async()
{
    //GIVEN the service is running 
    //WHEN a GET request is submitted to api/fizzbuzz with a negative number for a parameter
    var result = await client.GetAsync("/api/fizzbuzz/-1");

    //THEN the response should return a status code 400.
    result.StatusCode.Should().Be(400);
}
```

* Controller code:

```c#
[HttpGet("{number}")]
public IActionResult Get(int number){
    if (number < 0) return BadRequest();
    return Ok();
}
```

### Refactor

* Add `FizzBuzzController_Get_Tests/FizzBuzzController_Get_Tests.cs` to tests project with the following code:

```c#
using Xunit;
using FizzBuzz.WebApi.Controllers;
using Moq;
using FizzBuzz.WebApi.Services;

namespace FizzBuzz.Tests.FizzBuzzController_CollaboratorTests
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
```

* Refactor the `FizzBuzzController.Get()` method to the following:

```c#
public IActionResult Get(int number)
{
    var result = _fizzBuzzService.GetAnswer(number);
    if (result == "Invalid") return BadRequest();
    return Ok();
}
```

* Add `FizzBuzzService_UnitTests/FizzBuzzService_GetAnswer_Tests.cs` to tests project with the following code:

```c#
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
        public void Should_Return_1_When_Parameter_Is_1()
        {
            //Given
            
            //When
            var result = SUT.GetAnswer(1);
            
            //Then
            result.Should().Be("1");
        }
    }
}
```

* Add `Services/FizzBuzzService.cs` to WebApi project with the following code:

```c#
namespace FizzBuzz.WebApi.Services
{
    public class FizzBuzzService : IFizzBuzzService
    {
        public string GetAnswer(int number)
        {
            if (number >= 0) return number.ToString();
            return "Invalid";
        }
    }
}
```

* Run all tests to validate passing

## Phase 7

### Red

* Add the following tests to `FizzBuzz_GET_Route`:

```c#
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
public async Task TestNameAsync()
{
    //GIVEN the service is running
    //WHEN a GET request is submitted to api/fizzbuzz with no parameter
    var response = await client.GetAsync("/api/fizzbuzz");
    var result = await response.Content.ReadAsStringAsync();

    //THEN the response body should contain an error message indicating a parameter is required
    result.Should().Be("Error: A parameter is required.");
}
```

### Green

* Add the following code to the `FizzBuzzController`;

```c#
[HttpGet]
public IActionResult Get(){
    return NotFound("Error: A parameter is required.");
}
```

### Refactor

* Rename `TestNameAsync` to `Should_Return_Error_Message_When_Passed_No_Parameter_Async`
* Rerun tests

## Phase 8

### Red

* Add the following scenario to the `FizzBuzz_GET_Route` class:

```c#
[Theory]
[InlineData(15)]
[InlineData(30)]
[InlineData(45)]
[InlineData(60)]
public async Task TestNameAsync(int value)
{
    //GIVEN the service is running 
    //WHEN a GET request is submitted to api/fizzbuzz with a positive number parameter that is a multiple of 3 and 5 
    var response = await client.GetAsync($"/api/fizzbuzz/{value}");
    var result = await response.Content.ReadAsStringAsync();

    //THEN the response body should return fizzbuzz in the body of the response.
    result.Should().Be("FizzBuzz");
}
```

* Add the following test to the `FizzBuzzService_GetAnswer_Tests` class:

```c#
[Theory]
[InlineData(15)]
[InlineData(30)]
[InlineData(45)]
[InlineData(60)]
public void TestName(int value)
{
    //Given
    
    //When
    var result = SUT.GetAnswer(value);
    //Then
    result.Should().Be("FizzBuzz");
}
```

* Run failing tests

### Green

* Change `FizzBuzzService.GetAnswer()` to the following code:
  
```c#
public string GetAnswer(int number)
{
    if (number > 0 && number % 5 == 0) { return "FizzBuzz"; }
    if (number >= 0) return number.ToString();
    return "Invalid";
}
```

* Change `FizzBuzzController.Get()` to the following code:

```c#
[HttpGet("{number}")]
public IActionResult Get(int number)
{
    var result = _fizzBuzzService.GetAnswer(number);
    if (result == "Invalid") return BadRequest();
    return Ok(result);
}
```

### Refactor

* Rename `FizzBuzzService_GetAnswer_Tests.TestName` to `Should_Return_FizzBuzz_When_Parameter_Is_Divisible_By_3_And_5`
* Rename `FizzBuzz_GET_Route.TestNameAsync` to `Should_Return_FizzBuzz_When_Parameter_Is_Divisible_By_3_And_5_Async`

## Phase 9

### Red

* Add the following test to `FizzBuzz_GET_Route`:

```c#
[Theory]
[InlineData(3)]
[InlineData(6)]
[InlineData(9)]
[InlineData(12)]
public async Task TestNameAsync(int value)
{
    //GIVEN the service is running 
    //WHEN a GET request is submitted to api/fizzbuzz with a positive number parameter that is a multiple of just 3
    var response = await client.GetAsync($"/api/fizzbuzz/{value}");
    var result = await response.Content.ReadAsStringAsync();
    
    //THEN the response body should return fizz in the body of the response.
    result.Should().Be("Fizz");
}
```

* Add the following test to `FizzBuzzService_GetAnswer_Tests`:

```c#
[Theory]
[InlineData(3)]
[InlineData(6)]
[InlineData(9)]
[InlineData(12)]
public void TestName(int value)
{
    //Given
    //When
    var result = SUT.GetAnswer(value);
    //Then
    result.Should().Be("Fizz");
}
```

### Green

* Change `FizzBuzzService.GetAnswer()` to the following code:

```c#
public string GetAnswer(int number)
{
  if (number > 0 && number % 5 == 0) { return "FizzBuzz"; }
  if (number > 0 && number % 3 == 0) { return "Fizz"; }
  if (number >= 0) return number.ToString();
  return "Invalid";
}
```

### Refactor

* Change `FizzBuzzService.GetAnswer()` to the following code:

```c#
public string GetAnswer(int number)
{
    if (number >= 0) {
        if (number % 5 == 0) return "FizzBuzz";
        if (number % 3 == 0) return "Fizz";
        return number.ToString();
    }
    return "Invalid";
}
```

* Rename `FizzBuzzService_GetAnswer_Tests.TestName` to `Should_Return_Fizz_When_Passed_Value_Divisible_By_Only_3`
* Rename `FizzBuzz_GET_Route.TestNameAsync` to `Should_Return_Fizz_When_Passed_Value_Divisible_By_Only_3_Async`
* Rerun tests
