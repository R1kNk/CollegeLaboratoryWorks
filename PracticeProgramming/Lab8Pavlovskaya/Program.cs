using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
class Card
{
    string lear;
    int number;
    string first_lear = "Пика";
    string second_lear = "Трефа";
    string third_lear = "Бубна";
    string fourth_lear = "Черва";
    public string Lear { get => lear; private set => lear = value; }
    public int Number { get => number; private set => number = value; }
    public Card()
    {
        Random randobj = new Random();
        int buf_number;
        ConsoleKeyInfo Key;
       
            Console.WriteLine("Зарандомить карту?\n1.Да\n2.Ввести самому");
            Key = Console.ReadKey();
            Console.WriteLine();

            switch (Key.KeyChar)
            {
                case '1':
                    {
                        buf_number = randobj.Next(1, 8);
                        switch (buf_number)
                        {
                            case 1: { Lear = first_lear; break; }
                            case 2: { Lear = second_lear; break; }
                            case 3: { Lear = third_lear; break; }
                            case 4: { Lear = fourth_lear; break; }
                            case 5: { Lear = first_lear; break; }
                            case 6: { Lear = second_lear; break; }
                            case 7: { Lear = fourth_lear; break; }
                            case 8: { Lear = third_lear; break; }
                    }
                        buf_number = randobj.Next(6, 14);
                        Number = buf_number;

                        break;
                    }
                case '2':
                    {
                        Console.WriteLine("Выберите масть карты:1.Пика\n2.Трефа\n3.Черва\n4.Бубна");
                        Key = Console.ReadKey();
                        Console.WriteLine();
                        switch (Key.KeyChar)
                        {
                            case '1': { Lear = first_lear; break; }
                            case '2': { Lear = second_lear; break; }
                            case '3': { Lear = third_lear; break; }
                            case '4': { Lear = fourth_lear; break; }
                            default:
                                {
                                    buf_number = randobj.Next(1, 4);
                                    Console.WriteLine("Неверная цифра. Рандомная номер масти - {0}", buf_number);
                                    switch (buf_number)
                                    {
                                        case 1: { Lear = first_lear; break; }
                                        case 2: { Lear = second_lear; break; }
                                        case 3: { Lear = third_lear; break; }
                                        case 4: { Lear = fourth_lear; break; }
                                    }
                                    break;
                                }
                        }
                        Console.WriteLine("Выберите номер карты от 6 до 14");
                        buf_number = Convert.ToInt32(Console.ReadLine());
                        if (buf_number < 6 || buf_number > 14) { Console.WriteLine("Неверный диапазон. Номер будет зарандомлен"); buf_number = randobj.Next(6, 14); Console.WriteLine(buf_number); }
                        Number = buf_number;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Такой опции нет. Будет осуществлен рандом.");
                        buf_number = randobj.Next(1, 4);
                        switch (buf_number)
                        {
                            case 1: { Lear = first_lear; break; }
                            case 2: { Lear = second_lear; break; }
                            case 3: { Lear = third_lear; break; }
                            case 4: { Lear = fourth_lear; break; }
                        }
                        buf_number = randobj.Next(6, 14);
                        Number = buf_number;
                        break;
                    }
            }

        
    }
    private Card(bool isIt)
    {
        Lear = " ";
        Number = 0;
    }
   static public Card ChangeCard(string lear1, int number1)
    {
        Card res = new Card(true);
        res.Lear = lear1;
        res.Number = number1;
        return res;
    }
}
      

class ClassDeck
{
    Card[] deck;
    int deck_length;

    public int Deck_length { get => deck_length; private set => deck_length = value; }

