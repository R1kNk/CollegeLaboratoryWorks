using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITK13
{


    
    ///-------------------------------------
    class ClickEventArgs:EventArgs
    {
        private ConsoleKeyInfo key;
        public ClickEventArgs(ConsoleKeyInfo _key)
        {
            key = _key;
        }
        public ConsoleKey Key { get => key.Key; }
    }
    class ClickEvent
    {
        public event EventHandler<ClickEventArgs> Click;
        public void OnClickEvent(ConsoleKeyInfo key)
        {
            ClickEventArgs temp = new ClickEventArgs(key);
            if (Click != null)
                Click(this,temp);
        }

    }
    //--------------------------------------
    enum TV { News,Weather,Humor,Sport,Incident};
    class NewsEventArgs:EventArgs
    {
        private string _massage;
        public string Massage { get => _massage; }
        public NewsEventArgs(string massage)
        {
            _massage = massage;
        }
    }
    class TVEvent
    {
        public event EventHandler<NewsEventArgs> NewsEvent;
        public event EventHandler<NewsEventArgs> WeatherEvent;
        public event EventHandler<NewsEventArgs> HumorEvent;
        public event EventHandler<NewsEventArgs> SportEvent;
        public event EventHandler<NewsEventArgs> IncidentEvent;


        public void OnNewsEvent(string massage,TV sender)
        {
            NewsEventArgs args = new NewsEventArgs(massage);
            if (NewsEvent != null&&TV.News==sender)
                NewsEvent(this, args);
            if (WeatherEvent != null && TV.Weather == sender)
                WeatherEvent(this, args);
            if (HumorEvent != null && TV.Humor == sender)
                HumorEvent(this, args);
            if (SportEvent != null && TV.Sport == sender)
                SportEvent(this, args);
            if (IncidentEvent != null && TV.Incident == sender)
                IncidentEvent(this, args);
        }

    }
    class Person
    {
        public void NewsHandler(object sender,NewsEventArgs e)
        {
            Console.WriteLine("Сообщение");
            Console.WriteLine(e.Massage);
        }
    }


    delegate void Mass(string massage);


    class Program
    {
        static void Main(string[] args)
        {
            
            Person Sasha = new Person();
            Person Aleksey = new Person();
            TVEvent evn = new TVEvent();
            evn.HumorEvent += Sasha.NewsHandler;
            evn.NewsEvent += Aleksey.NewsHandler;
            evn.HumorEvent += Aleksey.NewsHandler;
            string message = "Начался чемпионат мира по футболу!";
            evn.OnNewsEvent(message,TV.Sport);
            string mas = "Трамп стал президентом";
            message = "Колобок повесился!";
            evn.OnNewsEvent(message, TV.Humor);
            Console.ReadKey();
            evn.OnNewsEvent(mas, TV.Incident);
            Console.Clear();

            Mass del=null;
            del += (n) => Console.WriteLine(n);
            del.Invoke(mas);
        }
        static void Handler(object sender,ClickEventArgs e)
        {
            Console.WriteLine("Sender->" + sender.ToString());
            if (e.Key == ConsoleKey.Home)
                Console.WriteLine("Нажата кнопка Home");
            else
                Console.WriteLine("\nНажата кнопка " + e.Key);
        }
    }
}
