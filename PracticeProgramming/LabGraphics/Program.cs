using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Solve
{
    static public double SolveGraphic1(double x, int counter_circles, params double[] array)
    {
        if (x >= -7 && x <= -6) return 1;
        else if (x > -6 && x < -4)
        {
            double k = -0.5;
            double b = -2;
            //y = kx + b;
            //0 = k*-4 +b; -b = k*-4 b = -k*4  b=4k
            // 1 = k*-6+(-k*4);1 = -10k k = -0,1
            return (k * x) + b;
        }
        else if (x >= -4 && x <= 0)
        {
            if (x == -4) return 0;
            else if (x == 0) return 0;
            else
            {
                // (y-[1])^2 = R^2 - (x-[0])^2 y^2 = [1]^2
                double radius = 2;
                double y = Math.Sqrt(Math.Pow(radius, 2) - (Math.Pow((x - (-2)), 2)));
                return y;
            }
        }
        else if (x > 0 && x <= 2)
        {
            if (x == 2) return 0;
            double radius = 1;
            double y = -(Math.Sqrt(Math.Pow(radius, 2) - (Math.Pow((x - array[2]), 2))));
            return y;

        }
        else if (x > 2 && x < 3)
        {
            double k = -1;
            double b = 2;
            //y = kx + b;
            //0 = 2k +b; -b = 2k b = -2k
            // -1 = 3k+b;1 = -10k k = 0, b=0;
            return k * x + b;
        }
        else return -2.2;
    }
    static public bool SolveGraphic2(double x, double y, double r)
    {
        if (x <= 0 && y >= 0)
        {
            if (x == 0 && y == 0) return true;
            double result = Math.Sqrt((Math.Pow((r - x), 2)) + Math.Pow((-r - y), 2));
            if (result < r) return false;
            else return true;

        }
        else
        if (x > 0 && y < 0)
        {
            if (y >= r && x <= r)
            {
                double result = Math.Sqrt((Math.Pow((0 - x), 2)) + Math.Pow((0 - y), 2));
                if (result > r) return false;
                else return true;
            }
            else return false;
        }
        else return false;
    }
}

    class Program
    {
        static void Main()
        {
        double x;
        bool exit=true;
        while (exit) {
            Console.Clear();
            Console.WriteLine("Выберите график: 1 или 2");
            int choise = Convert.ToInt32(Console.ReadLine());
            switch (choise)
            {
                case 1:
                    {
                        Console.WriteLine("Введите x для графика");
                        x = Convert.ToDouble(Console.ReadLine());
                        double[] arguments = { -2, 0, 1, 0 };
                        Console.WriteLine("y = {0}", Solve.SolveGraphic1(x, 2, arguments));
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Введите x для графика");
                        x = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите y для графика");
                        double y = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введите R для графика");
                        double R = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Точка принадлежит заштрихованным областям? - {0}", Solve.SolveGraphic2(x, y, R));
                        break;
                    }
                default: Console.WriteLine("Нет такого графика"); break;

            }
            Console.WriteLine("ещё?");
            exit = Convert.ToBoolean(Console.ReadLine());
        }
        }
    }
