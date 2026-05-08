using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;

namespace CRUD_GROUP
{
    public class DatabaseWorker
    {
        DataTable dTable;
        SqlDataAdapter sqlDA;
        SqlCommand sqlCmd;
        SqlConnection sqlCon;
        string conStr;

        public DatabaseWorker()
        { // Paste the connection string here. Be sure to include double quotation marks (")!
            conStr = "Data Source=localhost,1433;Initial Catalog=Records;Persist Security Info=True;User ID=sa;Password=jer0me-LL()YD;Encrypt=False";
            sqlCon = new SqlConnection(conStr);
            sqlCon.Open();
        }

        ~DatabaseWorker()
        {
            try { sqlCon.Close(); } catch { }
        }

        public int ExecuteNonQuery(String sql)
        {
            sqlCmd = new SqlCommand(sql, sqlCon);
            return sqlCmd.ExecuteNonQuery();
        }

        public DataTable ExecuteQuery(String sql)
        {
            dTable = new DataTable();
            sqlDA = new SqlDataAdapter(sql, conStr);
            sqlDA.Fill(dTable);
            sqlDA.Dispose();
            return dTable;
        }
    }
}
