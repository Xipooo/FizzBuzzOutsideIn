namespace FizzBuzz.WebApi.Services
{
    public class FizzBuzzService : IFizzBuzzService
    {
        public string GetAnswer(int number)
        {
            if (number >= 0) {
                if (number % 5 == 0) return "FizzBuzz";
                if (number % 3 == 0) return "Fizz";
                return number.ToString();
            }
            return "Invalid";
        }
    }
}