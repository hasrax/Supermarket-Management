using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Supermarket_2
{
    internal class AutoIncriment
    {  
        SqlConnection conn=new SqlConnection("Data Source=MAHIII;Initial Catalog=Supermarket;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        
        public string IDIncriment(string Query,string ColumnName)
        {
            conn.Open();
            string ID="0";

            cmd = new System.Data.SqlClient.SqlCommand(Query, conn);
            //SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                ID = Convert.ToString(dr[ColumnName]);
                ID = ID.Substring(1);
                double EID = Convert.ToInt32(ID) + 1;
                ID = Convert.ToString(EID);
            }
            conn.Close();

            return ID;

        }

         
        
    }
}
