using System;
using System.Globalization;
using System.Text;

namespace Lab3_Task12
{
    public class Shape
    {
        public string Color { get; set; }

        public Shape(string color)
        {
            Color = color;
        }

        public virtual double GetArea()
        {
            return 0;
        }

        public virtual void Draw()
        {
            Console.WriteLine($"[Shape] Малюю фігуру ({Color})");
        }

        // Невіртуальний метод
        public void GetInfo()
        {
            Console.WriteLine($"[Shape] Це фігура кольору {Color}");
        }
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius, string color) : base(color)
        {
            Radius = radius;
        }

        // override: поліморфізм працює
        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }

        public override void Draw()
        {
            Console.WriteLine($"[Circle] Малюю коло R={Radius} ({Color})");
        }

        // new: приховує метод базового класу, поліморфізму немає
        public new void GetInfo()
        {
            Console.WriteLine($"[Circle] Це коло R={Radius} ({Color})");
        }
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height, string color) : base(color)
        {
            Width = width;
            Height = height;
        }

        public override double GetArea()
        {
            return Width * Height;
        }

        public override void Draw()
        {
            Console.WriteLine($"[Rectangle] Малюю прямокутник {Width}x{Height} ({Color})");
        }

        public new void GetInfo()
        {
            Console.WriteLine($"[Rectangle] Це прямокутник {Width}x{Height} ({Color})");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture; // крапка в дробах

            Circle circle = new Circle(5.0, "червоний");
            Shape shapeRef = circle;   // посилання базового типу

            Console.WriteLine("=== override (поліморфізм працює) ===");
            circle.Draw();          // [Circle] ...
            shapeRef.Draw();        // [Circle] ... — override

            Console.WriteLine("\n=== new (поліморфізм НЕ працює) ===");
            circle.GetInfo();       // [Circle] ...
            shapeRef.GetInfo();     // [Shape] ... — new, тип змінної визначає метод

            Console.WriteLine("\n=== Масив Shape[] з різними фігурами ===");
            Shape[] shapes =
            {
                new Circle(5.0, "червоний"),
                new Rectangle(4.0, 6.0, "синій"),
                new Circle(3.0, "зелений")
            };

            foreach (Shape s in shapes)
            {
                Console.WriteLine($"Площа: {s.GetArea():F2}");
                s.Draw();       // віртуальний виклик
                s.GetInfo();    // викликається Shape.GetInfo()
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
