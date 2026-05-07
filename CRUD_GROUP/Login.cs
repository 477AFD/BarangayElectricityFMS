using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_GROUP
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                KeyValuePair<string, string> account = new KeyValuePair<string, string>(txtUsername.Text, txtPassword.Text);
                MainForm main = new MainForm(account);
                Hide();
                DialogResult t = main.ShowDialog();
                if (t == DialogResult.OK)
                {
                    txtUsername.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    Show();
                } else
                {
                    txtUsername.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    Close();
                }
            }
            catch (AccessViolationException ew)
            {
                if (ew.Message == "SYS:0014")
                {
                    MessageBox.Show("Incorrect username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ew.Message == "SYS:0001")
                {
                    MessageBox.Show("There is a seeker in the records database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Seeker Detected:\n{ex}", "Guru meditation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            Program.fSignUpForm.Show();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Program.fSignUpForm.Close();
        }
    }
}
