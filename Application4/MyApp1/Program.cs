// See https://aka.ms/new-console-template for more information
using System;

namespace MyApp01
{

    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class GenericLinkedList<T>
    {
        private Node<T> head;

        public void Add(T data)
        {
            if (head == null)
            {
                head = new Node<T>(data);
            }
            else
            {
                Node<T> current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = new Node<T>(data);
            }
        }

        public void ForEach(Action<T> action)
        {
            Node<T> current = head;
            while (current != null)
            {
                action(current.Data);
                current = current.Next;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            GenericLinkedList<int> list = new GenericLinkedList<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);
            list.Add(40);
            list.Add(50);

            // 打印链表元素
            Console.WriteLine("链表元素:");
            list.ForEach(x => Console.Write(x + " "));
            Console.WriteLine();

            // 计算最大值
            int max = int.MinValue;
            list.ForEach(x => { if (x > max) max = x; });
            Console.WriteLine("最大值: " + max);

            // 计算最小值
            int min = int.MaxValue;
            list.ForEach(x => { if (x < min) min = x; });
            Console.WriteLine("最小值: " + min);

            // 计算总和
            int sum = 0;
            list.ForEach(x => sum += x);
            Console.WriteLine("总和: " + sum);
        }
    }

}

