using System;
using System.IO;

namespace EmailAndPassValidationCons
{
    public class Program
    {
        static void Main()
        {
            string projectFolder = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

            string SaveFolder = Path.Combine(projectFolder, "SaveFolder");
            string SaveFile = "SaveFile.txt";
            string FilePath = Path.Combine(SaveFolder, SaveFile);

            if (!Directory.Exists(SaveFolder))
            {
                Directory.CreateDirectory(SaveFolder);
            }

            if (!File.Exists(FilePath))
            {
                try
                {
                    Console.WriteLine("Please, register your account...");
                    Console.Write("Enter your email: ");
                    string Email = Console.ReadLine();
                    Console.Write("Enter your password: ");
                    string Password = Console.ReadLine();

                    File.WriteAllText(FilePath, $"{Email}/{Password}");
                    Console.WriteLine("Registration successful!");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return;
                }
            }

            else
            {
                Console.WriteLine("Existing user found. Please log in.");
            }

            try
            {
                Console.WriteLine("\nLog in to your account:");
                Console.Write("Enter your email: ");
                string CheckEmail = Console.ReadLine();
                Console.Write("Enter your password: ");
                string CheckPassword = Console.ReadLine();

                string SavedCredentials = File.ReadAllText(FilePath);
                string[] Parts = SavedCredentials.Split('/');

                if (Parts.Length == 2 && Parts[0] == CheckEmail && Parts[1] == CheckPassword)
                {
                    Console.WriteLine("Authorization successful.");
                }
                else
                {
                    Console.WriteLine("Invalid credentials.");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
