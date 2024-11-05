using System;

class Book
{
    // Properties
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }

    // Constructor
    public Book(string title, string author, decimal price)
    {
        Title = title;
        Author = author;
        Price = price;
    }

    // Method to display book info
    public void DisplayInfo()
    {
        Console.WriteLine($"Title: {Title}, Author: {Author}, Price: {Price}");
    }
}

class MathHelper
{
    // Static method to find the maximum value in an array
    public static int FindMax(int[] array)
    {
        int max = array[0];
        foreach (int num in array)
        {
            if (num > max)
            {
                max = num;
            }
        }
        return max;
    }
}

class Student
{
    // Properties
    public string Name { get; set; }
    public int[] Marks { get; set; }

    // Method to calculate the average mark
    public double CalculateAverage()
    {
        int sum = 0;
        foreach (int mark in Marks)
        {
            sum += mark;
        }
        return (double)sum / Marks.Length;
    }
}

class Program
{
    static void Main()
    {
        // Task 1: Book class and DisplayInfo method
        Console.WriteLine("Task 1: Book class and DisplayInfo method\n");
        Book book = new Book("C# in Depth", "Jon Skeet", 29.99m);
        book.DisplayInfo();
        Console.WriteLine();

        // Task 2: Calculating average temperature
        Console.WriteLine("Task 2: Calculating average temperature\n");
        int[] temperatures = { 20, 22, 21, 19, 24, 23, 18 };
        int sum = 0;
        for (int i = 0; i < temperatures.Length; i++)
        {
            sum += temperatures[i];
        }
        double averageTemperature = (double)sum / temperatures.Length;
        Console.WriteLine($"The average temperature for the week is: {averageTemperature}°C\n");

        // Task 3: Age validation
        Console.WriteLine("Task 3: Age validation\n");
        Console.Write("Enter your age: ");
        int age = int.Parse(Console.ReadLine());
        if (age > 0)
        {
            Console.WriteLine("Welcome!");
        }
        else
        {
            Console.WriteLine("Invalid age entered.");
        }
        Console.WriteLine();

        // Task 4: MathHelper static method to find max value in an array
        Console.WriteLine("Task 4: MathHelper static method\n");
        int[] numbers = { 15, 32, 7, 22, 9 };
        int max = MathHelper.FindMax(numbers);
        Console.WriteLine($"The largest number is: {max}\n");

        // Task 5: Student class and CalculateAverage method
        Console.WriteLine("Task 5: Student class and CalculateAverage method\n");
        Student student = new Student
        {
            Name = "Ana",
            Marks = new int[] { 80, 90, 85 }
        };
        double averageMarks = student.CalculateAverage();
        Console.WriteLine($"{student.Name}'s average marks: {averageMarks}\n");
    }
}
