// See https://aka.ms/new-console-template for more information

using System;
using System.Collections;
class Program
{
    static void Main(String[] args)
    {
        ArrayList list = new ArrayList();
        int cnt = 2;
        for (int i = 2; i < 100; i++)
        {
            list.Add(i);
        }
        for (int j = 2; j< 100; j++)
        {
            if (list.Contains(j))
            {
                for (int k = 2; k * j < 100; k++)
                {
                    int x = j * k;
                    if (list.Contains(x))
                    {
                        list.Remove(x);
                    }
                }
            }
        }

        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }
}
