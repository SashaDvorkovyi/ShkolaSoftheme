using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unique_value
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arrey = new int[200001];
            int i = 0;
            for (int t = 0; t < 20033; t++)
            {
                arrey[i] = t;
                i++;
                arrey[i] = t;
                i++;
            }
            arrey[i] = 20033;
            i++;
            for (int t = 20034; t < 100000; t++)
            {
                arrey[i] = t;
                i++;
                arrey[i] = t;
                i++;
            }
            //int[] arrey = {  0, 0, 4, 1, 1, 2, 2, 3, 3, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10 };

            int lastIndex = arrey.Length;
            int firstIndex = 0;
            int a = Value(arrey, firstIndex, lastIndex);
            Console.WriteLine(a);
            Console.ReadKey();
        }
        public static int Value(int[] arrey, int firstIndex, int lastIndex)
        {

            int value;

            if(lastIndex - firstIndex < 2)
            {
                if (firstIndex % 2 == 0)
                {
                    value = arrey[firstIndex]; 
                }
                else
                {
                    value = arrey[lastIndex];
                }
            }
            else
            {
                if (((lastIndex + firstIndex) / 2) % 2 == 0)
                {
                    if (arrey[(lastIndex + firstIndex) / 2 - 2] == arrey[(lastIndex + firstIndex) / 2 - 1])
                    {
                        firstIndex = (lastIndex + firstIndex) / 2;
                        value = Value(arrey, firstIndex, lastIndex);
                    }
                    else
                    {
                        lastIndex = (lastIndex + firstIndex) / 2;
                        value = Value(arrey, firstIndex, lastIndex);
                    }
                }
                else
                {
                    if (arrey[(lastIndex + firstIndex) / 2 - 1] == arrey[(lastIndex + firstIndex) / 2])
                    {
                        firstIndex = (lastIndex + firstIndex) / 2;
                        value = Value(arrey, firstIndex, lastIndex);
                    }
                    else
                    {
                        lastIndex = (lastIndex + firstIndex) / 2;
                        value = Value(arrey, firstIndex, lastIndex);
                    }
                }
            }

                return value;
        }
    }
}
