using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct TwoIntDB {
    public int number;
   public int times;
}

static class CheckingMassive
{
    
    static public void CheckingForNumbers(int[] a)
    {
        TwoIntDB[] return_db = new TwoIntDB[a.Length / 2 + 1];
        Console.WriteLine("Исходный массив:");
        for (int v = 0; v < a.Length; v++) Console.Write("{0} ", a[v]);
        Console.WriteLine();
        for(int i=0; i<return_db.Length; i++)
        {
            return_db[i].times = -1;
        }
        int buf_number;
        bool is_exist = false;
        bool ok = false;
        int counter = 0;
        for (int i=0; i< a.Length; i+=2)
        {
            buf_number = a[i];
            for (int j = 0; j<a.Length; j+=2)
            {
                if (buf_number == a[j]) counter++;
            }
            for (int c=0; c<return_db.Length; c++)
            {
                if(return_db[c].number == buf_number)
                {
                    is_exist = true;
                }
            }
            if (is_exist==false)
            {
                for (int r = 0; r < return_db.Length; r++)
                {
                    if (return_db[r].times == -1)
                    {
                        return_db[r].number = buf_number;
                        return_db[r].times = counter;
                        ok = true;

                    }
                    if (ok) { break; }
                }
            }
            ok = false; is_exist = false; counter = 0;
        }
        Console.WriteLine("Числа близнецы:");
        for (int i =0; i< return_db.Length; i++)
        {   if(return_db[i].times!=-1&&(return_db[i].times==2||return_db[i].times>3))
            Console.WriteLine("Число-близнец {0} появлялось на четной позиции {1} раз\n", return_db[i].number, return_db[i].times);
        }
     
    }

}

    class Program
    {
        static void Main()
        {
        Console.WriteLine("Введите количество элементов массива");
        int count = Convert.ToInt32(Console.ReadLine());
        int[] arr = new int[count];
            for (int i = 0; i < arr.Length; i++)
            {
            try
            {
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Строка введена некорректно, данный элемент будет заменен на 0");
            }
        }
        Console.Clear();
        CheckingMassive.CheckingForNumbers(arr);
       
       
        }
    }
