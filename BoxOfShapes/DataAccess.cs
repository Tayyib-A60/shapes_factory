using System.Data.SqlClient;
using System.Collections.Generic;



namespace BoxOfShapes
{
    public class DataAccess
    {
        private string _connectionString;
        private  SqlConnection _sqlCon;
        private  SqlCommand _cmd;

        public DataAccess(string connectionString)
        {
            this._connectionString = connectionString;
        }

        private SqlConnection OpenConnection()
        {
            _sqlCon = new SqlConnection(_connectionString);
            _sqlCon.Open();
            return _sqlCon;
        }

        public  SqlDataReader Select(string queryString)
        {
            OpenConnection();
            _cmd = new SqlCommand(queryString,_sqlCon);
            SqlDataReader reader = _cmd.ExecuteReader();
            return reader;
        }
        public void PerformOperation(string queryString, Dictionary<string,double> parameters)
        {
            OpenConnection();
            _cmd = new SqlCommand(queryString,_sqlCon);
            foreach (KeyValuePair<string,double> parameter in parameters)
            {
                _cmd.Parameters.AddWithValue(parameter.Key,parameter.Value);
            }
            try
            { 
                int rowsAffected = _cmd.ExecuteNonQuery();   
            }
           finally
           {
               _sqlCon.Close();
           }
        }

        
    }
}
