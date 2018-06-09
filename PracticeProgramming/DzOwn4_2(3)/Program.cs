using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

struct point
{
    public double x;
    public double y;

}
static class IsPointExistsInShape
{
    static public bool IsExistsInTriangle(point A, point B, point C, point P)
    {
        double k1 = 0, b1 = 0, k2 = 0, b2 = 0;
        b1 = (C.y - (k1 * C.x));
        k1 = ((A.y - b1) / A.x);
        b2 = (C.y - (k2 * C.x));
        k2 = ((B.y - b2) / B.x);
        if ((P.y < k1 * P.x + b1) && (P.y < (k2 * P.x + b2)) && P.y > -1) return true;
        else return false;
    }
}
class Program
{
    static void Main()
    {
        point A = new point();
        A.x = -1; A.y = -1;
        point B = new point();
        B.x = 1; B.y = -1;
        point C = new point();
        C.x = 0; C.y = 2;
        point P = new point();
        Console.WriteLine("Введите координату x");
        P.x = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Введите координату y");
        P.y = Convert.ToDouble(Console.ReadLine());
        if (IsPointExistsInShape.IsExistsInTriangle(A, B, C, P)) Console.WriteLine("Точка находится в заданной плоскости");
        else Console.WriteLine("Точка не принадлежит плоскости");
    }
}
