using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class BaseADO
    {
        //protected SqlConnection _connection;
        protected readonly string _connstring;

        public BaseADO()
          {
            _connstring = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            //_connection = new SqlConnection(
            //    ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        }
    }


}
