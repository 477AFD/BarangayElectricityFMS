using GraphingTests;
using System;
using System.CodeDom;
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
using System.Drawing.Imaging;
using System.IO;
using System.Data.Common;

namespace CRUD_GROUP
{
    public partial class MainForm : Form
    {
        // Default (root) account
        KeyValuePair<string, string> adminAccount = new KeyValuePair<string, string>("admin", "TLoZ_B0TW32");
        // is admin
        readonly bool admin = false;
        // Account log-in
        KeyValuePair<string, string> account;
        // Database Worker
        readonly DatabaseWorker db;
        public MainForm(KeyValuePair<string, string> accountName)
        {
            InitializeComponent();
            // Hire a new database worker
            db = new DatabaseWorker();
            // Disable fields input by default (since it only shows when a user clicks Add/Edit)
            FieldsPanel.Enabled = false;
            FieldsPanel.Visible = false;
            // If it is a root
            if (accountName.Key == adminAccount.Key && accountName.Value == adminAccount.Value)
            {
                account = new KeyValuePair<string, string>(accountName.Key, accountName.Value);
                admin = true;
                UserLabel.Text = "Administrator";
            }
            else try // If it is not a root account
                {
                    // Check if a user exists
                    DataTable re = db.ExecuteQuery("SELECT Username FROM AccountTBL");
                    bool tr = false;
                    foreach (DataRow dr in re.Rows)
                    {
                        if (dr["Username"].ToString() == accountName.Key)
                        {
                            tr = true;
                            break;
                        }
                    }
                    if (!tr) throw new AccessViolationException("SYS:0014"); // SYS:0014 is the error code for Incorrect username or password, if the user does not exist
                    DataTable accountInfo = db.ExecuteQuery($"SELECT * FROM AccountTBL WHERE Username='{accountName.Key}'");
                    if (accountInfo != null)
                    {
                        Debug.WriteLine("Loading passwords");
                        string fpwd = accountInfo.Rows[0]["PasswordHash"].ToString();
                        string spwd = accountName.Value;
                        // Check password
                        if (fpwd != spwd) throw new AccessViolationException("SYS:0014"); // SYS:0014 is the error code for Incorrect username or password
                        else
                        {
                            Debug.WriteLine("Installing account");
                            // Install account to a KeyValuePair
                            account = new KeyValuePair<string, string>(accountName.Key, accountName.Value);
                            // Check if it is an admin account
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
                string sql = $"SELECT RatePerKWH, Price FROM RecordTable WHERE CustomerName = '{name}' ORDER BY RecordTimeIndex ASC";
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
            SearchComboBox.Items.Clear();
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
            // TODO: This line of code loads data into the 'recordsDataSet2.RecordTable' table. You can move, or remove it, as needed.
            this.recordTableTableAdapter3.Fill(this.recordsDataSet2.RecordTable);
            // TODO: This line of code loads data into the 'recordList.RecordTable' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'recordsDataSet1.RecordTable' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'recordsDataSet.RecordTable' table. You can move, or remove it, as needed.
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
                infoToolStripMenuItem.Visible = false;
                infoToolStripMenuItem.Enabled = false;
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

        private int GetLastIndex()
        {
            int lastID = 0;
            DataTable e = db.ExecuteQuery("SELECT RecordTimeIndex FROM RecordTable ORDER BY RecordTimeIndex ASC");
            for (int i = 0; i < e.Rows.Count; i++)
            {
                int r = int.Parse(e.Rows[i]["RecordTimeIndex"].ToString());
                if (lastID < r) lastID = r;
            }
            return lastID + 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(false, true)) return;
            string x = (admin) ? txtName.Text : account.Key;
            decimal pricePerKwh = decimal.Parse(txtRate.Text);
            decimal a = decimal.Parse(txtPrev.Text);
            decimal b = decimal.Parse(txtCurrent.Text);
            decimal price = (b - a) * pricePerKwh;
            int idx = GetLastIndex();
            string dt = DateTime.Now.ToString("yyyy-MM-dd");
            int r = PaidCheckBox.Checked ? 1 : 0;
            int id = GetLastID();
            string sql = "INSERT INTO RecordTable VALUES (" + id +
                ", '" + x +
                "', '" + a + "', '" + b + "', "+  pricePerKwh +
                 ", " + price + ", '" + txtReceipt.Text + "', "+r+", "+idx+", '"+dt+"')";
            Debug.WriteLine(sql);
            try
            {
                int t = db.ExecuteNonQuery(sql);
                if (t > 0)
                {
                    ClearData();
                    DisplayData();
                    MessageBox.Show("Record has been saved.", $"Affected rows: {t}",
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

        private bool ValidateInput(bool delete, bool add)
        {
            bool[] ftd = new bool[6];
            ftd[0] = !admin || add || uint.TryParse(txtID.Text, out _) && !string.IsNullOrWhiteSpace(txtID.Text);
            ftd[1] = !admin || delete || !string.IsNullOrWhiteSpace(txtName.Text);
            ftd[2] = delete || uint.TryParse(txtPrev.Text, out _);
            ftd[3] = delete || uint.TryParse(txtCurrent.Text, out _);
            ftd[4] = delete || decimal.TryParse(txtRate.Text, out decimal f) && f >= 0;
            ftd[5] = delete || !string.IsNullOrWhiteSpace(txtReceipt.Text);
            string wpetoro = "";
            wpetoro += (!ftd[0]) ? "- The ID is not specified or it is not a positive integer\n" : "";
            wpetoro += (!ftd[1]) ? "- The name is not specified\n" : "";
            wpetoro += (!ftd[2]) ? "- The Previous kWh is not in a decimal format or it is negative\n" : "";
            wpetoro += (!ftd[3]) ? "- The Current kWh is not in a decimal format or it is negative\n" : "";
            wpetoro += (!ftd[4]) ? "- The rate per kWh was not in a decimal format or it is negative\n" : "";
            wpetoro += (!ftd[5]) ? "- The receipt number is not specified\n" : "";
            if (ftd[0] && ftd[1] && ftd[2] && ftd[3] && ftd[4] && ftd[5]) return true;
            else
            {
                MessageBox.Show($"Please correct any mistakes in the fields.\n{wpetoro}", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e) // Admins Only!
        {
            if (admin)
            {
                if (!ValidateInput(false, false)) return;
                decimal pricePerKwh = decimal.Parse(txtRate.Text);
                decimal a = decimal.Parse(txtPrev.Text);
                decimal b = decimal.Parse(txtCurrent.Text);
                decimal price = (b - a) * pricePerKwh;
                int r = (PaidCheckBox.Checked ? 1 : 0);
                string sql = "UPDATE RecordTable SET CustomerName='" + txtName.Text +
                    "',PreviousKWH=" + a +
                    ", CurrentKWH=" + b +
                    ", RatePerKWH=" + pricePerKwh +
                    ", Price=" + price +
                    ", ReceiptNo='" + txtReceipt.Text + 
                    "', Paid=" + r +
                    " WHERE ID ='" + txtID.Text + "'";

                int t = db.ExecuteNonQuery(sql);
                if (t > 0)
                {
                    ClearData();
                    DisplayData();
                    MessageBox.Show("Record has been updated.", $"Affected rows: {t}",
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
                    MessageBox.Show("An error has been occured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ClearData();
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
                if (!ValidateInput(true, false)) return;
                int f = int.Parse(txtID.Text);
                DialogResult r = MessageBox.Show("Are you sure you want to delete this ID?\n\nThis cannot be undone!", $"CAUTION: DELETE ID {f}", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    string sql = $"DELETE FROM RecordTable WHERE ID={f}";
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
                MessageBox.Show("Whoops! This box is reserved only for sysadmins!" +
                    "\n\nTry logging in as administrator and try again.","Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            if (admin) {
                NewAccount t = new NewAccount(db);
                Hide();
                DialogResult f = t.ShowDialog();
                Show();
                if (f == DialogResult.OK)
                {
                    FillSearch();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Image f = null;
            switch (GraphTabControl.SelectedIndex)
            {
                case 0:
                    // Save price graph
                    p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250, true);
                    Task.Delay(100);
                    f = picGraph.Image;
                    Task.Delay(100);
                    p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                    break;
                case 1:
                    p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraph.Width, picGraph.Height), 1, 1, true);
                    Task.Delay(100);
                    f = picGraphPricePerKwh.Image;
                    Task.Delay(100);
                    p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraph.Width, picGraph.Height), 1, 1);
                    break;
                default:
                    return;
            }
            SaveFileDialog t = new SaveFileDialog()
            {
                Title = "Save graph image as:",
                Filter = "Portable Network Graphics (.png)|*.png|JPEG Image (.jpg, .jpeg, .jpe, .jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|Bitmap image (.bmp)|*.bmp|All files|*.*",
            };
            if (t.ShowDialog() == DialogResult.OK)
            {
                string ee = t.FileName;
                if (File.Exists(ee))
                {
                    DialogResult d = MessageBox.Show($"Replace this file?\n{ee}","Replace",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (d != DialogResult.Yes) return;
                }
                if ((ee.Contains(".jpg") && ee.IndexOf(".jpg") == ee.Length - 4) || (ee.Contains(".jpeg") && ee.IndexOf(".jpeg") == ee.Length - 5) || (ee.Contains(".jpe") && ee.IndexOf(".jpe") == ee.Length - 5) || (ee.Contains(".jfif") && ee.IndexOf(".jfif") == ee.Length - 5))
                {
                    f.Save(ee, ImageFormat.Jpeg);
                    Open(ee);
                }
                else if (ee.Contains(".png") && ee.IndexOf(".png") == ee.Length - 4)
                {
                    f.Save(ee, ImageFormat.Png); Open(ee);
                }
                else if (ee.Contains(".bmp") && ee.IndexOf(".bmp") == ee.Length - 4)
                {
                    f.Save(ee, ImageFormat.Bmp); Open(ee);
                }
                else if (ee.Contains(".tiff") && ee.IndexOf(".tiff") == ee.Length - 5)
                {
                    f.Save(ee, ImageFormat.Tiff); Open(ee);
                }
                else if (ee.Contains(".gif") && ee.IndexOf(".gif") == ee.Length - 4)
                {
                    f.Save(ee, ImageFormat.Gif); Open(ee);
                }
                else if (ee.Contains(".wmf") && ee.IndexOf(".wmf") == ee.Length - 4)
                {
                    f.Save(ee, ImageFormat.Wmf); Open(ee);
                }
                else MessageBox.Show("Invalid file format.\n\nSupported file formats:\n" +
                    "- JPEG (.jpg/.jpeg/.jpe/.jfif)\n" +
                    "- PNG (.png)\n" +
                    "- Bitmap (.bmp)\n" +
                    "- TIFF (.tiff)\n" +
                    "- GIF (.gif)\n" +
                    "- WMF (.wmf)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            if (!admin)
            {
                p.DrawLineExt(picGraph, 1, Color.Blue, new Point(1920, 1080), 1, 250, false, null, false, true);
                p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(1920, 1080), 1, 1, false, null, false, true);
                Bitmap rt = (Bitmap)picGraph.Image;
                Bitmap pt = (Bitmap)picGraphPricePerKwh.Image;
                p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraphPricePerKwh.Width, picGraphPricePerKwh.Height), 1, 1);
                string uname = GetName();
                string dname = "";
                DataTable users = db.ExecuteQuery($"SELECT FullName FROM AccountTBL WHERE Username='{uname}'");
                dname = users.Rows[0]["FullName"].ToString() ?? "Unknown Name";
                DataTable tbl = db.ExecuteQuery($"SELECT PreviousKWH, CurrentKWH, RatePerKWH, Price, ReceiptNo, DateCreated FROM RecordTable WHERE CustomerName='{uname}' ORDER BY RecordTimeIndex ASC");
                PrintWorker d = new PrintWorker(rt, pt, tbl, dname);
                d.Print();
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Draw();
        }
        /// <summary>
        /// This is for making changes to the size of the graph when performing window commands to prevent distortions.
        /// </summary>
        /// <param name="m">A message pointer that Windows executes. This contains commands for window resize, move, maximize, minimize, restore, and Alt key press.</param>
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
                            p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraphPricePerKwh.Width, picGraphPricePerKwh.Height), 1, 1);
                        }
                        
                        break;
                    case 0xF120: // Restore
                        Console.WriteLine("[GUI] Window restored");
                        //Program.d.DrawLine(f, new Point(f.Width, f.Height));
                        if (!admin)
                        {
                            p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                            p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraphPricePerKwh.Width, picGraphPricePerKwh.Height), 1, 1);
                        }
                        break;
                    case 0xF010: // Drag
                        Console.WriteLine("[GUI] Window dragged");
                        //Program.d.DrawLine(f, new Point(f.Width, f.Height));
                        if (!admin)
                        {
                            p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                            p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraphPricePerKwh.Width, picGraphPricePerKwh.Height), 1, 1);
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
                            p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraphPricePerKwh.Width, picGraphPricePerKwh.Height), 1, 1);
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

        private void picGraph_Click(object sender, EventArgs e)
        {

        }

        private void picGraphPricePerKwh_Click(object sender, EventArgs e)
        {

        }

        private void ClickedImage(int index, Point point, bool dc)
        {
            string name = GetName();
            DataTable ds = db.ExecuteQuery($"SELECT DateCreated, PreviousKWH, CurrentKWH, RatePerKWH, Price FROM RecordTable WHERE CustomerName = '{name}' ORDER BY RecordTimeIndex ASC");
            if (index == 0)
            {
                p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250, false, point, dc);
                int idx = p.ClickedIndex;
                if (idx > -1)
                {
                    DataRow dr = ds.Rows[idx];
                    string date = "Unknown";
                    if (!string.IsNullOrWhiteSpace(dr["DateCreated"].ToString())) date = dr["DateCreated"].ToString();
                    string dt = $"Details:\n" +
                        $"Date: {date}\n" +
                        $"kWh consumed: {int.Parse(dr["CurrentKWH"].ToString()) - int.Parse(dr["PreviousKWH"].ToString())} kWh ({dr["CurrentKWH"]} kWh - {dr["PreviousKWH"]} kWh)\n" +
                        $"Price rate per kWh: {dr["RatePerKWH"]:C}\n" +
                        $"Price: {dr["Price"]:C}";
                    GraphInfo.Text = dt;
                } else
                {
                    GraphInfo.Text = "Details:\nClick on the graph points to see its details.";
                }
            }
            if (index == 1)
            {
                p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraphPricePerKwh.Width, picGraphPricePerKwh.Height), 1, 1, false, point, dc);
                int idx = p.ClickedIndex;
                if (idx > -1)
                {
                    DataRow dr = ds.Rows[idx];
                    string dt = $"Details:\n" +
                        $"Date: {dr["DateCreated"]}\n" +
                        $"Price rate per kWh: {dr["RatePerKWH"]:C}\n";
                    GraphInfo.Text = dt;
                }
                else
                {
                    GraphInfo.Text = "Details:\nClick on the graph points to see its details.";
                }
            }

        }

        private void picGraph_MouseClick(object sender, MouseEventArgs e)
        {
            Point fclick = e.Location;
            ClickedImage(0, fclick, true);
        }

        private void picGraphPricePerKwh_MouseClick(object sender, MouseEventArgs e)
        {
            Point fclick = e.Location;
            ClickedImage(1, fclick, true);

        }

        private void picGraphPricePerKwh_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point fclick = e.Location;
            ClickedImage(0, fclick, true);

        }

