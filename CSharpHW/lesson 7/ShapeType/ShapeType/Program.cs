using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeType
{
    class Program
    {
        static void Main(string[] args)
        {
            var point1 = new Point(0, 0);
            var point2 = new Point(1, 0);
            var shapeDiscr1 = new ShapeDescriptor(point1, point2);
            Console.WriteLine("The form is: " + shapeDiscr1.ShapeType() + ". Area is:" + shapeDiscr1.Area());


            var point3 = new Point(2, 1);
            var point4 = new Point(4, 3);
            var point5 = new Point(5, 6);
            var point6 = new Point(5, 8);
            var point7 = new Point(4, 3);
            var point8 = new Point(1, -2);
            var point9 = new Point(0, -4);
            var point10 = new Point(-1, -2);

            List<Point> points = new List<Point>();
            points.Add(point1);
            points.Add(point2);
            points.Add(point3);
            points.Add(point4);
            points.Add(point5);
            points.Add(point6);
            points.Add(point7);
            points.Add(point9);
            points.Add(point9);
            points.Add(point10);

            var shapeDiscr2 = new ShapeDescriptor(point2, point3, point5);
            var shapeDiscr3 = new ShapeDescriptor(point2, point3, point5, point8);
            var shapeDiscr4 = new ShapeDescriptor(point2, point3, point5, point9, point10);
            var shapeDiscr5 = new ShapeDescriptor(points);

            Console.WriteLine("The form is: " + shapeDiscr2.ShapeType() + ". Area is:" + shapeDiscr2.Area());
            Console.WriteLine("The form is: " + shapeDiscr3.ShapeType() + ". Area is:" + shapeDiscr3.Area());
            Console.WriteLine("The form is: " + shapeDiscr4.ShapeType() + ". Area is:" + shapeDiscr4.Area());
            Console.WriteLine("The form is: " + shapeDiscr5.ShapeType() + ". Area is:" + shapeDiscr5.Area());
            Console.ReadKey();
        }
    }
}
