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