    public void fabricDeck(int Count_Cards)
    {
        deck = new Card[Count_Cards];
        for (int i = 0; i < deck.Length; i++)
        {
            Console.WriteLine("Карта {0}", i + 1);
            deck[i] = new Card();
            Console.Clear();
        }
        this.Deck_length = deck.Length;
    }
    public void OutAllInfo()
    {
        Console.WriteLine("\nИнформация о всех картах:");
        for (int i = 0; i < deck.Length; i++)
        {
            Console.WriteLine("\nКарта {0}:\nМасть {1}\nНомер {2}", i + 1, deck[i].Lear, deck[i].Number);
        }
    }
    public void OutNumberCardInfo(int number)
    {
        if (number > deck.Length || number <= 0) Console.WriteLine("Неверный номер карты");
        else Console.WriteLine("\nКарта №{0}:\nМасть: {1}\nНомер: {2}", number, deck[number - 1].Lear, deck[number - 1].Number);
    }
    public void DeSort()
    {
        Random rand = new Random(DateTime.Now.Millisecond);
        string buf_lear;
        int buf_number;
        int swap1;
        int swap2;
        for (int i = 0; i < 1000; i++)
        {
            swap1 = rand.Next(0, deck.Length - 1);
            swap2 = rand.Next(0, deck.Length - 1);
            buf_lear = deck[swap1].Lear;
            buf_number = deck[swap1].Number;
            deck[swap1] = Card.ChangeCard(deck[swap2].Lear, deck[swap2].Number);
            deck[swap2] = Card.ChangeCard(buf_lear, buf_number);

        }
        Console.WriteLine("\nКарты перемешаны");
    }
    public void OutSixRandomCards()
    {
        Console.WriteLine("\nШесть рандомных карт:");
        int counter_for_array = 0;
        int counter_cards = 0;
        int[] AlreadyExisted = new int[6];
        for (int i = 0; i < AlreadyExisted.Length; i++)
        AlreadyExisted[i] = -1;
        bool isExist = false;
        Random rand = new Random(DateTime.Now.Millisecond);
        for (counter_cards = 0; counter_cards < 6;)
        {
            isExist = false;
            int buf_number = rand.Next(1, deck.Length);
            for (int i = 0; i < AlreadyExisted.Length; i++)
                if (buf_number == AlreadyExisted[i]) { isExist = true;  break; }
            if (!isExist)
            {
                AlreadyExisted[counter_for_array] = buf_number;
                counter_for_array++;
                Console.WriteLine("\nКарта №{0}:\nМасть: {1}\nНомер: {2}", buf_number, deck[buf_number - 1].Lear, deck[buf_number - 1].Number);
                counter_cards++;
            }
        }
    }
    public void OutAllCardsRandomly()
    {
        Console.WriteLine("\nВсе карты, выданные рандомно:");
        int counter_cards = 0;
        Random rand = new Random(DateTime.Now.Millisecond);
        for (counter_cards = 0; counter_cards < deck.Length;)
        {
           int buf_number = rand.Next(1, deck.Length);
           Console.WriteLine("\nКарта №{0}:\nМасть: {1}\nНомер: {2}", buf_number, deck[buf_number - 1].Lear, deck[buf_number - 1].Number);
           counter_cards++;
        }
    } 
}
namespace Lab8Pavlovskaya
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassDeck OurDeck=new ClassDeck();
            ConsoleKeyInfo key;
            Console.WriteLine("Введите количество карт в вашей колоде:");
            bool rand = true;
            OurDeck.fabricDeck(Convert.ToInt32(Console.ReadLine()));
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Размер колоды: {0}", OurDeck.Deck_length);
                Console.WriteLine("Выберите действие:\n1.Вывести все карты по порядку\n2.Вывести одну карту по её номеру\n3.Вывести карты поодиночке рандомно(с повторениями)\n4.Вывести 6 неповторяющихся рандомных карт\n5.Перемешать колоду\n6.Выход");
                key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1':
                        {
                            OurDeck.OutAllInfo();
                            Console.ReadKey();
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine("\nКакую карту в колоде вы хотите посмотреть?");
                            int card_number = Convert.ToInt32(Console.ReadLine());
                            OurDeck.OutNumberCardInfo(card_number);
                            Console.ReadKey();

                            break;
                        }
                    case '3':
                        {
                            OurDeck.OutAllCardsRandomly();
                            Console.ReadKey();
                            break;
                        }
                    case '4':
                        {
                            OurDeck.OutSixRandomCards();
                            Console.ReadKey();
                            break;
                        }
                    case '5':
                        {
                            OurDeck.DeSort();
                            Console.ReadKey();
                            break;
                        }
                    case '6':
                        {
                            exit = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Такого пункта нет!");
                            break;
                        }
                        break;
                }
            }
            
        }
    }
}
