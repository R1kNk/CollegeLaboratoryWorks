using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class OwnLinkedList<T> : IEnumerable<T>  
    {
         class Node<T>
        {
            public Node(T data)
            {
                Data = data;
            }
            public T Data { get; set; }
            public Node<T> Next { get; set; }
      

        }
    Node<T> head; 
        Node<T> tail; 
        int count;  

    public void Add(T data)
        {
            Node<T> node = new Node<T>(data);

            if (head == null)
                head = node;
            else
                tail.Next = node;
            tail = node;
        Console.WriteLine("Добавлен элемент {0}", data);
            count++;
        }
    public void ViewHead()
    {
        if(count!=0)
        Console.WriteLine("{0}", head.Data);
    }
    public void ViewTail()
    {
        if (count != 0)
            Console.WriteLine("{0}", tail.Data);
    }
    public bool Remove(T data)
        {
            Node<T> current = head;
            Node<T> previous = null;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        
                        if (current.Next == null)
                            tail = previous;
                    }
                    else
                    {
                        
                        head = head.Next;

                        if (head == null)
                            tail = null;
                    }
                    count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            }
            return false;
        }

    public void ViewAll()
    {
        if (count > 0)
        {
            Node<T> current = head;
            while (current != tail)
            {
                Console.Write("{0} ", current.Data);
                current = current.Next;
                if (current == tail)
                {
                    Console.Write("{0} ", current.Data);
                    Console.WriteLine();
                }
            }
        }
    }

        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public bool Contains(T data)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }

        public void AppendFirst(T data)
        {
            Node<T> node = new Node<T>(data);
            node.Next = head;
            head = node;
            if (count == 0)
                tail = head;
            count++;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    public static OwnLinkedList<T> operator -(OwnLinkedList<T> obj1)
    {
        Node<T> previous = null;
        previous = obj1.head;
        if (obj1.count != 0)
        {
            if (obj1.count > 1)
            {
                while (previous.Next != obj1.tail)
                {
                    previous = previous.Next;
                }
                obj1.tail = previous;

            }
            else
            {
                obj1.head = null;
                obj1.tail = null;
            }
        }
        return obj1;
    }
    public static OwnLinkedList<T> operator -(OwnLinkedList<T> obj1,T data)
    {
        Node<T> current = obj1.head;
        Node<T> previous = null;
        if(obj1.Count>0)
        while (current != null)
        {
            if (current.Data.Equals(data))
            {
                if (previous != null)
                {
                    previous.Next = current.Next;


                    if (current.Next == null)
                        obj1.tail = previous;
                }
                else
                {

                    obj1.head = obj1.head.Next;

                    if (obj1.head == null)
                        obj1.tail = null;
                }
                obj1.count--;
            }

            previous = current;
            current = current.Next;
        }
        return obj1;
    }
    public static OwnLinkedList<T> operator +(OwnLinkedList<T> obj1,OwnLinkedList<T> obj2)
    {
        Node<T> current = obj2.head;
        if (obj2.Count > 0)
        {
            while (current != obj2.tail)
            {
                obj1.Add(current.Data);
                current = current.Next;
            }
            obj1.Add(current.Data);
        }
        return obj1;
       
    }
    public static bool operator ~(OwnLinkedList<T> obj1)
    {
       bool ex = (obj1.Count>0) ?  true: false;
        return ex;
    }
    public static bool operator ==(OwnLinkedList<T> obj1, OwnLinkedList<T> obj2)
    {
        if (obj1.Count != obj2.Count) return false;
        return true;
       
    }
    public static bool operator !=(OwnLinkedList<T> obj1, OwnLinkedList<T> obj2)
    {
        if (obj1.Count != obj2.Count) return true;
        return false;
    }
}
class OwnStack<T>
    {
        List<T> ourList;
        public OwnStack()
        {
            ourList = new List<T>();
        }
        public void pushElement(T obj)
        {
            ourList.Add(obj);

        }
        public void popElement()
        {
            ourList.RemoveAt(ourList.Count - 1);
        }
        public void viewTop()
        {
            Console.WriteLine("{0}", ourList[ourList.Count - 1]);
        }

    }


class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            ConsoleKeyInfo key;
            OwnStack<int> stack = new OwnStack<int>();
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:\n1.Добавить элемент в стэк\n2.Удалить элемент из стэка\n3.Вывести верхушку стэка\n4.Выход");
                key = Console.ReadKey();
                Console.WriteLine();
                switch (key.KeyChar)
                {
                    case '1':
                        {
                            stack.pushElement(Convert.ToInt32(Console.ReadLine()));
                            break;
                        }
                    case '2':
                        {
                            stack.popElement();
                            break;
                        }
                    case '3':
                        {
                            Console.WriteLine();
                            stack.viewTop();
                            Console.ReadKey();
                            break;
                        }
                    case '4':
                        {
                            exit = true;
                            break;
                        }

                }
            }
        OwnLinkedList<int> list = new OwnLinkedList<int>();
        list.Add(7);
        list.Add(4);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.ViewAll();
        OwnLinkedList<int> list1 = new OwnLinkedList<int>();
        list1.Add(3);
        list1.Add(5);
        list1.ViewAll();
        list = list - 4;
        list.ViewAll();
        list1 = list1 + list;
        list1.ViewAll();
        if (~list1){
            Console.WriteLine("list1 не пустой");
        }else Console.WriteLine("list1 пустой");
        list1.Clear();
        if (~list1)
        {
            Console.WriteLine("list1 не пустой");
        }
        else Console.WriteLine("list1 пустой");
    }
    
}

