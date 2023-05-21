using System.Text.RegularExpressions;

namespace SPI_ISBNChecker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continueValidation = true;

            while (continueValidation)
            {
                Console.WriteLine("Bitte geben Sie eine 10-stellige ISBN ein:");

                string input = Console.ReadLine() ?? "".Trim();

                ISBNChecker checker = new ISBNChecker();
                bool isValid = checker.CheckISBN(input);

                if (isValid)
                {
                    Console.WriteLine("Die eingegebene ISBN ist gueltig.");
                }
                else
                {
                    Console.WriteLine("Die eingegebene ISBN ist ungueltig.");
                }

                Console.WriteLine("Moechten Sie eine weitere ISBN ueberpruefen? (J/N)");
                string answer = Console.ReadLine() ?? "".Trim();

                continueValidation = (answer == "J" || answer == "j");

            }

            Console.WriteLine("Das Programm wird beendet.\n" +
                "Druecken Sie eine beliebige Taste, um dieses Fenster zu schliessen");
            Console.ReadKey();

        }

        static bool IsValidISBN(string input)
        {
            int checksum = 0;
            for (int i = 0; i < 9; i++)
            {
                checksum += (input[i] - '0') * (10 - i);
            }
            //check if the last character is number.
            //If not number (it's X) then convert it to 10 
            int lastDigit = !Char.IsDigit(input[9]) ? 10 : input[9] - '0';
            checksum += lastDigit;

            return checksum % 11 == 0;
        }

    }

    public class ISBNChecker {

        private readonly Regex isbnFormatRegex;

        public ISBNChecker()
        {
            //Use regular expressions to check the correctness of isbn-10 format,
            //e.g. 7-309-04547-5 or 7-309-04547-X or 7 309 04547 5 or 7309045475 
            isbnFormatRegex = new Regex(@"^\d+-\d+-\d+-[0-9xX]$ || 
                                        ^\d+\s\d+\s\d+\s[0-9xX]$ || 
                                        ^[0-9]{9}[0-9xX]$");
        }

        private string CleanInput(string input)
        {
            //Replace multiple consecutive spaces in a string with a single space
            string cleanedInput = Regex.Replace(input, @"\s+", " ");
            return cleanedInput;
        }

        public bool CheckISBN(string input)
        {
            string cleanedInput = CleanInput(input);
            bool isFormatValid = CheckFormatISBN(cleanedInput);

            return isFormatValid && CheckValidityISBN(cleanedInput);
        }

        public bool CheckFormatISBN(string input)
        {
            string cleanedInput = CleanInput(input);
            bool isFormatValid = isbnFormatRegex.IsMatch(cleanedInput) &&
                                    Regex.Replace(cleanedInput, @"[\s-]", "").Length == 10;

            bool a = isbnFormatRegex.IsMatch(cleanedInput);
            bool b = Regex.Replace(cleanedInput, @"[\s-]", "").Length == 10;

            return isFormatValid;
        }

        private bool CheckValidityISBN(string input)
        {
            int checksum = 0;
            for (int i = 0; i < 9; i++)
            {
                checksum += (input[i] - '0') * (10 - i);
            }
            //check if the last character is number.
            //If not number (it's X) then convert it to 10 
            int lastDigit = !Char.IsDigit(input[9]) ? 10 : input[9] - '0';
            checksum += lastDigit;

            return checksum % 11 == 0;
        }
    }
}