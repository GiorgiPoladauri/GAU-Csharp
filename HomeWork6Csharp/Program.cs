using System;
using System.Text;

namespace HomeWork6Csharp
{
    public static class GeorgianToLatinExtension
    {
        public static string ToLatin(this string georgianText)
        {
            StringBuilder latinText = new StringBuilder();

            foreach (char c in georgianText)
            {
                switch (c)
                {
                    case 'ა': latinText.Append('a'); break;
                    case 'ბ': latinText.Append('b'); break;
                    case 'გ': latinText.Append('g'); break;
                    case 'დ': latinText.Append('d'); break;
                    case 'ე': latinText.Append('e'); break;
                    case 'ვ': latinText.Append('v'); break;
                    case 'ზ': latinText.Append('z'); break;
                    case 'თ': latinText.Append('t'); break;
                    case 'ი': latinText.Append('i'); break;
                    case 'კ': latinText.Append('k'); break;
                    case 'ლ': latinText.Append('l'); break;
                    case 'მ': latinText.Append('m'); break;
                    case 'ნ': latinText.Append('n'); break;
                    case 'ო': latinText.Append('o'); break;
                    case 'პ': latinText.Append('p'); break;
                    case 'ჟ': latinText.Append('j'); break;
                    case 'რ': latinText.Append('r'); break;
                    case 'ს': latinText.Append('s'); break;
                    case 'ტ': latinText.Append('t'); break;
                    case 'უ': latinText.Append('u'); break;
                    case 'ფ': latinText.Append('f'); break;
                    case 'ქ': latinText.Append('k'); break;
                    case 'ღ': latinText.Append("gh"); break;
                    case 'ყ': latinText.Append('q'); break;
                    case 'შ': latinText.Append("sh"); break;
                    case 'ჩ': latinText.Append("ch"); break;
                    case 'ც': latinText.Append("ts"); break;
                    case 'ძ': latinText.Append("dz"); break;
                    case 'წ': latinText.Append("ts"); break;
                    case 'ჭ': latinText.Append("ch"); break;
                    case 'ხ': latinText.Append("kh"); break;
                    case 'ჯ': latinText.Append('j'); break;
                    case 'ჰ': latinText.Append('h'); break;
                    default: latinText.Append(c); break;
                }
            }

            return latinText.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Input Your Name (Georgian): ");
            string firstName = Console.ReadLine();

            Console.WriteLine("Input Your SurName (Georgian): ");
            string lastName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Please, input both values...");
                return;
            }

            string username = firstName.Substring(0, 1).ToLatin().ToLower() + "." + lastName.ToLatin().ToLower();

            Console.WriteLine("Your username is: " + username);
        }
    }
}
