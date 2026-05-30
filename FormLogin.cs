using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementSystem
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Username and Password.", "Login Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Database Query with parameterized values to prevent SQL injection
            string query = "SELECT * FROM `users` WHERE username = @User AND password = @Pass";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@User", MySqlDbType.VarChar) { Value = username },
                new MySqlParameter("@Pass", MySqlDbType.VarChar) { Value = password }
            };

            DataTable dt = DatabaseConnection.ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                string fullName = dt.Rows[0]["full_name"].ToString();
                string role = dt.Rows[0]["role"].ToString();

                MessageBox.Show($"Welcome, {fullName}! Logged in successfully as {role}.", "Login Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open Main Form and hide login
                FormMain mainForm = new FormMain(fullName, role);
                this.Hide();
                mainForm.ShowDialog();
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Invalid Username or Password. Please try again.", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtUsername.Focus();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
