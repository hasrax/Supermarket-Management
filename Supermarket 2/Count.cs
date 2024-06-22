using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarket_2
{
    internal class Count
    {

        Myconnection obj = new Myconnection();
        public float GetCount(string query)
        {
            obj.con.Open();
            obj.cmd = new System.Data.SqlClient.SqlCommand(query, obj.con);
            float count = Convert.ToSingle(obj.cmd.ExecuteScalar());
            obj.con.Close();
            return count;
        }
    }
}
