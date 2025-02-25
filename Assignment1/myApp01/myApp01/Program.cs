using System;

namespace Test001
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = Convert.ToDouble(Console.ReadLine());
            double b = Convert.ToDouble(Console.ReadLine());
            char c = Convert.ToChar(Console.ReadLine());
            if (c == '+')
            {
                Console.WriteLine(a + b);
            }
            else if (c == '-')
            {
                Console.WriteLine(a - b);
            }
            else if (c == '*')
            {
                Console.WriteLine(a * b);
            }
            else
            {
                Console.WriteLine(a / b);
            }
        }
    }
}
