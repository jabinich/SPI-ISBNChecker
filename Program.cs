using System.Text.RegularExpressions;

namespace SPI_ISBNChecker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continueValidation = true;
            ISBNChecker checker = new ISBNChecker();

            while (continueValidation)
            {
                Console.WriteLine("Bitte geben Sie eine ISBN ein:");

                string input = Console.ReadLine() ?? "";
                checker.CheckISBN(input);
                
                if (checker.IsFormatValid) {
                    Console.WriteLine("Die eingegebene {0} ist {1}.", checker.ISBNFormat, checker.IsISBNValid ? "gueltig" : "ungueltig");
                }
                else
                {
                    Console.WriteLine("Ungueltiges Format.");
                }

                Console.WriteLine("Moechten Sie eine weitere ISBN ueberpruefen? (J/N)");
                string answer = Console.ReadLine() ?? "".Trim();

                continueValidation = (answer == "J" || answer == "j");

            }

            Console.WriteLine("Das Programm wird beendet.\n" +
                "Druecken Sie eine beliebige Taste, um dieses Fenster zu schliessen");
            Console.ReadKey();

        }

    }

    public class ISBNChecker {

        private readonly Regex isbn10FormatRegex;
        private readonly Regex isbn13FormatRegex;

        public bool IsFormatValid { get; private set; }
        public bool IsISBNValid { get; private set; }
        public string ISBNFormat { get; private set; }

        public ISBNChecker()
        {
            //Use regular expressions to check the correctness of the format,
            //for ISBN-10:
            //e.g. 7-309-04547-5,7-309-04547-X using \d+\-\d+\-\d+\-[0-9xX]
            //or 7 309 04547 5 using \d+\ \d+\ \d+\ [0-9xX]
            //or 7309045475 using [0-9]{9}[0-9xX] 
            //And extend the expression above with 'positive Lookahead' to restrict the length:
            //using (?=(?:\D*\d){9}\D*[0-9xX]$)
            
            isbn10FormatRegex = new Regex(@"^(?=(?:\D*\d){9}\D*[0-9xX]$)(\d+\-\d+\-\d+\-[0-9xX]|\d+\ \d+\ \d+\ [0-9xX]|[0-9]{9}[0-9xX])$");

            //for ISBN-13:
            //e.g. 978-986-181-728-6 using \d{3}\-\d+\-\d+\-\d+\-[0-9]
            //or 978 986 181 728 6 using \d{3}\ \d+\ \d+\ \d+\ [0-9]
            //or 9789861817286 using [0-9]{13}
            //And extend the expression above with 'positive Lookahead' to restrict the length:
            //using (?=(?:\D*\d){13}\D*$)

            isbn13FormatRegex = new Regex(@"^(?=(?:\D*\d){13}\D*$)(\d{3}\-\d+\-\d+\-\d+\-[0-9]|\d{3}\ \d+\ \d+\ \d+\ [0-9]|[0-9]{13})$");

            ISBNFormat = string.Empty;
        }

        public void CheckISBN(string input)
        {
            string cleanedInput = CleanInput(input);

            if (CheckFormatISBN10(cleanedInput))
            {
                IsFormatValid = true;
                ISBNFormat = "ISBN-10";
                IsISBNValid = CheckValidityISBN10(cleanedInput);
            }
            else if (CheckFormatISBN13(cleanedInput))
            {
                IsFormatValid = true;
                ISBNFormat = "ISBN-13";
                IsISBNValid = CheckValidityISBN13(cleanedInput);
            }
            else {
                IsFormatValid = false;
                ISBNFormat = "";
                IsISBNValid = false;
            }
        }

        private string CleanInput(string input)
        {
            //Replace multiple consecutive spaces in a string with a single space
            string cleanedInput = Regex.Replace(input, @"\s+", " ").Trim();
            return cleanedInput;
        }

        private bool CheckFormatISBN10(string input)
        {
            return isbn10FormatRegex.IsMatch(input);
        }

        private bool CheckFormatISBN13(string input)
        {
            return isbn13FormatRegex.IsMatch(input);
        }

        private bool CheckValidityISBN10(string input)
        {
            input = Regex.Replace(input, @"[\s-]", "");
            int checksum = 0;
            for (int i = 0; i < 9; i++)
            {
                checksum += int.Parse(input[i].ToString()) * (i + 1);
            }
            //check if the last character is number.
            //If not number (it's X) then convert it to 10 
            int lastDigit = !Char.IsDigit(input[9]) ? 10 : int.Parse(input[9].ToString());
            checksum -= lastDigit;

            return checksum % 11 == 0;
        }

        private bool CheckValidityISBN13(string input)
        {
            input = Regex.Replace(input, @"[\s-]", "");
            int checksum = 0;
            for (int i = 0; i < 12; i++)
            {
                checksum += (i % 2 == 0) ? int.Parse(input[i].ToString()) : int.Parse(input[i].ToString()) * 3;
            }
            int lastDigit = int.Parse(input[12].ToString());
            checksum += lastDigit;

            return checksum % 10 == 0;
        }
    }
}