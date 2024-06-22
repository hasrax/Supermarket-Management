using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_2
{
    internal class Myconnection
    {
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlDataAdapter da;
        public DataTable dt;

        public Myconnection()
        {
            con=new SqlConnection("Data Source=M_HASRA\\SQLEXPRESS;Initial Catalog=Supermarket;Integrated Security=True");
        }
        public static string type;
    }
}
