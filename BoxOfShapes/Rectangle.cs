using System;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace BoxOfShapes
{

    public class Rectangle : Shape
    {
        private DataAccess _dataAccess;
        
        private double _length;
        public double prevLength;
        private double _breadth;
        public double prevBreadth;
        
        public double Length{
            get{ return this._length;}
            set
            { 
               this._length = value;
               this.ChangeState(); 
            }
        }

        public double Breadth{
            get{ return this._breadth;}
            set
            { 
               this._breadth = value;
               this.ChangeState(); 
            }
        }

        public Rectangle(DataAccess dt,double length)
        {
            this._dataAccess = dt;
            this._length = length;
            this._breadth = this._length;
           
        }

        public Rectangle(DataAccess dt,double length, double breadth)
        {
            this._dataAccess = dt;
            this._length = length;
            this._breadth = breadth;
           
        }

    
        public static List<Rectangle> Select(DataAccess dt)
        {
            var rectangles = new List<Rectangle>();
          
            string queryString = "SELECT length,breadth FROM dbo.rectangles";
            
            SqlDataReader reader = null;
            try
            {
                reader =  dt.Select(queryString);
                while(reader.Read())
                {
                    rectangles.Add(new Rectangle(dt,reader.GetDouble(0),reader.GetDouble(1)){
                        State = ObjectState.Unchanged,
                        prevLength = reader.GetDouble(0),
                        prevBreadth = reader.GetDouble(1),
                        });     
                }
            
            }
            finally
            {
                reader?.Close();
                
            }

            return rectangles;
            
        }


        internal override void Insert()
        {
            Dictionary<string,double> dict = new Dictionary<string,double>();
            string addRectangles = "INSERT INTO dbo.rectangles(length,breadth) VALUES(@length,@breadth)";
            dict.Add("@length",this._length);
            dict.Add("@breadth",this._breadth);
            _dataAccess.PerformOperation(addRectangles,dict);
        }

        internal override void Update()
        {
            Dictionary<string,double> dict = new Dictionary<string,double>();
            string updateRectangles = "UPDATE dbo.rectangles SET length=@newLength,breadth=@newBreadth WHERE length=@length AND breadth=@breadth";
            dict.Add("@newLength",_length);
            dict.Add("@length",prevLength);
            dict.Add("@newBreadth",_breadth);
            dict.Add("@breadth",prevBreadth);
            _dataAccess.PerformOperation(updateRectangles,dict);
        }


        internal  override void Delete()
        {
           Dictionary<string,double> dict = new Dictionary<string,double>();
            string deleteRectangles= "DELETE FROM dbo.rectangles WHERE length=@length AND breadth=@breadth";
            dict.Add("@length",_length); 
            dict.Add("@breadth",_breadth);   
           _dataAccess.PerformOperation(deleteRectangles,dict);
        }

        public override double Area()
        {

            double area  = _length * _breadth; 

            return area;
            
        }
        
        public override string ToString() 
        {
            if (_length == _breadth)
                return $"Length: {_length}";
            else
                return $"Length: {_length} Breadth: {_breadth}";
        }
    }
}
