using System;
using System.ServiceModel;

namespace StudentCRUD.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "StudentCRUD - WCF Service Host";

            // ServiceHost = WCF სერვისის "გამშვები"
            // typeof(StudentService) = რომელი კლასი არის სერვისი
            using (ServiceHost host = new ServiceHost(typeof(StudentService)))
            {
                try
                {
                    // Open() = სერვისი იწყებს მოსმენას App.config-ში მითითებულ პორტზე
                    host.Open();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("╔══════════════════════════════════════════╗");
                    Console.WriteLine("║   StudentCRUD WCF Service - RUNNING      ║");
                    Console.WriteLine("╠══════════════════════════════════════════╣");
                    Console.WriteLine("║  URL: http://localhost:8080/StudentService║");
                    Console.WriteLine("║  WSDL: ...StudentService?wsdl            ║");
                    Console.WriteLine("╠══════════════════════════════════════════╣");
                    Console.WriteLine("║  Press ENTER to stop the service...      ║");
                    Console.WriteLine("╚══════════════════════════════════════════╝");
                    Console.ResetColor();

                    Console.ReadLine(); // Enter-ის მოლოდინი

                    host.Close();
                    Console.WriteLine("Service stopped.");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: " + ex.Message);
                    Console.ResetColor();
                    Console.ReadLine();
                }
            }
        }
    }
}
