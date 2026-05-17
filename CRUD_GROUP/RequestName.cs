using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_GROUP
{
    public partial class RequestName : Form
    {
        public static string Execute(DatabaseWorker worker, string name)
        {
            RequestName x = new RequestName(worker, name);
            DialogResult g = x.ShowDialog();
            if (g == DialogResult.OK) return resultingName;
            else return string.Empty;
        }

        public static string resultingName = string.Empty;  

        readonly DatabaseWorker db;
        readonly string accountName;

        RequestName(DatabaseWorker worker, string accountName)
        {
            InitializeComponent();
            db = worker;
            this.accountName = accountName;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            resultingName = cboName.Text;
            DialogResult = DialogResult.OK;
        }

        private void CancelButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void RequestName_Load(object sender, EventArgs e)
        {
            cboName.Items.Clear();
            List<string> str = new List<string>();
            List<string> ptr = new List<string>();
            DataTable s = db.ExecuteQuery("SELECT CustomerName FROM RecordTable");
            DataTable d = db.ExecuteQuery("SELECT Username FROM AccountTBL");
            foreach (DataRow w in d.Rows)
            {
                ptr.Add(w["Username"].ToString());
            }

            foreach (DataRow x in s.Rows)
            {
                bool dd = false;
                string w = x["CustomerName"].ToString();
                foreach (string item in ptr)
                {
                    if (w == item || item == accountName)
                    {
                        dd = true;
                        break;
                    }
                }
                if (!dd) str.Add(w);
            }
            str.AddRange(ptr);
            str.Sort();
            cboName.Items.AddRange(str.ToArray());
            cboName.SelectedIndex = 0;
        }
    }
}
