using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            //NewMethod();
            DList list = new DList();
            list.AddFirst(20);
            list.AddFirst(30);
            list.AddFirst(50);
            list.Print();

            Console.WriteLine("adding 70");
            list.AddAfter(30, 70);
            list.Print();


            //DNode f = list.Search(30);
            //if (f != null)
            //    Console.WriteLine("Found:{0}", f.data);

        }

        private static void NewMethod()
        {
            SList list = new SList();
            list.AddFirst(20);
            list.AddFirst(101);
            list.AddFirst(30);
            list.Print();
            list.RemoveLast();
            Console.WriteLine("Printing after removal");
            list.Print();

            //Node f = list.Search(201);
            //if (f != null)
            //    Console.WriteLine("Found data is:{0}", f.data);
            //else
            //    Console.WriteLine("Not found");
        }
    }

    public class SList
    {
        Node head;
        Node tail;

        public SList()
        {
            head = null;
            tail = null;
        }

        public void AddFirst(int e)
        {
            Node n = new Node(e);
            n.next = head;
            head = n;

            if (tail == null)
                tail = n;
        }

        public void AddLast(int e)
        {
            Node n = new Node(e);
            if (tail != null)
                tail.next = n;
            tail = n;

            if (head == null)
                head = n;
        }

        public void RemoveFirst()
        {
            if (head != null)
            {
                Node x = head;
                head = head.next;
                x = null;
            }
        }

        public void RemoveLast()
        {
            Node x = head;
            while (x.next != tail)
            {
                x = x.next;
            }

            tail = x;
            tail.next = null;
        }

        public void Print()
        {
            Node x = head;
            while (x != null)
            {
                Console.WriteLine(x.data);
                x = x.next;
            }
        }

        public Node Search(int e)
        {
            Node x = head;
            while (x != null)
            {
                if (x.data == e)
                    return x;
                x = x.next;
            }

            return null;
        }
    }
    public class Node
    {
        public int data;
        public Node next;

        public Node(int e)
        {
            data = e;
            next = null;
        }
    }

    public class DNode
    {
        public int data;
        public DNode next;
        public DNode previous;

        public DNode(int e)
        {
            data = e;
            next = null;
            previous = null;
        }
    }

    public class DList
    {
        DNode head;
        DNode tail;

        public DList()
        {
            head = null;
            tail = null;
        }

        public void AddFirst(int e)
        {
            DNode n = new DNode(e);

            if (head == null)
            {
                head = n;
                tail = n;
            }
            else
            {
                n.next = head;
                head.previous = n;
                head = n;
            }
        }

        public void AddAfter(int x, int e)
        {
            DNode found = Search(x);
            DNode y = found.next;
            DNode n = new DNode(e);
            n.previous = found;
            n.next = y;
            found.next = n;
            y.previous = n;
        }

        public void Print()
        {
            DNode x = head;
            while(x != null)
            {
                Console.WriteLine(x.data);
                x = x.next;
            }
        }

        public DNode Search(int e)
        {
            DNode x = head;
            while(x != null)
            {
                if (x.data == e)
                    return x;
                x = x.next;
            }

            return null;
        }
    }
}
