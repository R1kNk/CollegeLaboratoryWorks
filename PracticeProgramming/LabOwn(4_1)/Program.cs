using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SolutionOfExample
{
    static public int Solving(int N, int k, out int res)
    {
        res = 4 * k;
        if (N % 4 == 0) return 0;
        else if (N % 4 == 1) return 1;
        else if (N % 4 == 2) return 2;
        else if (N % 4 == 3) return 3;
        else return -1;
    }

}
    class Program
    {
        static void Main()
        {
        Console.WriteLine("Дано натуральное число N. Если оно делится на 4, вывести на экран ответ N = 4k (где k — соответствующее частное); если остаток от деления на 4 равен 1, N = 4k + 1; если остаток от деления на 4 равен 2, N = 4k + 2; если остаток от деления на 4 равен 3, N = 4k + 3. Например, 12 = 43, 22 = 45 + 2");
        Console.WriteLine("\nВведите число N:");
        int n = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите число k:");
        int k = Convert.ToInt32(Console.ReadLine());
        int mod;
        int result = SolutionOfExample.Solving(n, k, out mod);
        if (result == 0 || result == -1) Console.WriteLine("{0} = {1}", n, mod);
        else Console.Write("{0} = {1} + {2}", n, mod, result+"\n");
    }
    }
