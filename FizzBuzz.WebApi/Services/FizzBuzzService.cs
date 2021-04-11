namespace FizzBuzz.WebApi.Services
{
    public class FizzBuzzService : IFizzBuzzService
    {
        public string GetAnswer(int number)
        {
            if (number > 0 && number % 5 == 0) { return "FizzBuzz"; }
            if (number >= 0) return number.ToString();
            return "Invalid";
        }
    }
}