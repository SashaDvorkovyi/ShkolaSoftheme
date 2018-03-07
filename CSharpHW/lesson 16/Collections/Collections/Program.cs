using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = new MyQueue<int>(1,2,3,4);
            queue.Enqueue(5);
            var value1 = queue.Dequeue();
            var value2 = queue.Peek();

            var stack = new MyStack<int>(1, 2, 3, 4);
            stack.Push(5);
            var value3 = stack.Pop();
            var value4 = stack.Peek();

            var dictionary = new MyDictionary<int, string>();
            dictionary.Add(1, "qqq");
            dictionary.Add(7, "aaa");
            dictionary.Add(3, "zzz");
            dictionary.Add(2, "www");
            dictionary.Add(10, "sss");
            dictionary.Sorted();
            dictionary.Remove(10);
            var value5 = dictionary.GetValue(1);
        }
    }
}
