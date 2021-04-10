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
