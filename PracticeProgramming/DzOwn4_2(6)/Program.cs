using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class SolveSystem {
static public double SolveThreeStoreyd(double i)
    {
        int choose;
        if (((i > 5) || (5 == i)) && ((i < 10) || (i == 10))) choose =  1;
        else if (((i > 11) || (11 == i)) && ((i < 20) || (i == 20))) choose = 2;
        else choose= 3;
        switch (choose) {
            case 1:
                return i % 2;
                break;
            case 2:
                return (int)(i / 2);
                break;
            default:
                return 0;
                break;
        }
    }
}
    class Program
    {
        static void Main(string[] args)
        {
        Console.WriteLine("Введите i");
        double i = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Результат решения системы выражения  a = {0}", SolveSystem.SolveThreeStoreyd(i));
        }
    }