        private void picGraph_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point fclick = e.Location; 
            ClickedImage(1, fclick, true);

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }
        private void Open(string path)
        {
            Process t = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = @"C:\Windows\Explorer.exe",
                    UseShellExecute = true,
                    Arguments = $"/select,{path}"
                }
            };
            t.Start();
        }
        private void SaveCSVRecord_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog()
            {
                Title = "Save as:",
                Filter = "CSV Files|*.csv"
            };
            if (s.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(s.FileName))
                {
                    DialogResult ed = MessageBox.Show($"Do you want to replace this file?\n\n{s.FileName}", "Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (ed != DialogResult.Yes) return;
                }
                string name = GetName();
                string csv = $"Name of Customer:,{name}\n\nDate,Previous kWh,Current kWh,kWh Consumed,Rate,Price";
                DataTable prt = db.ExecuteQuery($"SELECT * FROM RecordTable WHERE CustomerName = '{name}' ORDER BY RecordTimeIndex ASC");
                foreach (DataRow prow in prt.Rows)
                {
                    uint con = uint.Parse(prow["CurrentKWH"].ToString()) - uint.Parse(prow["PreviousKWH"].ToString());
                    string d = "Unknown Date";
                    if (!string.IsNullOrWhiteSpace(prow["DateCreated"].ToString())) d = prow["DateCreated"].ToString();
                    csv += $"\n{d},{prow["PreviousKWH"]},{prow["CurrentKWH"]},{con},{prow["RatePerKWH"]},{prow["Price"]}";
                }
                File.WriteAllText(s.FileName, csv);
                Open(s.FileName);
            }
        }
        bool ft = false;
        private void exitAltF4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ft) DialogResult = DialogResult.Cancel;
        }

        private void accountInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ft = true;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
