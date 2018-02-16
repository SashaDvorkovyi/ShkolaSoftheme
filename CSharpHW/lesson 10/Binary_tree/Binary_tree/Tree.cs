using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_tree
{
    public class Tree
    {
        public Tree Right { get; set; }
        public Tree Left { get; set; }
        public int Value { get; set; }

        public Tree(int value)
        {
            Value = value;
        }
    }
}
