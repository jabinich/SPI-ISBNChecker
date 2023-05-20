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

                //Replace multiple consecutive spaces in a string with a single space
                input = Regex.Replace(input, @"\s+", " ");
                
                //Use regular expressions to check the correctness of isbn-10 format,
                //e.g. 7-309-04547-5 or 7-309-04547-X or 7 309 04547 5 or 7309045475 
                bool isbnFormatValidation = (Regex.IsMatch(input, @"^\d+-\d+-\d+-[0-9xX]$") ||
                                       Regex.IsMatch(input, @"^\d+\s\d+\s\d+\s[0-9xX]$")) &&
                                       Regex.Replace(input, @"[\s-]", "").Length == 10;

                if (IsValidISBN(Regex.Replace(input, @"[\s-]", "")) && isbnFormatValidation)
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
}