using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int number = int.Parse(Console.ReadLine());
        List<int> primeFactors = FindPrimeFactors(number);
        foreach (var factor in primeFactors)
        {
            Console.WriteLine(factor);
        }
    } 
    static List<int> FindPrimeFactors(int n)
    {
        List<int> factors = new List<int>();
        for (int factor = 2; n > 1; factor++)
        {
            while (n % factor == 0)
            {
                factors.Add(factor);
                n /= factor;
            }
        }
        return factors;
    }
}