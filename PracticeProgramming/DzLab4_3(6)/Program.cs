using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BankNumeration
{
    static public int CountPercent(double sum, double percent)
    {
        int counter = 0;
        double buf_sum = sum;
        double prom_sum;
        while (sum < buf_sum * 2)
        {
            prom_sum = ((sum / 100) * percent);
            sum += prom_sum;
            counter++;
        }
        return counter;
    }
}
    class Program
    {
        static void Main(string[] args)
    {
        Console.WriteLine("Введите изначальную сумму");
        double summa= Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Введите процент");
        double procent = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Через {0} лет сумма {1} будет больше в 2 раза ", BankNumeration.CountPercent(summa, procent),summa);



    }
    }
