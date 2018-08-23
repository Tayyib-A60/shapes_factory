using System;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace BoxOfShapes
{

    public class Triangle : Shape
    {
        private DataAccess _dataAccess;
        
        private double _base;
        private double _height;
        private double prevBase;
        private double prevHeight;
        
        public double Base{
            get{ return this._base;}
            set
            { 
               this._base = value;
               this.ChangeState(); 
            }
        }

        public double Height{
            get{ return this._height;}
            set
            { 
               this._height = value;
               this.ChangeState(); 
            }
        }

        public Triangle(DataAccess dt,double triangleBase, double height)
        {
            this._dataAccess = dt;
            this._base = triangleBase;
            this._height = height;
           
        }

    
        public static List<Triangle> Select(DataAccess dt)
        {
            var triangles = new List<Triangle>();
            
            string queryString = "SELECT base,height FROM dbo.triangles";
            
            SqlDataReader reader = null;
            try
            {
                reader =  dt.Select(queryString);
                while(reader.Read())
                {
                    triangles.Add(new Triangle(dt,reader.GetDouble(0),reader.GetDouble(1)){
                        State = ObjectState.Unchanged,
                        prevBase = reader.GetDouble(0),
                        prevHeight = reader.GetDouble(1)
                        });
                    
                    
                }
            
            }
            finally
            {
                reader?.Close();
                
            }

            return triangles;
            
        }


        internal override void Insert()
        {
            Dictionary<string,double> dict = new Dictionary<string,double>();
            string addTriangles = "INSERT INTO dbo.triangles(base,height) VALUES(@base,@height)";
            dict.Add("@base",this._base);
            dict.Add("@height",this._height);
            _dataAccess.PerformOperation(addTriangles,dict);
        }

        internal override void Update()
        {
            Dictionary<string,double> dict = new Dictionary<string,double>();
            string updateTriangles = "UPDATE dbo.triangles SET base=@newBase,height=@newHeight WHERE base=@base AND height=@height";
            dict.Add("@newBase",_base);
            dict.Add("@base",prevBase);
            dict.Add("@newHeight",_height);
            dict.Add("@height",prevHeight);
            _dataAccess.PerformOperation(updateTriangles,dict);
        }


        internal  override void Delete()
        {
            Dictionary<string,double> dict = new Dictionary<string,double>();
            string deleteTriangles = "DELETE FROM dbo.triangles WHERE base=@base";
            dict.Add("@base",_base);    
           _dataAccess.PerformOperation(deleteTriangles,dict);
        }

        public override double Area()
        {
            double area = (_height * _base)/2;
            return area;
        }

        public override string ToString() 
        {
            return $"Base: {_base} Height: {_height}";
        }
    }
}
