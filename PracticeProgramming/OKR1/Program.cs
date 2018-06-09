using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKR1
{
    class Square
    {
        double size;
        public Square(double sideSize)
        {
            if (sideSize >0) size = sideSize;
            else {throw new ArgumentException();}
            
        }

        public double Size { get => size; private set => size = value; }
        public double squareSquare { get => Math.Pow(Size, 2); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите сторону квадрата");
              Square first = new Square(Convert.ToDouble(Console.ReadLine()));
                Console.WriteLine("Длина стороны квадрата = {0}\nПлощадь квадрата = {1}", first.Size, first.squareSquare);
            }
            catch (ArgumentException e) { Console.WriteLine("Неправильный аргумент стороны квадрата"); }
        }
    }
}
