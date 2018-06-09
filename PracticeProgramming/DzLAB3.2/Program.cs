using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
static class CheckShape
{
    static public bool CircleIntoSquare(double SSquare, double SCircle)
    {
        double radius = Math.Sqrt(SCircle / Math.PI);
        if (Math.Sqrt(SSquare) > (2 * radius))
            return true;
        else return false;
    }
    static public bool SquareIntoCircle(double SSquare, double SCircle)
    {
        double radius = Math.Sqrt(SCircle / Math.PI);
        if (Math.Sqrt(SSquare) < (2 * radius))
            return true;
        else return false;
    }

}

    class DzLab3Main
    {
        static void Main()
        {
        double S_Square, S_Circle;
        Console.WriteLine("Введите площадь квадрата:");
        S_Square = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Введите площадь круга:");
        S_Circle = Convert.ToDouble(Console.ReadLine());
        bool CircleIntoSquare = CheckShape.CircleIntoSquare(S_Square, S_Circle);
        bool SquareIntoCircle = CheckShape.ReferenceEquals(S_Square, S_Circle);
        bool impossible=false;
        if (SquareIntoCircle && CircleIntoSquare == false) impossible = true;
        if (CircleIntoSquare) Console.WriteLine("Мы можем вписать данный круг в квадрат");
        else if (SquareIntoCircle) Console.WriteLine("Мы можем вписать данный квадрат в круг");
        if (impossible) Console.WriteLine("Мы не можем вписать круг в квадрат и квадрат в круг");

    }
    }
