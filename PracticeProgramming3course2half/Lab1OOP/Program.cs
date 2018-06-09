using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    class Timer
    {
        static public List<Timer> list = new List<Timer>();
        double tickQuantity;
        private Timer(double tick)
        {
            tickQuantity = tick;
        }
        static public Timer createSeconds(double seconds1, double seconds2)
        {
            double tick = (seconds1-seconds2) * 18.2;
            Timer tm = new Timer(tick);
            return tm;
        }
        static public Timer createTicks(double ticks1, double ticks2)
        {

            Timer tm = new Timer(ticks1-ticks2);
            return tm;
        }
        public double TickQuantity { get => tickQuantity; }
        public double SecondQuantity { get => tickQuantity / 18.2; }
        public double MinuteQuantity { get => tickQuantity / 18.2 / 60; }
        public Timer() { tickQuantity = 0.0; }
        public void InfoMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:\n1.Время в тиках\n2.Время в секундах\n3.Время в минутах\n4.Сложение\n5.Вычитание\n6.Выход");
                ConsoleKeyInfo key;
                key = Console.ReadKey();
                Console.WriteLine();
                switch (key.KeyChar)
                {
                    case '1':
                        {
                            Console.WriteLine(TickQuantity);
                            Console.ReadKey();
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine(SecondQuantity);
                            Console.ReadKey();
                            break;
                        }
                    case '3':
                        {
                            Console.WriteLine(MinuteQuantity);

                            Console.ReadKey();
                            break;
                        }
                case '4':
                    {
                        if (Timer.list.Count != 0)
                        {
                            Timer plus = new Timer();
                            Console.WriteLine("Выберите таймер для сложения");
                            int counter = 1;
                            foreach (Timer t in Timer.list)
                            {
                                Console.WriteLine("{0}.{1} ticks", counter, t.TickQuantity);
                                counter++;
                            }
                            int choise = Convert.ToInt32(Console.ReadLine());
                            counter = 1;
                            foreach (Timer t in Timer.list)
                            {
                                if (counter == choise) { plus = t; break; }
                                else counter++;
                            }
                            Timer res = new Timer();
                            res.tickQuantity = this.TickQuantity + plus.tickQuantity;
                            res.InfoMenu();
                        }
                        else Console.WriteLine("У вас нет таймеров."); break;
                    }
                case '5':
                    {
                        if (Timer.list.Count != 0)
                        {
                            Timer minus = new Timer();
                            Console.WriteLine("Выберите таймер для сложения");
                            int counter = 1;
                            foreach (Timer t in Timer.list)
                            {
                                Console.WriteLine("{0}.{1} ticks", counter, t.TickQuantity);
                                counter++;
                            }
                            int choise = Convert.ToInt32(Console.ReadLine());
                            counter = 1;
                            foreach (Timer t in Timer.list)
                            {
                                if (counter == choise) { minus = t; break; }
                                else counter++;
                            }
                            Timer res = new Timer();
                            res.tickQuantity = this.TickQuantity - minus.tickQuantity;
                            res.InfoMenu();
                        }
                        else Console.WriteLine("У вас нет таймеров."); break;
                    }
                case '6':
                        {
                            exit = true;
                            break;
                        }

                }
            }

        }
        public static Timer operator -(Timer obj1, Timer obj2)
        {
            Timer tm = new Timer();
            tm.tickQuantity = obj1.TickQuantity - obj2.TickQuantity;
            return tm;
        }

    public Timer plus(Timer r2)
    { return new Timer(this.tickQuantity + r2.tickQuantity); }
        public static Timer operator +(Timer obj1, Timer obj2)
        {
            Timer tm = new Timer();
            tm.tickQuantity = obj1.TickQuantity + obj2.TickQuantity;
            return tm;
        }


        class Program
        {
            static void Main(string[] args)
            {
                bool exit = false;
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("Выберите действие:\n1.Создать таймер\n2.Выбрать таймер для действий\n3.Выход");
                    ConsoleKeyInfo key;
                    key = Console.ReadKey();
                    Console.WriteLine();
                    switch (key.KeyChar)
                    {
                        case '1':
                            {
                                Console.WriteLine("1 - таймер в секундах. 2 - таймер в тиках");
                                Timer temp;
                                if (Console.ReadLine() == "1") { Timer.list.Add(temp = Timer.createSeconds(Convert.ToDouble(Console.ReadLine()), Convert.ToDouble(Console.ReadLine()))); }
                                else { Timer.list.Add(temp = Timer.createTicks(Convert.ToDouble(Console.ReadLine()), Convert.ToDouble(Console.ReadLine()))); }
                            Console.ReadKey();
                                break;
                            }
                        case '2':
                            {
                                if (Timer.list.Count != 0)
                                {
                                    Console.WriteLine("Выберите таймер");
                                    int counter = 1;
                                    foreach (Timer t in Timer.list)
                                    {
                                        Console.WriteLine("{0}.{1} ticks", counter, t.TickQuantity);
                                        counter++;
                                    }
                                    int choise = Convert.ToInt32(Console.ReadLine());
                                    counter = 1;
                                    foreach (Timer t in Timer.list)
                                    {
                                        if (counter == choise) { t.InfoMenu(); break; }
                                        else counter++;
                                    }
                                }
                                else Console.WriteLine("У вас нет таймеров.");
                                Console.ReadKey();
                                break;
                            }
                        
                        case '3':
                            {
                                exit = true;
                                break;
                            }

                    }
                }

            }
        }
    }
