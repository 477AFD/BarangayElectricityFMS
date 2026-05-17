using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_GROUP
{
    public partial class DisplayUnrecord : Form
    {
        DatabaseWorker db;
        List<string> id;
        List<KeyValuePair<decimal, decimal>> kilowatthour;
        List<decimal> ratePerKwh;
        List<string> name;
        public DisplayUnrecord(DatabaseWorker worker)
        {
            InitializeComponent();
            db = worker;
            id = new List<string>();
            kilowatthour = new List<KeyValuePair<decimal, decimal>>();
            ratePerKwh = new List<decimal>();
            name = new List<string>();
        }

        private void DisplayUnrecord_Load(object sender, EventArgs e)
        {
            MainList.Items.Clear();
            DataTable table = db.ExecuteQuery("SELECT * FROM UnrecordTable");
            if (table.Rows.Count < 1)
            {
                MessageBox.Show("There are no unclaimed reference IDs.", "No unclaimed IDs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            foreach (DataRow row in table.Rows)
            {
                // We just need the unclaimed ID and its corresponding name
                string dor = row["ReferenceID"].ToString();
                string fot = row["CustomerName"].ToString();
                dor += $" - {fot}";
                MainList.Items.Add(dor);
                id.Add(row["ReferenceID"].ToString());
                KeyValuePair<decimal, decimal> idx = new KeyValuePair<decimal, decimal>(decimal.Parse(row["PreviousKWH"].ToString()), decimal.Parse(row["CurrentKWH"].ToString()));
                kilowatthour.Add(idx);
                ratePerKwh.Add(decimal.Parse(row["RatePerKWH"].ToString()));
                name.Add(fot);
            }
            Clip.Text = string.Empty;
            MainList.SelectedIndex = 0;
        }

        [STAThread]
        public static void AppendToClipboard(string text)
        {
            Clipboard.SetText(text);
        }

        private void MainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idx = MainList.SelectedIndex;
                lstDetails.Items.Clear();
                ListViewItem ds = new ListViewItem("Reference ID");
                ds.SubItems.Add(id[idx]);
                lstDetails.Items.Add(ds);
                ListViewItem ds1 = new ListViewItem("Associated username");
                ds1.SubItems.Add(name[idx]);
                lstDetails.Items.Add(ds1);
                ListViewItem ds2 = new ListViewItem("Previous kWh");
                ds2.SubItems.Add($"{kilowatthour[idx].Key:N0}");
                lstDetails.Items.Add(ds2);
                ListViewItem ds3 = new ListViewItem("Current kWh");
                ds3.SubItems.Add($"{kilowatthour[idx].Value:N0}");
                lstDetails.Items.Add(ds3);
                ListViewItem ds4 = new ListViewItem("Price Rate per kWh");
                ds4.SubItems.Add($"{ratePerKwh[idx]:C}");
                lstDetails.Items.Add(ds4);
                ListViewItem ds5 = new ListViewItem("Price");
                ds5.SubItems.Add($"{(kilowatthour[idx].Value - kilowatthour[idx].Key) * ratePerKwh[idx]:C}");
                lstDetails.Items.Add(ds5);

            } catch
            {
                Clip.Text = string.Empty;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int idx = MainList.SelectedIndex;
                DialogResult re = MessageBox.Show($"Are you sure you want to delete this unclaimed ID?\n\n{id[idx]}", "Delete unclaimed record", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (re == DialogResult.Yes)
                {
                    int error = db.ExecuteNonQuery($"DELETE FROM UnrecordTable WHERE ReferenceID = '{id[idx]}'");
                    if (error != 1)
                    {
                        MessageBox.Show("An error has been occured.", "Rows not deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Unclaimed ID removed.", $"Affected rows: {error}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lstDetails.Items.Clear();
                        MainList.SelectedIndex = 0;
                        id = new List<string>();
                        kilowatthour = new List<KeyValuePair<decimal, decimal>>();
                        ratePerKwh = new List<decimal>();
                        name = new List<string>();
                        DisplayUnrecord_Load(sender, e);
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show($"An error has been occured.\n\n{ex}", "Seeker detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyID_Click(object sender, EventArgs e)
        {
            int idx = MainList.SelectedIndex;
            AppendToClipboard(id[idx]);
            Clip.Text = $"Copied: {id[idx]}";
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
