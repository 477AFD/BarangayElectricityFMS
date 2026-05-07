using GraphingTests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CRUD_GROUP
{
    public partial class MainForm : Form
    {
        KeyValuePair<string, string> adminAccount = new KeyValuePair<string, string>("admin", "TLoZ_B0TW32");
        readonly bool admin = false;
        KeyValuePair<string, string> account;
        readonly DatabaseWorker db;
        GraphWorker graph;
        public MainForm(KeyValuePair<string, string> accountName)
        {
            InitializeComponent();
            db = new DatabaseWorker();
            FieldsPanel.Enabled = false;
            FieldsPanel.Visible = false;
            if (accountName.Key == adminAccount.Key && accountName.Value == adminAccount.Value)
            {
                account = new KeyValuePair<string, string>(accountName.Key, accountName.Value);
                admin = true;
                UserLabel.Text = "Administrator";
            }
            else try
                {
                    DataTable accountInfo = db.ExecuteQuery($"SELECT * FROM AccountTBL WHERE Username='{accountName.Key}'");
                    if (accountInfo != null)
                    {
                        Debug.WriteLine("Loading passwords");
                        string fpwd = accountInfo.Rows[0]["PasswordHash"].ToString();
                        string spwd = accountName.Value;
                        if (fpwd != spwd) throw new AccessViolationException("SYS:0014");
                        else
                        {
                            Debug.WriteLine("Installing account");
                            account = new KeyValuePair<string, string>(accountName.Key, accountName.Value);
                            admin = int.Parse(accountInfo.Rows[0]["isAdmin"].ToString()) == 1;
                        }
                    }
                    else
                    {
                        throw new AccessViolationException("SYS:0001");
                    }
                    if (admin)
                    {
                        UserLabel.Text = "Administrator";
                    } else
                    {
                        UserLabel.Text = $"User: {account.Key}";
                        //CreateGraph();
                    }
                    Debug.WriteLine("Fill Search Operations");
                    FillSearch();
                    Draw();
                }
                catch (Exception ex)
                {
                    if (ex is AccessViolationException) throw;
                    else throw new Exception($"[Exception] SEEKER DETECTED:\n\n{ex}");
                }
        }
        GraphWorker p;
        public void Draw()
        {
            if (!admin)
            {
                string name = GetName();
                string sql = $"SELECT RatePerKWH, Price FROM RecordTable WHERE CustomerName = '{name}'";
                DataTable spill = db.ExecuteQuery(sql);
                p = GraphWorker.MakeGraphWorker(spill);
                p.DrawLineExt(picGraph, 1, Color.Blue, null, 1, 250);
                p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, null, 1, 1);
            }
        }

        public void DisplayData()
        {
            string name = (SearchComboBox.Text != "All" || !string.IsNullOrEmpty(SearchComboBox.Text)) ? GetName() : string.Empty;
            string sql;
            if (admin && !string.IsNullOrEmpty(name)) sql = $"SELECT * FROM RecordTable WHERE CustomerName = '{name}'";
            else if (admin) sql = "SELECT * FROM RecordTable";
            else sql = $"SELECT * FROM RecordTable WHERE CustomerName = '{name}'";
            if (chkUnpaid.Checked)
            {
                sql += (!string.IsNullOrEmpty(name)) ? " AND NOT Paid = 1" : " WHERE NOT Paid = 1";
            }
            Debug.WriteLine(sql);
            dgvRecords.DataSource = db.ExecuteQuery(sql);
        }

        private string GetName()
        {
            string name = SearchComboBox.Text;
            if (admin)
            {
                DataTable s = db.ExecuteQuery("SELECT CustomerName FROM RecordTable");
                foreach (DataRow e in s.Rows)
                {
                    string fname = e["CustomerName"].ToString();
                    if (fname.ToUpper() == name.ToUpper()) return fname;
                }
                return string.Empty;
            } else
            {
                return account.Key;
            }
        }

        private void FillSearch()
        {
            DataTable s = db.ExecuteQuery("SELECT CustomerName FROM RecordTable");
            DataTable d = db.ExecuteQuery("SELECT Username FROM AccountTBL");
            foreach (DataRow e in d.Rows)
            {
                SearchComboBox.Items.Add(e["Username"].ToString());
            }
            bool dd = false;
            foreach (DataRow e in s.Rows)
            {
                dd = false;
                string w = e["CustomerName"].ToString();
                ComboBox.ObjectCollection list = SearchComboBox.Items;
                foreach (string item in list) 
                {
                    if (w == item)
                    {
                        dd = true;
                        break;
                    }
                }
                if (!dd) SearchComboBox.Items.Add(w);
            }

        }

        public void ClearData()
        {
            txtName.Clear();
            txtPrev.Clear();
            txtCurrent.Clear();
            txtRate.Clear();
            txtReceipt.Clear();
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainControl.SelectedIndex == 1) {
                Draw();
            }
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0 || index >= dgvRecords.Rows.Count)
            {
                return;
            }
            DataGridViewRow selectedRows = dgvRecords.Rows[index];
            txtID.Text = selectedRows.Cells[0].Value.ToString();
            txtName.Text = selectedRows.Cells[1].Value.ToString();
            txtPrev.Text = selectedRows.Cells[2].Value.ToString();
            txtCurrent.Text = selectedRows.Cells[3].Value.ToString();
            txtRate.Text = selectedRows.Cells[4].Value.ToString();
            txtReceipt.Text = selectedRows.Cells[6].Value.ToString();
            PaidCheckBox.Checked = (int.Parse(selectedRows.Cells[7].Value.ToString()) == 1);
            isAdding = true;
            AddButton.Enabled = false;
            AddButton.Visible = false;
            dgvRecords.Visible = false;
            FieldsPanel.Enabled = true;
            FieldsPanel.Visible = true;
            chkUnpaid.Enabled = false;
            chkUnpaid.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'recordsDataSet1.RecordTable' table. You can move, or remove it, as needed.
            this.recordTableTableAdapter1.Fill(this.recordsDataSet1.RecordTable);
            // TODO: This line of code loads data into the 'recordsDataSet.RecordTable' table. You can move, or remove it, as needed.
            this.recordTableTableAdapter.Fill(this.recordsDataSet.RecordTable);
            Debug.WriteLine("Inserting data");
            DisplayData();
            if (!admin)
            {
                txtID.Enabled = false;
                lblID.Enabled = false;
                txtID.Visible = false;
                lblID.Visible = false;
                btnUpdate.Text = "Cancel";
                btnSave.Text = "New Record";
                SearchComboBox.Enabled = false;
                SearchComboBox.Visible = false;
                searchToolStripMenuItem.Enabled = false;
                searchToolStripMenuItem.Visible = false;
                lblName.Enabled = false;
                lblName.Visible = false;
                txtName.Visible = false;
                txtName.Enabled = false;
                CancelAddButton.Enabled = false;
                CancelAddButton.Visible = false;
                dgvRecords.Columns[7].Visible = false;
                dgvRecords.Columns[1].Visible = false;
                btnDelete.Enabled = false;
                btnDelete.Visible = false;
            } else
            {
                MainControl.TabPages.Remove(Tab2);
                graphToolStripMenuItem.Enabled = false;
                graphToolStripMenuItem.Visible = false;
                dgvRecords.Columns[0].Visible = true;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private int GetLastID()
        {
            int lastID = 0;
            DataTable e = db.ExecuteQuery("SELECT ID FROM RecordTable ORDER BY ID ASC");
            for (int i = 0; i < e.Rows.Count; i++)
            {
                if (lastID == int.Parse(e.Rows[i]["ID"].ToString())) lastID++;
                else break;
            }
            return lastID;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string x = (admin) ? txtName.Text : account.Key;
            decimal pricePerKwh = decimal.Parse(txtRate.Text);
            decimal a = decimal.Parse(txtPrev.Text);
            decimal b = decimal.Parse(txtCurrent.Text);
            decimal price = (b - a) * pricePerKwh;
            int r = PaidCheckBox.Checked ? 1 : 0;
            int id = GetLastID();
            string sql = "Insert into RecordTable Values (" + id +
                ", '" + x +
                "', '" + a + "', '" + b + "', "+  pricePerKwh +
                 ", " + price + ", '" + txtReceipt.Text + "', "+r+")";
            Debug.WriteLine(sql);
            try
            {
                if (db.ExecuteNonQuery(sql) > 0)
                {
                    ClearData();
                    DisplayData();
                    MessageBox.Show("Record has been saved.", "SAVED SUCCESS",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    isAdding = false;
                    AddButton.Enabled = true;
                    AddButton.Visible = true;
                    dgvRecords.Visible = true;
                    FieldsPanel.Enabled = false;
                    FieldsPanel.Visible = false;
                    chkUnpaid.Enabled = true;
                    chkUnpaid.Visible = true;
                }
                else
                {
                    MessageBox.Show("An error has been occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has been occurred.\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e) // Admins Only!
        {
            if (admin)
            {

                decimal pricePerKwh = decimal.Parse(txtRate.Text);
                decimal a = decimal.Parse(txtPrev.Text);
                decimal b = decimal.Parse(txtCurrent.Text);
                decimal price = (b - a) * pricePerKwh;
                int r = (PaidCheckBox.Checked ? 1 : 0);
                string sql = "Update RecordTable set CustomerName='" + txtName.Text +
                    "',PreviousKWH=" + a +
                    ", CurrentKWH=" + b +
                    ", RatePerKWH=" + pricePerKwh +
                    ", Price=" + price +
                    ", ReceiptNo='" + txtReceipt.Text + 
                    "', Paid=" + r +
                    " Where ID ='" + txtID.Text + "'";


                if (db.ExecuteNonQuery(sql) > 0)
                {
                    ClearData();
                    DisplayData();
                    MessageBox.Show("Record has been updated.", "UPDATE SUCCESS",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    isAdding = false;
                    AddButton.Enabled = true;
                    AddButton.Visible = true;
                    dgvRecords.Visible = true;
                    FieldsPanel.Enabled = false;
                    FieldsPanel.Visible = false;
                    chkUnpaid.Enabled = true;
                    chkUnpaid.Visible = true;
                }
                else
                {
                    MessageBox.Show("Error has been occured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                isAdding = false;
                AddButton.Enabled = true;
                AddButton.Visible = true;
                dgvRecords.Visible = true;
                FieldsPanel.Enabled = false;
                FieldsPanel.Visible = false;
                chkUnpaid.Enabled = true;
                chkUnpaid.Visible = true;
            }
        }
        bool isAdding = false;
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (isAdding) return;
            else
            {
                isAdding = true;
                AddButton.Enabled = false;
                AddButton.Visible = false;
                dgvRecords.Visible = false;
                FieldsPanel.Enabled = true;
                FieldsPanel.Visible = true;
                chkUnpaid.Enabled = false;
                chkUnpaid.Visible = false;
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isAdding)
            {
                isAdding = false;
                AddButton.Enabled = true;
                AddButton.Visible = true;
                dgvRecords.Visible = true;
                FieldsPanel.Enabled = false;
                FieldsPanel.Visible = false;
                chkUnpaid.Enabled = true;
                chkUnpaid.Visible = true;
            }
            DisplayData();
        }

        private void CancelAddButton_Click(object sender, EventArgs e)
        {
            isAdding = false;
            AddButton.Enabled = true;
            AddButton.Visible = true;
            dgvRecords.Visible = true;
            FieldsPanel.Enabled = false;
            FieldsPanel.Visible = false;
            chkUnpaid.Enabled = true;
            chkUnpaid.Visible = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (admin)
            {
                DialogResult r = MessageBox.Show("Are you sure you want to delete this ID?\n\nThis cannot be undone!", $"CAUTION: DELETE ID {txtID.Text}", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (r == DialogResult.OK)
                {
                    string sql = $"DELETE FROM RecordTable WHERE ID={txtID.Text}";
                    try
                    {
                        int s = db.ExecuteNonQuery(sql);
                        ClearData();
                        if (s > 0)
                        {
                            MessageBox.Show("Item deleted", $"Affected rows: {s}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DisplayData();
                        }
                        else
                        {
                            MessageBox.Show("There are no items to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Seeker detected: \n{ex}","Guru meditation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } else
            {
                MessageBox.Show("Whoops! Zelda cannot let you delete someone as a user.\n\nTry logging in as administrator and try again.","Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SearchComboBox_Click(object sender, EventArgs e)
        {

        }

        private void chkUnpaid_CheckedChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void PrintButton_Click(object sender, EventArgs e)
        {

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Draw();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x0112) // WM_SYSCOMMAND
            {
                int wparam = m.WParam.ToInt32() & 0xfff0;

                switch (wparam)
                {
                    case 0xF030: // Maximize
                        Console.WriteLine("[GUI] Window maximized");
                        //Program.d.DrawLine(f, new Point(f.Width, f.Height));
                        if (!admin) 
                        {
                            p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                            p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraph.Width, picGraph.Height), 1, 1);
                        }
                        
                        break;
                    case 0xF120: // Restore
                        Console.WriteLine("[GUI] Window restored");
                        //Program.d.DrawLine(f, new Point(f.Width, f.Height));
                        if (!admin)
                        {
                            p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                            p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraph.Width, picGraph.Height), 1, 1);
                        }
                        break;
                    case 0xF010: // Drag
                        Console.WriteLine("[GUI] Window dragged");
                        //Program.d.DrawLine(f, new Point(f.Width, f.Height));
                        if (!admin)
                        {
                            p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                            p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraph.Width, picGraph.Height), 1, 1);
                        }
                        break;
                    case 0xF060: // Close
                        Console.WriteLine("[GUI] Window closed");
                        break;
                    case 0xF020: // Minimize
                        Console.WriteLine("[GUI] Window minimized");
                        break;
                    case 0xF000: // Resize
                        Console.WriteLine("[GUI] Window resized");
                        //Program.d.DrawLine(f, new Point(f.Width, f.Height));
                        if (!admin)
                        {
                            p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                            p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraph.Width, picGraph.Height), 1, 1);
                        }
                        break;
                    case 0xF100: // Alt key
                        Console.WriteLine("[GUI] Alt key pressed");
                        break;
                    case 0xF170: // Sleep mode detected
                        Console.WriteLine("[GUI] System went to sleep mode");
                        break;
                    default:     // Seeker
                        Console.WriteLine($"[GUI] 0x{wparam:X4} is unknown!");
                        MessageBox.Show($"Sorry, a seeker has been detected.\n\n{wparam:X4} is not a valid WM_COMMAND message.\n\nCheck the code for errors!", "Guru meditation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else if (m.Msg == 0x0219)
            {
                switch ((int)m.WParam)
                {
                    case 0x8004:
                        // Device removed
                        Console.WriteLine("[GUI] A device has been disconnected.");
                        int devType = Marshal.ReadInt32(m.LParam, 4);
                        if (devType == 0x00000002) // DBT_DEVTYP_VOLUME
                        {
                            // Refresh volume list
                            Console.WriteLine("[SYSTEM] Storage lost connection, refreshing disk list...");
                            //RefreshDisk(true);
                        }
                        break;
                    case 0x8000:
                        // Device inserted
                        Console.WriteLine("[GUI] A device has been connected.");
                        DEV_BROADCAST_VOLUME vol = Marshal.PtrToStructure<DEV_BROADCAST_VOLUME>(m.LParam);
                        if (vol.dbcv_devicetype == 0x00000002)
                        {
                            // vol.dbcv_unitmask for drive letter
                            //string driveLetter = DriveMaskToLetter(vol.dbcv_unitmask);
                            //MessageBox.Show($"Drive {driveLetter} has been inserted.");
                            Console.WriteLine("[SYSTEM] Storage connection detected. Refreshing disk list...");
                            //RefreshDisk(false);
                        }
                        break;
                }
            }
            //Console.WriteLine($"[SYSTEM] Performing Win32 message {m.WParam}...");
        }
        public struct DEV_BROADCAST_VOLUME
        {
            public int dbcv_size;
            public int dbcv_devicetype;
            public int dbcv_reserved;
            public int dbcv_unitmask;
        }
    }
}
