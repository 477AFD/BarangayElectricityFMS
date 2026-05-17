using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CRUD_GROUP
{
    public partial class SignUpForm : Form
    {
        DatabaseWorker w;

        public SignUpForm()
        {
            InitializeComponent();
            w = new DatabaseWorker();
        }

        private void LogIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            Program.fLog.Show();
        }
        private int GetLastID()
        {
            int lastID = 0;
            DataTable e = w.ExecuteQuery("SELECT [Index] FROM AccountTBL ORDER BY [Index] ASC");
            for (int i = 0; i < e.Rows.Count; i++)
            {
                if (lastID == int.Parse(e.Rows[i]["Index"].ToString())) lastID++;
                else break;
            }
            return lastID;
        }
        private void SignupButton_Click(object sender, EventArgs e)
        {
            bool[] t = new bool[6];
            t[0] = !string.IsNullOrWhiteSpace(txtUsername.Text) && !txtUsername.Text.Contains(' ');
            t[1] = !string.IsNullOrWhiteSpace(txtDisplayName.Text);
            t[2] = !string.IsNullOrEmpty(txtPwd.Text);
            t[3] = !string.IsNullOrEmpty(txtConfPwd.Text);
            // Here is the truth table:
            /*
             * T2 T3 T4
             * F F T
             * F T T
             * T F T
             * T T [T4]
             */
            DataTable ptr = w.ExecuteQuery("SELECT Username FROM AccountTBL");

            t[4] = !t[2] || !t[3] || (txtConfPwd.Text == txtPwd.Text);
            string wpetoro = "Please correct the following fields:";
            wpetoro += !t[0] ? "\n- The username is empty or contains spaces" : "";
            wpetoro += !t[1] ? "\n- The full name is empty or is using whitespace characters" : "";
            wpetoro += !t[2] ? "\n- The new password is empty" : "";
            wpetoro += !t[3] ? "\n- The confirm password is empty" : "";
            wpetoro += !t[4] ? "\n- The passwords do not match" : "";
            t[5] = true;
            foreach (DataRow p in ptr.Rows)
            {
                if (txtUsername.Text.ToUpper() == p["Username"].ToString().ToUpper())
                {
                    wpetoro += "\n- The username matches with another one, try another username!";
                    t[5] = false;
                    break;
                }
            }
            if (t[0] && t[1] && t[2] && t[3] && t[4] && t[5])
            {
                // Create new account!
                int idx = GetLastID();
                int pf = 0;
                string cmd = $"INSERT INTO AccountTBL VALUES ({idx}, '{txtUsername.Text}','{txtPwd.Text}', {pf}, '{txtDisplayName.Text}')";
                try
                {
                    int rows = w.ExecuteNonQuery(cmd);
                    if (rows > 0)
                    {
                        MessageBox.Show("Account created!", $"Affected rows: {rows}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Hide();
                        Program.fLog.Show();
                    }
                    else
                    {
                        MessageBox.Show("An error has been occured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sorry, Zelda found a SEEKER in her Minecraft world.\n\n{ex}", "Guru meditation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(wpetoro, "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SignUpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.fLog.Close();
        }
    }
}
