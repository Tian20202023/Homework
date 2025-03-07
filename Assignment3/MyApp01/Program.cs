// See https://aka.ms/new-console-template for more information
using System;

namespace MyApp01
{
    public interface IShape
    {
        double GetArea();
        bool IsValid();
        void show();
    }
    //长方形
    public class Rectangle : IShape
    {
        private double Width;
        private double Height;

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double GetArea()
        {
            return Width * Height;
        }

        public bool IsValid()
        {
            return Width > 0 && Height > 0;
        }

        public void show()
        {
            Console.WriteLine("I am Rectangle  "+GetArea());
        }
    }

    public class Square : IShape
    {
        private double Side;

        public Square(double side)
        {
            Side = side;
        }

        public double GetArea()
        {
            return Side * Side;
        }

        public bool IsValid()
        {
            return Side > 0;
        }
        public void show()
        {
            Console.WriteLine("I am Square  "+GetArea());
        }
    }
    public class Triangle:IShape
    {
        private double a;
        private double b;
        private double c;

        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public double GetArea()
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public bool IsValid()
        {
            if (a > 0 && b > 0 && c > 0 && a + b > c && b + c > a && a + c > b)
            {
                return true;
            }
            else return false;
        }
        public void show()
        {
            Console.WriteLine("I am Triangle  "+GetArea());
        }
    }

    public static class ShapeFactory
    {
        public static IShape CreateRandomShape()
        {
            Random random = new Random();
            int shapeType = random.Next(3); // 0: Rectangle, 1: Square, 2: Triangle

            switch (shapeType)
            {
                case 0:
                    return new Rectangle(random.NextDouble() * 10 + 1, random.NextDouble() * 10 + 1);
                case 1:
                    return new Square(random.NextDouble() * 10 + 1);
                default:
                    return new Triangle(random.NextDouble() * 10 + 1, random.NextDouble() * 10 + 1,random.NextDouble() * 10 + 1);
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            List<IShape> shapes = new List<IShape>();
            double sum = 0;
            for (int i = 0; i < 10; i++)
            { 
                IShape shape = ShapeFactory.CreateRandomShape();
                if (shape.IsValid())
                {
                    shapes.Add(shape);
                    shape.show();
                    sum += shape.GetArea();
                }
            }
            Console.WriteLine("总面积："+sum);
        }
    }
}





