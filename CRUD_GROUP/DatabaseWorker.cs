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
    internal class DatabaseWorker
    {
        DataTable dTable;
        SqlDataAdapter sqlDA;
        SqlCommand sqlCmd;
        SqlConnection sqlCon;
        string conStr;

        public DatabaseWorker()
        {
            conStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Kersti\\source\\repos\\CRUD_GROUP\\CRUD_GROUP\\Records.mdf;Integrated Security=True";
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
