// See https://aka.ms/new-console-template for more information
using System;
using System.Reflection.Metadata.Ecma335;

class Program
{
    static void Main(string[] args)
    {
        String s = Console.ReadLine();
        int.TryParse(s, out int m);
        String s1=Console.ReadLine();
        int.TryParse(s1, out int n);
        int[][] arr = new int[m][];
        for (int i = 0; i < m; i++)
        {
            arr[i] = new int[n];
            for (int j = 0; j < n; j++)
            {
                int x = int.Parse(Console.ReadLine());
                arr[i][j] = x;
            }
            
        }

        int a = 1, b = 1;
        int cnt = arr[0][0];
        while (a < m)
        {
            if (arr[a][b] == cnt)
            {
                a++;
                b++;
                continue;
            }
            else
            {
                Console.WriteLine("False");
                return;
            }
        }
        Console.WriteLine("True");
    }
}