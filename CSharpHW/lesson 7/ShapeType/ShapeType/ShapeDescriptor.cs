using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeType
{
    public class ShapeDescriptor
    {
        public List<Point> points;
        private string[] shape = { "straight", "quadrilateral", "triangle", "pentagon", "hexagon", "heptagon", "octagon", "polygon", };

        public ShapeDescriptor(Point a, Point b)
        {
           this.points = new List<Point>();
            points.Add(a);
            points.Add(b);
        }
        public ShapeDescriptor(Point a, Point b, Point c) : this(a, b)
        {
            points.Add(c);
        }
        public ShapeDescriptor(Point a, Point b, Point c, Point d) : this(a, b, c)
        {
            points.Add(d);
        }
        public ShapeDescriptor(Point a, Point b, Point c, Point d, Point e) : this(a, b, c, e)
        {
            points.Add(e);
        }
        public ShapeDescriptor(List<Point> p)
        {
            this.points = new List<Point>();
            points = p;
        }


        public string ShapeType()
        {
            var result = string.Empty;
            if(this.Area() == 0)
            {
                result = shape[0];
            }
            else if ((this.Area() != 0) && (points.Count - 1<= shape.Length))
            {
                result = shape[points.Count-1];

            }
            else
            {
                result = shape.Last();
            }
            return result;
        }

        public int Area()
        {
            var result = default(int);
            for(var i=0; i<this.points.Count-1; i++)
            {
                result = points[i].coordinatesA * points[i + 1].coordinatesB - points[i + 1].coordinatesA * points[i].coordinatesB;
            }
            result = Math.Abs(result + points[points.Count-1].coordinatesA * points[0].coordinatesB - points[0].coordinatesA * points[points.Count-1].coordinatesB);
            return result;
        }
    }
}
