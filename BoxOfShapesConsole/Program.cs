using System;
using BoxOfShapes;
using System.Collections.Generic;

namespace BoxOfShapesConsole
{
    class Program
    {
        
        private List<Shape> shapesList;
        private DataAccess dt;
        
        static void Main(string[] args)
        {
                var program = new Program();
                program.dt = new DataAccess("Data Source=.\\SQLEXPRESS;Database=ShapesFactory;Integrated Security=True;");

                while(true)
                {
                     program.ShapesOption();   
                }

        }
        public void ShapesOption()
        {
                shapesList = new List<Shape>();
                shapesList.AddRange(Circle.Select(dt));
                shapesList.AddRange(Triangle.Select(dt));
                shapesList.AddRange(Rectangle.Select(dt));
                System.Console.WriteLine("Shapes Properties\nIndex\tProperty");
                for (int i= 0; i < shapesList.Count; i++)
                {
                        System.Console.WriteLine(i + "\t" + shapesList[i].ToString());
                }
                
                System.Console.WriteLine(@"Do you want to insert a Shape, Calculate the Area of a shape, or not any of these?
Enter 1 to Insert
Enter 2  to Calculate Area 
Enter 3 if you don't want any of these");
                string userInput = Console.ReadLine();
                switch(userInput){
                        case "1": 
                                InsertShape();
                                
                                break;
                               
                        case "2": 
                                System.Console.WriteLine("Enter the index of a Shape to calculate Area: ");
                                int index = Convert.ToInt32(Console.ReadLine());
                                Shape shape = shapesList[index];
                                Console.WriteLine("The area of the shape is {0} square units",shape.Area());   
                                break;  
                        case "3": 
                                UpdateShape();        
                                break;              
                }
        }

        public void InsertShape()
        {
                Console.WriteLine("Do you want to insert a Circle,Triangle, or Rectangle: Enter 1 for Circle,"+
                " 2 for Triangle,3 for Rectangle,4 for Square");
                int insertIndex = Convert.ToInt32(Console.ReadLine());
                if ( insertIndex == 1)
                {
                        Console.WriteLine("Enter the radius of the circle you wish to insert");
                        double radius = Convert.ToDouble(Console.ReadLine());
                        Circle circle = new Circle(dt,radius);
                        circle.Save();
                        
                } 
                else if (insertIndex == 2)
                {
                        Console.WriteLine("Enter the base and height of the Triangle you wish to insert");
                        string insertInput = Console.ReadLine();
                        string[] insertInputArray = insertInput.Split();
                        
                        double triangleBase = Convert.ToDouble(insertInputArray[0]);
                        double triangleHeight = Convert.ToDouble(insertInputArray[1]);
                        Triangle triangle = new Triangle(dt,triangleBase,triangleHeight);
                        triangle.Save();
                }

                else if (insertIndex == 3)
                {
                        Console.WriteLine("Enter the length and breadth of the Rectangle you wish to insert separated by space e.g 6 8");
                        string insertInput = Console.ReadLine();
                        string[] insertInputArray = insertInput.Split();
                        
                        double rectangleLength = Convert.ToDouble(insertInputArray[0]);
                        double rectangleBreadth = Convert.ToDouble(insertInputArray[1]);
                        Rectangle rectangle = new Rectangle(dt,rectangleLength,rectangleBreadth);
                        rectangle.Save();
                }

                else if (insertIndex == 4)
                {
                        Console.WriteLine("Enter the length of the square you wish to insert");
                        double squareLength = Convert.ToDouble(Console.ReadLine());
                        Rectangle square = new Rectangle(dt,squareLength);
                        square.Save();
                }
                else
                {
                        System.Console.WriteLine("Invalid input...");
                }
        }


        public void UpdateShape()
        {
                System.Console.WriteLine("Enter the index of a Shape to perform an update operation: ");
                int index = Convert.ToInt32(Console.ReadLine());

                if (index < shapesList.Count)
                {
                        if (shapesList[index] is Circle)
                        {
                                Circle circle = (Circle)shapesList[index];
                                Console.WriteLine("Enter the new value of the radius of the circle ");
                                double newRadius = Convert.ToDouble(Console.ReadLine());
                                circle.Radius=newRadius;
                                circle.Save();
                        }
                        else if (shapesList[index] is Triangle){
                        
                                Triangle triangle = (Triangle)shapesList[index];
                                Console.WriteLine("Enter the new value of the base and height of the triangle "+
                                "separated by space e.g. 6 7 ");
                                string updateInput = Console.ReadLine();
                                string[] updateInputArray = updateInput.Split();
                                triangle.Base = Convert.ToDouble(updateInputArray[0]);
                                triangle.Height = Convert.ToDouble(updateInputArray[1]);
                                triangle.Save();
                        }

                         else if (shapesList[index] is Rectangle){
                                
                                Rectangle rectangle = (Rectangle)shapesList[index];
                                if (rectangle.Length == rectangle.Breadth)
                                {
                                        Console.WriteLine("Enter the new value of the length of the square: ");
                                        double rectangleLength = Convert.ToDouble(Console.ReadLine());
                                        rectangle.Length = rectangleLength;
                                }
                                else
                                {
                                        Console.WriteLine("Enter the new value of the length  and breadth of the Rectangle: ");
                                        string updateInput = Console.ReadLine();
                                        string[] updateInputArray = updateInput.Split();
                                        rectangle.Length = Convert.ToDouble(updateInputArray[0]);
                                        rectangle.Breadth = Convert.ToDouble(updateInputArray[1]);
                                        rectangle.Save();    
                                }        
                                
                        }
                }
                else
                {
                        System.Console.WriteLine("Index Outside the bounds of the list");
                }

                                
        }

            
     }


        

   
}

