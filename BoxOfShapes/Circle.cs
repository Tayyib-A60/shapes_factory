using System;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace BoxOfShapes
{

    public class Circle : Shape
    {
        private DataAccess _dataAccess;
        
        private double _radius;
        public double prevRadius;
        
        public double Radius{
            get{ return this._radius;}
            set
            { 
               this._radius = value;
               this.ChangeState(); 
            }
        }

        public Circle(DataAccess dt,double radius)
        {
            this._dataAccess = dt;
            this._radius = radius;
           
        }

    
        public static List<Circle> Select(DataAccess dt)
        {
            var circles = new List<Circle>();
          
            string queryString = "SELECT radius FROM dbo.circles";
            
            SqlDataReader reader = null;
            try
            {
                reader =  dt.Select(queryString);
                while(reader.Read())
                {
                    circles.Add(new Circle(dt,reader.GetDouble(0)){
                        State = ObjectState.Unchanged,
                        prevRadius = reader.GetDouble(0)
                        });     
                }
            
            }
            finally
            {
                reader?.Close();
                
            }

            return circles;
            
        }


        internal override void Insert()
        {
            Dictionary<string,double> dict = new Dictionary<string,double>();
            string addCircles = "INSERT INTO dbo.circles(radius) VALUES(@radius)";
            dict.Add("@radius",this._radius);
            _dataAccess.PerformOperation(addCircles,dict);
        }

        internal override void Update()
        {
            Dictionary<string,double> dict = new Dictionary<string,double>();
            string updateCircles = "UPDATE dbo.circles SET radius=@newRadius WHERE radius=@radius";
            dict.Add("@radius",this.prevRadius);
            dict.Add("@newRadius",Radius);
            _dataAccess.PerformOperation(updateCircles,dict);
        }


        internal  override void Delete()
        {
           Dictionary<string,double> dict = new Dictionary<string,double>();
            string deleteCircle = "DELETE FROM dbo.circles WHERE radius=@radius";
            dict.Add("@radius",_radius);    
           _dataAccess.PerformOperation(deleteCircle,dict);
        }

        public override double Area()
        {
            const double PI = 3.14;

            double area  = PI * _radius * _radius; 

            return area;
            
        }
        
        public override string ToString() 
        {
            return $"Radius: {_radius}";
        }
    }
}
