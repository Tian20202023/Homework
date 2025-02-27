// See https://aka.ms/new-console-template for more information
using System;

namespace MyApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            String num=Console.ReadLine();
            int.TryParse(num, out int n);
            int[] arr = new int[n];
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                String s = Console.ReadLine();
                int.TryParse(s, out arr[i]);
                sum += arr[i];
            }
            Array.Sort(arr);
            Console.WriteLine("最大值： "+arr[n-1]);
            Console.WriteLine("最小值： "+arr[0]);
            Console.WriteLine("和： "+sum);
            Console.WriteLine("平均值： "+sum/n);
        }
    }
}


