using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
struct patient
{
    public int ward;
    public int cot;
    public double temp; 
}

class SolvingTaskMassive
{
    static int counter = 0;
    static public void SimpleMassiveObjective(double[,] hospital)
    {

        bool is_exist = false;
        Console.WriteLine("Всего в больнице {0} палат\n", hospital.Length);
        for (int i = 0; i < hospital.GetLength(0); i++)
        {
            Console.WriteLine("В палате {0} находится {1} коек(и).\n", i+1, hospital.GetLength(0));
            for (int j = 0; j < hospital.GetLength(1); j++)
            {
                if (hospital[i, j] != 0 || hospital[i, j] > 0)
                {
                    counter++;
                    Console.WriteLine("В койке {0} лежит пациент с температурой {1}", j+1, hospital[i, j]);
                }
            }
        }
        patient[] patient_list = new patient[counter];
        Console.WriteLine("\nИнформация о пациентах с одинаковой температурой\n");
        double[] check = new double[counter];
        for (int i = 0; i < hospital.GetLength(0); i++)
            for (int j = 0; j < hospital.GetLength(1); j++)
            {
                for (int y = 0; y < check.Length; y++)
                    if (check[y] == hospital[i, j])
                        is_exist = true;
                if (!is_exist) for (int p = 0; p < check.Length; p++) if (check[p] == 0) { check[p] = hospital[i, j]; break; }
                is_exist = false;


            }
        for (int l = 0; l < patient_list.Length; l++) { patient_list[l].ward = -1; patient_list[l].cot = -1; }
        for (int p = 0; p < check.Length; p++)
        {
            for (int i = 0; i < hospital.GetLength(0); i++)
            {
                for (int j = 0; j < hospital.GetLength(1); j++)
                {
                    if (check[p] == hospital[i, j]) SolvingTaskMassive.EnterIntoStruct(i, j, hospital[i, j], ref patient_list);
                }
            }
        }
        int counter_for_out = 0;
        for (int i = 0; i < check.Length; i++)
        {
            for (int j = 0; j < patient_list.Length; j++)
            {
                if (check[i] == patient_list[j].temp) counter_for_out++;
            }
            if (counter_for_out >= 2)
            {
                Console.WriteLine("Пациенты с одинаковой температурой равной {0}\n", check[i]);
                for (int y = 0; y < patient_list.Length; y++)
                {
                    if (check[i] == patient_list[y].temp) Console.WriteLine("Пациент в палате {0} на койке {1}", patient_list[y].ward+1, patient_list[y].cot+1);
                }
            }
            counter_for_out = 0;
           
        }



    }
    static void EnterIntoStruct(int ward, int cot, double temp, ref patient[] patient_list)
    {
        for (int i = 0; i < patient_list.Length; i++)
        {
            if (patient_list[i].ward == -1 || patient_list[i].cot == -1) { patient_list[i].ward = ward; patient_list[i].cot = cot; patient_list[i].temp = temp; break; }
        }

    }    
    static public void ArrayObjective(double [,] hospital)
    {
        double[,] copy_hospital = new double[hospital.GetLength(0),hospital.GetLength(1)];
        Array.Copy(hospital, copy_hospital, hospital.GetLength(0)*hospital.GetLength(1));
        double current_temp=0;
        for(int i=0; i<copy_hospital.GetLength(0); i++)
        {
            for (int j = 0; j < copy_hospital.GetLength(1); j++)
            {
                if (copy_hospital[i, j] != 0)
                {
                    current_temp = copy_hospital[i, j];
                    if (SearchForNumber(hospital, current_temp) >= 2)
                    {
                        Console.WriteLine("Пациенты с температурой {0}:\n", current_temp);
                        SetAndOutNumber(ref copy_hospital, current_temp);
                    }
                }
            }
        }
    }
    static int SearchForNumber(double [,] array, double number) {
        int counter=0;
        for (int i = 0; i < array.GetLength(0); i++)
            for (int j = 0; j < array.GetLength(1); j++) if(array[i,j]==number)counter++;
        return counter;

    }
    static void SetAndOutNumber(ref double [,] array, double number)
    {
        for (int i = 0; i < array.GetLength(0); i++)
            for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == number)
                    {
                        Console.WriteLine("Пациент в палате {0} на койке {1}", i+1, j+1);
                        array[i, j] = 0;
                    }
                }
    }
}

    class Program
    {
        static void Main()
        {
            double count;
            Console.WriteLine("Введите кол-во палат в больнице");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите кол-во коек в каждой палате");
            int m = Convert.ToInt32(Console.ReadLine());
            double[,] hosp = new double[n,m];
            for (int i=0; i< hosp.GetLength(0); i++)
            for (int j = 0; j< hosp.GetLength(1); j++)
                {
                    Console.WriteLine("Введите температуру пациента, находящегося на койке {0}  в палате {1}", j+1, i+1);
                    count = Convert.ToDouble(Console.ReadLine());
                    hosp[i, j] = count;
                }
        Console.WriteLine("Обычная реализация:\n");
        SolvingTaskMassive.SimpleMassiveObjective(hosp);
        Console.WriteLine("\n\nУпрощенная реализация с использованием Array:\n");
        SolvingTaskMassive.ArrayObjective(hosp);
        }
    }
