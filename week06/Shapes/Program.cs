using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>();

        shapes.Add(new Square("red", 4));
        shapes.Add(new Rectangle("blue", 5, 3));
        shapes.Add(new Circle("green", 7));

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Color: {shape.GetColor()}, Area: {shape.GetArea():0.00}");
        }
    }
}