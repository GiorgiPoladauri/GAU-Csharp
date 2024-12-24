using System;

namespace DelegatesExample
{
    class Program
    {
        // Delegate declaration
        public delegate int MathOperation(int x, int y);

        static void Main(string[] args)
        {
            // Assign methods to the delegate
            MathOperation add = Add;
            MathOperation subtract = Subtract;
            MathOperation multiply = Multiply;
            MathOperation divide = Divide;

            // Use the delegate with different operations
            Console.WriteLine("Addition: " + add(10, 5));
            Console.WriteLine("Subtraction: " + subtract(10, 5));
            Console.WriteLine("Multiplication: " + multiply(10, 5));
            Console.WriteLine("Division: " + divide(10, 5));

            // Demonstrating delegate as a method parameter
            ExecuteOperation(15, 3, add);
            ExecuteOperation(15, 3, subtract);
            ExecuteOperation(15, 3, multiply);
            ExecuteOperation(15, 3, divide);

            // Using anonymous methods
            MathOperation mod = delegate (int x, int y) { return x % y; };
            Console.WriteLine("Modulus: " + mod(10, 3));

            // Using lambda expressions
            MathOperation power = (x, y) => (int)Math.Pow(x, y);
            Console.WriteLine("Power: " + power(2, 3));
        }

        static int Add(int x, int y)
        {
            return x + y;
        }

        static int Subtract(int x, int y)
        {
            return x - y;
        }

        static int Multiply(int x, int y)
        {
            return x * y;
        }

        static int Divide(int x, int y)
        {
            if (y == 0) throw new DivideByZeroException("Cannot divide by zero.");
            return x / y;
        }

        static void ExecuteOperation(int x, int y, MathOperation operation)
        {
            Console.WriteLine("Result of operation: " + operation(x, y));
        }
    }
}
