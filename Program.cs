using System;
using System.Globalization;

namespace Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome in Maxim Calculator, User!\n");
            Console.WriteLine("Rules of enter input operation:\n" +
                              "1. This is math operation.\n" +
                              "2. You can enter only two numbers and one operation(+, -, /, *, %) by itself.\n" +
                              "3. All through spaces.\n" +
                              "4. You can enter numbers with float point like 7.57\n" +
                              "So your operation must look like 4 + 9.");
            do
            {
                string[] tokens;
                
                Input(out tokens);

                double number1 = double.Parse(tokens[0], CultureInfo.InvariantCulture);
                double number2 = double.Parse(tokens[2], CultureInfo.InvariantCulture);

                PrintResult(number1, number2, tokens);

            }
            while (RequestsToUser.IsUserAgreeWith("Want do one more operation?\n"));
        }
        static void Input(out string[] tokens)
        {
            char[] signVariants = {'+', '-', '*', '/', '%'};
            for(; ; )
            {
                Console.Write("Enter operation: ");
                string input = Console.ReadLine();
                tokens = input.Split();
                try
                {
                    double number1 = double.Parse(tokens[0], CultureInfo.InvariantCulture);
                    double number2 = double.Parse(tokens[2], CultureInfo.InvariantCulture);
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }
                if (!char.TryParse(tokens[1], out char sign) ||
                     tokens.Length > 3 || 
                    !signVariants.Contains(sign))
                {
                    Console.WriteLine("Invalid operation");
                    continue;
                }
                break;
            }
        }
        static void PrintResult(double number1, double number2, string[] tokens)
        {
            double result = tokens[1] switch
            {
                "+" => number1 + number2,
                "-" => number1 - number2,
                "*" => number1 * number2,
                "/" => number1 / number2,
                "%" => number1 % number2,
                _ => throw new Exception("invalid input")
            };
            Console.WriteLine($"{tokens[0]} {tokens[1]} {tokens[2]} = {result}");
        }
    }
    internal static class RequestsToUser
    {
        private static readonly string[] _approvingResponses = { "yes", "y", "true", "yep", "yeah", "" };
        private static readonly string[] _disapprovingResponses = { "no", "n", "don't" };
        public static bool IsUserAgreeWith(string withWhat)
        {
            Console.Write(withWhat);
            for ( ; ; )
            {
                string response = Console.ReadLine();

                if (_approvingResponses.Contains(response))
                {
                    return true;
                }
                else if (_disapprovingResponses.Contains(response))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again");
                    continue;
                }
            }
        }
    }
}