using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_tree
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arrey = { 9, 5, 7, 10, 11, 22, 6, 15, 1, 8, 21, 22 };
            Tree root = new Tree(arrey[0]);
            for(var i=1; i<arrey.Length; i++)
            {
                BildTree(root, arrey[i]);
            }
            int a=0; 
            Sort(root, ref arrey, ref a);
            for(var t=0; t<arrey.Length; t++)
            {
                Console.Write(arrey[t]+", ");
            }
            Console.ReadKey();
        }
        public static void BildTree(Tree item, int value)
        {
            if (value > item.Value)
            {
                if (item.Right == null)
                {
                    item.Right = new Tree(value);
                }
                else
                {
                    BildTree(item.Right, value);
                }
            }
            else
            {
                if (item.Left == null)
                {
                    item.Left = new Tree(value);
                }
                else
                {
                    BildTree(item.Left, value);
                }
            }
        }
        public static void Sort(Tree root,ref int[] arrey, ref int i)
        {
            if (root.Left != null)
            {
                Sort(root.Left, ref arrey, ref i);
            }
            arrey[i] = root.Value;
            i++;
            if (root.Right != null)
            {
                Sort(root.Right, ref arrey, ref i);
            }

        }
    }
}
