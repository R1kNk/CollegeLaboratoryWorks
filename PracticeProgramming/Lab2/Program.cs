using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class SolvingExample
{
    static private double E = Math.E;
static private double x1 = 2.444, y1 = (0.869)*Math.Pow(10, -2), z1 = -0.13 * Math.Pow(10,3), h1 = -0.49871;

   static public double Exp
    {
        get {
            return E;
        }
        private set {
             E = value;
        }

    }


    static public double Y1 { get => y1; private set => y1 = value; }
    static public double X1 { get => x1; private set => x1 = value; }
    static public double Z1 { get => z1; private set => z1 = value; }
    static public double H1 { get => h1; private set => h1 = value; }

}
    class Lab2_main
    {
        static void Main()
        {
        double h2;
        h2 = (((Math.Pow(SolvingExample.X1, SolvingExample.Y1 - 1.0)) + Math.Pow(SolvingExample.Exp, SolvingExample.Y1 - 1.0)) / ((1.0 + SolvingExample.X1) * Math.Abs(SolvingExample.Y1 - Math.Tan(SolvingExample.Z1)))) * (1.0 + Math.Abs(SolvingExample.Y1 - SolvingExample.X1)) + (Math.Pow(Math.Abs(SolvingExample.Y1 - SolvingExample.X1), 2.0) / 2.0) - (Math.Pow(Math.Abs(SolvingExample.Y1 - SolvingExample.X1), 3.0) / 3.0);
        Console.WriteLine(h2);
       
    }
    }
