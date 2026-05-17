using GraphingTests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace CRUD_GROUP
{
    public partial class MainForm : Form
    {
        /*
         NOTE:
         You may see #region and #endregion macros.
         Those are macros for the text editor in the integrated development
         environment for .NET and Visual Studio to organize methods and fields.
         Those do not affect the operation of the program. But deleting 
         #endregion without its corresponding #region <name> will cause a compilation
         error.
        */

        // These are the variables.
        #region Variables
        // Default (root) account
        KeyValuePair<string, string> adminAccount = new KeyValuePair<string, string>("admin", "TLoZ_B0TW32");
        // is admin
        readonly int admin = 0;
        // Account log-in
        KeyValuePair<string, string> account;
        // Database Worker
        readonly DatabaseWorker db;
        // Graph Worker
        GraphWorker p;
        // Is it actually in adding or editing state?
        bool isAdding = false;
        // Exit bool variable
        bool ft = false;
        #endregion

        // This is the class constructor. This part checks for the password.
        #region Constructor
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
                admin = 1;
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
                            admin = int.Parse(accountInfo.Rows[0]["isAdmin"].ToString());

                        }
                    }
                    else
                    {
                        throw new AccessViolationException("SYS:0001");
                    }
                    if (admin == 1)
                    {
                        UserLabel.Text = "Administrator";
                    } else if (admin == 2)
                    {
                        UserLabel.Text = "Secretary";
                    } else
                    {
                        UserLabel.Text = $"{account.Key}";
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
        #endregion

        // This creates a graph worker object.
        #region Graph Worker Creator
        public void Draw()
        {
            if (admin == 0)
            {
                string name = GetName();
                string sql = $"SELECT RatePerKWH, Price FROM RecordTable WHERE CustomerName = '{name}' ORDER BY RecordTimeIndex ASC";
                DataTable spill = db.ExecuteQuery(sql);
                p = GraphWorker.MakeGraphWorker(spill);
                p.GraphType = 1;
                p.DrawLineExt(picGraph, 1, Color.Blue, null, 1, 250);
                p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, null, 1, 1);
            }
        }
        #endregion

        // This displays/updates the DataGridView.
        #region Display data to DataGridView
        public void DisplayData()
        {
            string name = (SearchComboBox.Text != "[ALL]" || !string.IsNullOrEmpty(SearchComboBox.Text)) ? SearchComboBox.Text : string.Empty;
            string sql;
            if (admin == 0) name = GetName();
            //AND NOT CustomerName = '{account.Key}'
            if (admin != 0 && SearchComboBox.Text != "[ALL]") sql = $"SELECT * FROM RecordTable WHERE CustomerName LIKE '%{name}%' COLLATE SQL_Latin1_General_CP1_CI_AS";
            else if (admin != 0 && SearchComboBox.Text == "[ALL]") sql = "SELECT * FROM RecordTable";
            else sql = $"SELECT * FROM RecordTable WHERE CustomerName = '{name}'";
            if (chkUnpaid.Checked)
            {
                sql += (!string.IsNullOrEmpty(name)) ? " AND NOT Paid = 1" : " WHERE NOT Paid = 1";
            }
            Debug.WriteLine(sql);
            dgvRecords.DataSource = db.ExecuteQuery(sql);
        }
        #endregion

        // This gets the name of the current account.
        #region Get name of user
        private string GetName()
        {
            string name = SearchComboBox.Text;
            if (admin >= 1)
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
        #endregion

        // This adds/updates the search list.
        #region Add items to search bar
        private void FillSearch()
        {
            SearchComboBox.Items.Clear();
            List<string> str = new List<string>();
            List<string> ptr = new List<string>();
            DataTable s = db.ExecuteQuery("SELECT CustomerName FROM RecordTable");
            DataTable d = db.ExecuteQuery("SELECT Username FROM AccountTBL");
            foreach (DataRow e in d.Rows)
            {
                ptr.Add(e["Username"].ToString());
            }

            foreach (DataRow e in s.Rows)
            {
                bool dd = false;
                string w = e["CustomerName"].ToString();
                foreach (string item in ptr) 
                {
                    if (w == item || item == account.Key)
                    {
                        dd = true;
                        break;
                    }
                }
                if (!dd) str.Add(w);
            }
            str.AddRange(ptr);
            str.Sort();
            SearchComboBox.Items.Add("[ALL]");
            SearchComboBox.Items.AddRange(str.ToArray());
            //SearchComboBox.Items.Sort();
        }
        #endregion

        // This clears the input fields.
        #region Clear fields
        public void ClearData()
        {
            txtName.Clear();
            txtPrev.Clear();
            txtCurrent.Clear();
            txtRate.Clear();
            txtReceipt.Clear();
        }
        #endregion

        // This event draws the graph when the user clicks the Graph tab.
        #region Draw graph when selecting "Graph" in tab control
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainControl.SelectedIndex == 1) {
                Draw();
            }
        }
        #endregion

        // This event occurs when one clicks a cell in DataGridView
        #region Copy data to fields when an item is clicked then redirect user to Add/Edit page
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0 || index >= dgvRecords.Rows.Count || admin == 0)
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
            txtName.Enabled = false;
            if (admin > 0)
            {
                chkUnpaid.Enabled = false;
                chkUnpaid.Visible = false;
                txtPrev.Enabled = true;
                txtPrev.Visible = true;
                chkUnpaid.Enabled = false;
                chkUnpaid.Visible = false;
                btnUnrecord.Enabled = false;
                btnUnrecord.Visible = false;
            }
        }
        #endregion

        // This function adds a dark effect to all elements in MainForm when a dialog is open
        #region Apply blur toggle

        Form overlay;
        private void ToggleDark(bool on)
        {   
            if (on)
            {
                overlay = new Form
                {
                    FormBorderStyle = FormBorderStyle.None,
                    BackColor = Color.Black,
                    Opacity = 0.50,
                    ShowInTaskbar = false,
                    StartPosition = FormStartPosition.Manual,
                    Bounds = Bounds,
                    Owner = this
                };
                overlay.Show();
            } else
            {
                Activate();
                Focus();
                overlay.Hide();
                overlay.Close();
                overlay.Dispose();
            }
        }
        #endregion

        // This event occurs when the form loads.
        #region Apply scope and limitations to the form depending on account
        private void MainForm_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("Inserting data");
            DisplayData();
            // User
            switch (admin)
            {
                case 0:
                    txtID.Enabled = false;
                    lblID.Enabled = false;
                    txtID.Visible = false;
                    lblID.Visible = false;
                    btnUpdate.Text = "Cancel";
                    btnSave.Text = "Append";
                    SearchComboBox.Enabled = false;
                    SearchComboBox.Visible = false;
                    searchToolStripMenuItem.Enabled = false;
                    searchToolStripMenuItem.Visible = false;
                    lblName.Enabled = false;
                    lblName.Visible = false;
                    lblCurrent.Enabled = false;
                    lblCurrent.Visible = false;
                    lblPrev.Enabled = false;
                    lblPrev.Visible = false;
                    lblRate.Enabled = false;
                    lblRate.Visible = false;
                    txtName.Visible = false;
                    txtName.Enabled = false;
                    btnUnrecord.Enabled = false;
                    btnUnrecord.Visible = false;
                    txtPrev.Enabled= false;
                    txtPrev.Visible = false;
                    txtCurrent.Enabled = false;
                    txtCurrent.Visible = false;
                    txtRate.Enabled = false;
                    txtRate.Visible = false;
                    CancelAddButton.Enabled = false;
                    CancelAddButton.Visible = false;
                    dgvRecords.Columns[7].Visible = false;
                    dgvRecords.Columns[1].Visible = false;
                    btnDelete.Enabled = false;
                    btnDelete.Visible = false;
                    txtReceipt.Location = new Point(157, 30);
                    lblRef.Location = new Point(32, 30);
                    txtReceipt.Size = new Size(640, 22);
                    infoToolStripMenuItem.Visible = false;
                    infoToolStripMenuItem.Enabled = false;
                    chkUnpaid.Enabled = false;
                    chkUnpaid.Visible = false;
                    PaidCheckBox.Enabled = false;
                    PaidCheckBox.Visible = false;
                    UnrecordCheckBox.Enabled = false;
                    UnrecordCheckBox.Visible = false;
                    break;
                case 1:
                    MainControl.TabPages.Remove(Tab2);
                    graphToolStripMenuItem.Enabled = false;
                    graphToolStripMenuItem.Visible = false;
                    dgvRecords.Columns[0].Visible = true;
                    break;
                case 2:
                    MainControl.TabPages.Remove(Tab2);
                    graphToolStripMenuItem.Enabled = false;
                    graphToolStripMenuItem.Visible = false;
                    dgvRecords.Columns[0].Visible = true;
                    PaidCheckBox.Enabled = false;
                    PaidCheckBox.Visible = false;
                    btnDelete.Enabled = false;
                    btnDelete.Visible = false;
                    btnUpdate.Text = "Cancel";
                    btnSave.Text = "New Record";
                    CancelAddButton.Enabled = false;
                    CancelAddButton.Visible = false;
                    infoToolStripMenuItem.Visible = false;
                    infoToolStripMenuItem.Enabled = false;
                    UnrecordCheckBox.Enabled = false;
                    UnrecordCheckBox.Visible = false;
                    break;
            }
        }
        #endregion

        // This gets the next usable ID.
        #region Get last unused ID of list
        private int GetLastID(bool unrecord = false)
        {
            int lastID = 0;
            DataTable e = (unrecord) ? db.ExecuteQuery("SELECT ID FROM UnrecordTable ORDER BY ID ASC") : db.ExecuteQuery("SELECT ID FROM RecordTable ORDER BY ID ASC");
            for (int i = 0; i < e.Rows.Count; i++)
            {
                if (lastID == int.Parse(e.Rows[i]["ID"].ToString())) lastID++;
                else break;
            }
            return lastID;
        }
        #endregion

        // This gets the next chronological index.
        #region Get the uppermost index for time
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
        #endregion

        // This event occurs when the user clicks the Create/New Record button.
        #region Create new row with specified data
        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(false, true)) return;
            if (admin == 0)
            {
                AddButtonUsr_Click(sender, e);
                return;
            }
            string x = (admin != 0) ? txtName.Text : account.Key;
            decimal pricePerKwh = decimal.Parse(txtRate.Text);
            decimal a = decimal.Parse(txtPrev.Text);
            decimal b = decimal.Parse(txtCurrent.Text);
            int id = GetLastID(admin == 2 || UnrecordCheckBox.Checked);
            int idx = GetLastIndex();
            decimal c = (b - a) * pricePerKwh;
            string dt = DateTime.Now.ToString("yyyy-MM-dd");
            DataFormat fa = new DataFormat(x, a, b, pricePerKwh, txtReceipt.Text);
            int d = (admin == 1) ? (PaidCheckBox.Checked) ? 1 : 0 : 0;
            string sql = (admin == 2 || UnrecordCheckBox.Checked) ? $"INSERT INTO UnrecordTable VALUES ({id}, '{x}', {a}, {b}, {pricePerKwh}, '{txtReceipt.Text}')" : $"INSERT INTO RecordTable VALUES ({id},'{x}',{a}, {b}, {pricePerKwh}, {c}, '{txtReceipt.Text}', {d}, {idx}, '{dt}')";
            Debug.WriteLine(sql);
            DataTable usr = db.ExecuteQuery($"SELECT * FROM AccountTBL WHERE Username = '{x}'");
            if (usr.Rows.Count < 1)
            {
                NewAccountWithDataUser fuse = new NewAccountWithDataUser(db, fa);
                DialogResult res = fuse.ShowDialog();
                if (res != DialogResult.OK) return;
            }
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
                    if (admin >= 1)
                    {
                        chkUnpaid.Enabled = true;
                        chkUnpaid.Visible = true;
                        btnUnrecord.Enabled = true;
                        btnUnrecord.Visible = true;
                    }
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
        #endregion

        // This checks for all fields before performing the CRUD.
        #region Validate fields before confirmation
        private bool ValidateInput(bool delete, bool add)
        {
            bool[] ftd = new bool[6];
            ftd[0] = admin == 0 || add || uint.TryParse(txtID.Text, out _) && !string.IsNullOrWhiteSpace(txtID.Text);
            ftd[1] = admin == 0 || delete || !string.IsNullOrWhiteSpace(txtName.Text);
            ftd[2] = admin == 0 || delete || uint.TryParse(txtPrev.Text, out _);
            ftd[3] = admin == 0 || delete || uint.TryParse(txtCurrent.Text, out _);
            ftd[4] = admin == 0 || delete || decimal.TryParse(txtRate.Text, out decimal f) && f >= 0;
            ftd[5] = delete || !string.IsNullOrWhiteSpace(txtReceipt.Text);
            string wpetoro = "";
            wpetoro += (!ftd[0]) ? "- The ID is not specified or it is not a positive integer\n" : "";
            wpetoro += (!ftd[1]) ? "- The name is not specified\n" : "";
            wpetoro += (!ftd[2]) ? "- The Previous kWh is not in a decimal format or it is negative\n" : "";
            wpetoro += (!ftd[3]) ? "- The Current kWh is not in a decimal format or it is negative\n" : "";
            wpetoro += (!ftd[4]) ? "- The rate per kWh was not in a decimal format or it is negative\n" : "";
            wpetoro += (!ftd[5]) ? "- The reference ID is not specified\n" : "";
            if (ftd[0] && ftd[1] && ftd[2] && ftd[3] && ftd[4] && ftd[5]) return true;
            else
            {
                MessageBox.Show($"Please correct any mistakes in the fields.\n{wpetoro}", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        // This updates the fields in a row when a user clicks "Update".
        #region Update fields in a row
        private void btnUpdate_Click(object sender, EventArgs e) // Admins Only!
        {
            if (admin == 1)
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
                    if (admin >= 1)
                    {
                        chkUnpaid.Enabled = true;
                        chkUnpaid.Visible = true; 
                        btnUnrecord.Enabled = true;
                        btnUnrecord.Visible = true;
                    }
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
                if (admin >= 1)
                {
                    chkUnpaid.Enabled = true;
                    chkUnpaid.Visible = true;
                    btnUnrecord.Enabled = true;
                    btnUnrecord.Visible = true;
                }
            }
        }
        #endregion

        // This event happens when the user clicks the Add/Edit page.
        #region Access add/edit page
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (isAdding) return;
            else
            {
                ClearData();

                string name;
                bool toggled = false;
                if (admin > 0)
                {
                    ToggleDark(true);
                    toggled = true;
                    name = RequestName.Execute(db, account.Key);
                } else
                {
                    name = GetName();
                }
                if (string.IsNullOrWhiteSpace(name))
                {
                    if (toggled) { ToggleDark(false); toggled = false; }
                    return;
                }
                DataTable re = db.ExecuteQuery("SELECT Username FROM AccountTBL");
                bool tr = false;
                foreach (DataRow dr in re.Rows)
                {
                    if (dr["Username"].ToString() == name)
                    {
                        tr = true;
                        break;
                    }
                }
                if (!tr)
                {   
                    DialogResult f = MessageBox.Show($"There is no account associated with username \"{name}\".\nCreate account now?", "No associated account", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                    if (f == DialogResult.Yes)
                    {
                        DataFormat r = new DataFormat(name, 0, 0, 0, "");
                        NewAccountWithDataUser g = new NewAccountWithDataUser(db, r);
                        if (g.ShowDialog() != DialogResult.OK) { if (toggled) { ToggleDark(false); toggled = false; } return; }
                        if (toggled) { ToggleDark(false); toggled = false; }
                    } else
                    {
                        if (toggled) { ToggleDark(false); toggled = false; }
                        return;
                    }
                }
                if (toggled) { ToggleDark(false); toggled = false; }
                int idx = -1;
                DataTable ee = db.ExecuteQuery($"SELECT ID FROM RecordTable WHERE CustomerName = '{name}' ORDER BY ID ASC");
                for (int i = 0; i < ee.Rows.Count; i++)
                {
                    idx = int.Parse(ee.Rows[i]["ID"].ToString());
                }
                string gtx;
                if (idx != -1)
                {
                    DataTable rtx = db.ExecuteQuery($"SELECT CurrentKWH FROM RecordTable WHERE ID = {idx}");
                    gtx = rtx.Rows[0]["CurrentKWH"].ToString();
                } else
                {
                    gtx = "0";
                }
                isAdding = true;
                AddButton.Enabled = false;
                AddButton.Visible = false;
                dgvRecords.Visible = false;
                FieldsPanel.Enabled = true;
                FieldsPanel.Visible = true;
                txtPrev.Text = gtx;
                txtPrev.Enabled = false;
                txtName.Text = name;
                txtName.Enabled = false;
                if (admin >= 1)
                {
                    chkUnpaid.Enabled = false;
                    chkUnpaid.Visible = false;
                    btnUnrecord.Enabled = false;
                    btnUnrecord.Visible = false;
                }
            }
        }
        #endregion

        // This displays the unclaimed data. Only for secretary and sysadmins!
        #region Display unrecorded data
        private void btnUnrecord_Click(object sender, EventArgs e)
        {
            DisplayUnrecord dg = new DisplayUnrecord(db);
            ToggleDark(true);
            dg.ShowDialog(this);
            ToggleDark(false);
            dg.Dispose();

        }
        #endregion

        // This is the Add new row for users (based on the ReferenceID)
        #region Add new row as a user by ReferenceID
        private void AddButtonUsr_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(false, true)) return;
            else
            {
                string currentName = GetName();
                string fid = txtReceipt.Text; // Previously this is for the receipt, but this now refers to the Reference ID in the receipt
                DataTable raw = db.ExecuteQuery($"SELECT * FROM UnrecordTable WHERE ReferenceID = '{fid}' AND CustomerName = '{currentName}'");
                if (raw.Rows.Count != 1)
                {
                    MessageBox.Show("The reference ID is invalid.","Invalid",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                } else
                {
                    string x = (admin != 0) ? txtName.Text : account.Key;
                    decimal prevKWH = decimal.Parse(raw.Rows[0]["PreviousKWH"].ToString());
                    decimal nextKWH = decimal.Parse(raw.Rows[0]["CurrentKWH"].ToString());
                    decimal rate = decimal.Parse(raw.Rows[0]["RatePerKWH"].ToString());
                    decimal price = (nextKWH - prevKWH) * rate;
                    int idx = GetLastIndex();
                    string dt = DateTime.Now.ToString("yyyy-MM-dd");
                    int id = GetLastID();
                    string input = $"INSERT INTO RecordTable VALUES ({id}, '{x}',{prevKWH},{nextKWH},{rate},{price},'{fid}',0,{idx},'{dt}')";
                    try
                    {
                        int rows = db.ExecuteNonQuery(input);
                        if (rows > 0)
                        {
                            ClearData();
                            db.ExecuteNonQuery($"DELETE FROM UnrecordTable WHERE ID = {raw.Rows[0]["ID"]}");
                            DisplayData();
                            MessageBox.Show("Record has been saved.", $"Affected rows: {rows}",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            isAdding = false;
                            AddButton.Enabled = true;
                            AddButton.Visible = true;
                            dgvRecords.Visible = true;
                            FieldsPanel.Enabled = false;
                            FieldsPanel.Visible = false;
                            if (admin >= 1)
                            {
                                chkUnpaid.Enabled = true;
                                btnUnrecord.Enabled = true;
                                btnUnrecord.Visible = true;
                                chkUnpaid.Visible = true;
                            }
                        } else
                        {
                            MessageBox.Show("An error has been occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } catch (Exception ex)
                    {
                        MessageBox.Show($"An error has been occurred.\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        // Exit out of the Input screen when searching
        #region Access list of records when user clicks the Search button
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
                if (admin >= 1)
                {
                    chkUnpaid.Enabled = true;
                    chkUnpaid.Visible = true;
                    btnUnrecord.Enabled = true;
                    btnUnrecord.Visible = true;
                }
            }
            DisplayData();
        }
        #endregion

        // Cancel button (users actually use the Update button as the cancel button and named "Cancel")
        #region Cancel button in add/edit page
        private void CancelAddButton_Click(object sender, EventArgs e)
        {
            isAdding = false;
            AddButton.Enabled = true;
            AddButton.Visible = true;
            dgvRecords.Visible = true;
            FieldsPanel.Enabled = false;
            FieldsPanel.Visible = false;
            if (admin >= 1)
            {
                chkUnpaid.Enabled = true;
                chkUnpaid.Visible = true;
                btnUnrecord.Enabled = true;
                btnUnrecord.Visible = true;
            }
        }
        #endregion

        // Delete a row based on the ID.
        #region Delete a row
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (admin == 1)
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
                            isAdding = false;
                            AddButton.Enabled = true;
                            AddButton.Visible = true;
                            dgvRecords.Visible = true;
                            FieldsPanel.Enabled = false;
                            FieldsPanel.Visible = false;
                            if (admin >= 1)
                            {
                                chkUnpaid.Enabled = true;
                                chkUnpaid.Visible = true;
                                btnUnrecord.Enabled = true;
                                btnUnrecord.Visible = true;
                            }
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
        #endregion

        // Disable the checkbox for Paid if not recorded
        #region Disable paid checkbox when unrecord is checked
        private void UnrecordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UnrecordCheckBox.Checked)
            {
                PaidCheckBox.Checked = false;
                PaidCheckBox.Enabled = false;
            } else
            {
                PaidCheckBox.Enabled = true;
            }
        }
        #endregion

        // This occurs when a checkbox for displaying the unpaid records is changed.
        #region Update list of records to display unpaid data
        private void chkUnpaid_CheckedChanged(object sender, EventArgs e)
        {
            DisplayData();
        }
        #endregion

        // This occurs when sysadmins click File > Account > Create account
        #region Create new account
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (admin == 1) {
                NewAccount t = new NewAccount(db);
                Hide();
                DialogResult f = t.ShowDialog();
                Show();
                if (f == DialogResult.OK)
                {
                    FillSearch();
                }
                t.Dispose();
            }
        }
        #endregion

        // This occurs when users click Graph > Save as... or click Save as PNG... in Graph view
        #region Create an image of graph and save as PNG, JPEG, etc.
        private void button4_Click(object sender, EventArgs e)
        {
            Image f;
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
                Filter = "Portable Network Graphics|*.png|JPEG Image|*.jpg;*.jpeg;*.jpe;*.jfif|Bitmap image|*.bmp|All files|*.*",
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
        #endregion

        // This calls the Print Worker when printing a report
        #region Report printing
        private void PrintButton_Click(object sender, EventArgs e)
        {
            if (admin == 0)
            {
                p.DrawLineExt(picGraph, 1, Color.Blue, new Point(1920, 1080), 1, 250, false, null, false, true);
                p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(1920, 1080), 1, 1, false, null, false, true);
                Bitmap rt = (Bitmap)picGraph.Image;
                Bitmap pt = (Bitmap)picGraphPricePerKwh.Image;
                p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraphPricePerKwh.Width, picGraphPricePerKwh.Height), 1, 1);
                string uname = GetName();
                DataTable users = db.ExecuteQuery($"SELECT FullName FROM AccountTBL WHERE Username='{uname}'");
                DataTable tbl = db.ExecuteQuery($"SELECT PreviousKWH, CurrentKWH, RatePerKWH, Price, ReceiptNo, DateCreated FROM RecordTable WHERE CustomerName='{uname}' ORDER BY RecordTimeIndex ASC");
                string dname = users.Rows[0]["FullName"].ToString() ?? "Unknown Name";
                PrintWorker d = new PrintWorker(rt, pt, tbl, dname);
                d.Print();
            }
        }
        #endregion

        // This redraws the graph when users click Graph > Refresh.
        #region Refresh graph data (if not already refreshed)
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Draw();
        }
        #endregion

        // This redraws the graph when specific Windows messages are called. This does not need to be called manually.
        #region Refresh graph image when manipulating with windows
        /// <summary>
        /// This is for making changes to the size of the graph when performing window commands to prevent distortions.
        /// </summary>
        /// <param name="m">A message pointer that Windows executes. This contains commands for window resize, move, maximize, minimize, restore, and Alt key press.</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m); // Perform Windows command first
            if (m.Msg == 0x0112) // 0x0112 tells us that Windows is trying to move the window
            {
                int wparam = m.WParam.ToInt32() & 0xfff0; // Convert a Windows message parameter into what we can read
                switch (wparam)
                {
                    case 0xF030: // Maximize
                        if (admin == 0) 
                        {
                            p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                            p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraphPricePerKwh.Width, picGraphPricePerKwh.Height), 1, 1);
                        }
                        break;
                    case 0xF120: // Restore
                        if (admin == 0)
                        {
                            p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                            p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraphPricePerKwh.Width, picGraphPricePerKwh.Height), 1, 1);
                        }
                        break;
                    case 0xF010: // Drag
                        if (admin == 0)
                        {
                            p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                            p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraphPricePerKwh.Width, picGraphPricePerKwh.Height), 1, 1);
                        }
                        break;
                    case 0xF000: // Resize
                        if (admin == 0)
                        {
                            p.DrawLineExt(picGraph, 1, Color.Blue, new Point(picGraph.Width, picGraph.Height), 1, 250);
                            p.DrawLineExt(picGraphPricePerKwh, 0, Color.Red, new Point(picGraphPricePerKwh.Width, picGraphPricePerKwh.Height), 1, 1);
                        }
                        break;
                }
            }
        }
        #endregion

        // This displays the details when users click parts of the graph
        #region Display details when a user click on points on a graph
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
        // These are called when a user click in a graph PictureBox
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
        #endregion

        // This is if users clicks Graph > Save as... menu
        #region Redirect the Save as button to the main save method
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }
        #endregion

        // This is called when we saved a graph into an image or record as a simple CSV spreadsheet file.
        #region Open directory
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
        #endregion

        // This creates a CSV spreadsheet file. This is mainly used to convert it into Excel or other spreadsheet formats.
        #region Create a CSV (comma-separated values) spreadsheet/list file
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
        #endregion

        // This closes the window without kicking us back to the login screen.
        #region Close window when clicking on X or clicking "Exit" button
        private void exitAltF4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        // This checks if the window is closed with Account > Log out
        #region Set DialogResult when closing depending on the exit variable
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ft) DialogResult = DialogResult.Cancel;
        }
        #endregion

        // Exit to login page if we confirm to log out
        #region Exit to login page
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleDark(true);
            if (MessageBox.Show("Do you want to log out?", "Log out", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ft = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            ToggleDark(false);
        }

        #endregion

    }
}
