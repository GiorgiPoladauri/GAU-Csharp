namespace ClassWorkCsharp
{
    class Task1
    {
        static string[] BookList = new string[5];
        static void Main()
        {
            Function1();
        }
        public static void Function1()
        {
            Console.WriteLine("Hello, please tell us your name : ");
            string UserName = Console.ReadLine();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{UserName}, tell us your favourite number {i + 1} book title : ");
                string BookTitle = Console.ReadLine();
                BookList[i] = BookTitle;
                Thread.Sleep(2000);
            }
            Console.WriteLine($"{UserName}, your favourite books are: {string.Join(", ",BookList)}");
            Thread.Sleep(2000);
            Function2();
        }
        public static void Function2()
        {
            Console.WriteLine("Type in book title to search it in your top 5 list : ");
            string SearchedTitle = Console.ReadLine();
            bool found = false;
            foreach (string book in BookList)
            {
                if (book == SearchedTitle)
                {
                    found = true;
                    break;
                }
            }
            if (found)
            {
                Console.WriteLine($"Congrats, there is a book named '{SearchedTitle}' in your top 5 list");
            }
            else
            {
                Console.WriteLine($"There is not any title named '{SearchedTitle}' in your top 5 list...");
            }
        }
    }
}
