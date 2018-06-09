using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class FactExp {

static public double SoltionExp(int n)
    { 
       double y = 1;
        int buf = (int)n;
       
        while (buf > 1)
        {
            y = (buf - 1) + (1.0 / n);
            buf--;
        }
        return y;
    }

    

}

    class Program
    {
        static void Main(string[] args)
        {
        int counter = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("{0}", FactExp.SoltionExp(counter));
        }
    }
