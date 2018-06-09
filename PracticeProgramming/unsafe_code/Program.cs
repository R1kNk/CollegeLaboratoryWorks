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

    unsafe static public void SimpleMassiveObjective(double* hospital, int rows, int columns)
    {

        bool is_exist = false;
        Console.WriteLine("Всего в больнице {0} палат\n", rows);
        for (int i = 0; i < rows; i++)
        {
            Console.WriteLine("В палате {0} находится {1} коек(и).\n", i + 1, columns);
            for (int j = 0; j < columns; j++)
            {
                if (hospital[i * columns + j] != 0 || hospital[i * columns + j] > 0)
                {
                    counter++;
                    Console.WriteLine("В койке {0} лежит пациент с температурой {1}", j + 1, hospital[i * columns + j]);
                }
            }
        }
        patient[] patient_list = new patient[counter];
        Console.WriteLine("\nИнформация о пациентах с одинаковой температурой\n");
        double* check = stackalloc double[counter];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
            {
                for (int y = 0; y < counter; y++)
                
                    if (check[y] == hospital[i * columns + j])
                        is_exist = true;
                    if (!is_exist) for (int p = 0; p < counter; p++) if (check[p] == 0) { check[p] = hospital[i * columns + j]; break; }
                    is_exist = false;
                


            }
        for (int l = 0; l < patient_list.Length; l++)
        {
            fixed (patient* pointerPatient = &patient_list[l])
            {
                pointerPatient->ward = -1; pointerPatient->cot = -1;
            }
        }
        for (int p = 0; p < counter; p++)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (check[p] == hospital[i * columns + j]) SolvingTaskMassive.EnterIntoStruct(i, j, hospital[i * columns + j], ref patient_list);
                }
            }
        }
        int counter_for_out = 0;
        for (int i = 0; i < counter; i++)
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
                    if (check[i] == patient_list[y].temp) Console.WriteLine("Пациент в палате {0} на койке {1}", patient_list[y].ward + 1, patient_list[y].cot + 1);
                }
            }
            counter_for_out = 0;

        }



    }
    unsafe static void EnterIntoStruct(int ward, int cot, double temp, ref patient[] patient_list)
    {
        for (int i = 0; i < patient_list.Length; i++)
        {
            fixed (patient* pointerPatient = &patient_list[i])
            {
                if (pointerPatient->ward == -1 || pointerPatient->cot == -1) { pointerPatient->ward = ward; pointerPatient->cot = cot; pointerPatient->temp = temp; break; }

            }
        }

    }
    unsafe static public void ArrayObjective(double[,] hospital)
    {
        double[,] copy_hospital = new double[hospital.GetLength(0), hospital.GetLength(1)];
        Array.Copy(hospital, copy_hospital, hospital.GetLength(0) * hospital.GetLength(1));
        double current_temp = 0;
        for (int i = 0; i < copy_hospital.GetLength(0); i++)
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
    unsafe static int SearchForNumber(double[,] array, double number)
    {
        int counter = 0;
        for (int i = 0; i < array.GetLength(0); i++)
            for (int j = 0; j < array.GetLength(1); j++) if (array[i, j] == number) counter++;
        return counter;

    }
    unsafe static void SetAndOutNumber(ref double[,] array, double number)
    {
        for (int i = 0; i < array.GetLength(0); i++)
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] == number)
                {
                    Console.WriteLine("Пациент в палате {0} на койке {1}", i + 1, j + 1);
                    array[i, j] = 0;
                }
            }
    }
}


class Program
{
    static void Main()
    {
        unsafe
        {
            int rows = 10,
             columns = 4;

            int* ptr = stackalloc int[rows * columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    ptr[i * columns + j] = rows;
                }
            }
            int counter = default(int);
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine("В палате {0} находится {1} коек(и).\n", i + 1, rows);
                for (int j = 0; j < columns; j++)
                {
                    if (ptr[i * columns + j] != 0 || ptr[i * columns + j] > 0)
                    {
                        counter++;
                        Console.WriteLine("В койке {0} лежит пациент с температурой {1}", j + 1, ptr[i * columns + j]);
                    }
                }
            }
            //
            double count;
            Console.WriteLine("Введите кол-во палат в больнице");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите кол-во коек в каждой палате");
            int m = Convert.ToInt32(Console.ReadLine());
            double* hosp = stackalloc double[n * m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    Console.WriteLine("Введите температуру пациента, находящегося на койке {0}  в палате {1}", j + 1, i + 1);
                    count = Convert.ToDouble(Console.ReadLine());
                    hosp[i * m + j] = count;
                }
            Console.WriteLine("Обычная реализация:\n");
            SolvingTaskMassive.SimpleMassiveObjective(hosp, n, m);
        }


    }
}
