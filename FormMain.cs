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
    public partial class FormMain : Form
    {
        private string loggedInUser;
        private string userRole;
        private Form activeForm = null;

        public FormMain(string username, string role)
        {
            InitializeComponent();
            loggedInUser = username;
            userRole = role;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadDashboardStats();
        }
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void LoadDashboardStats()
        {
            try
            {
                // Retrieve aggregates safely using ExecuteScalar
                object availCount = DatabaseConnection.ExecuteScalar("SELECT COUNT(*) FROM `rooms` WHERE status = 'Available'");
                object occupCount = DatabaseConnection.ExecuteScalar("SELECT COUNT(*) FROM `rooms` WHERE status = 'Occupied'");
                object maintenanceCount = DatabaseConnection.ExecuteScalar("SELECT COUNT(*) FROM `rooms` WHERE status = 'Maintenance'");
                object guestCount = DatabaseConnection.ExecuteScalar("SELECT COUNT(*) FROM `guests` ");

                lblAvailableCount.Text = availCount != null ? availCount.ToString() : "0";
                lblOccupiedCount.Text = occupCount != null ? occupCount.ToString() : "0";
                lblMaintenanceCount.Text = maintenanceCount != null ? maintenanceCount.ToString() : "0";
                lblGuestCount.Text = guestCount != null ? guestCount.ToString() : "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not initialize dashboard parameters: " + ex.Message);
            }
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm = null;
            }
            LoadDashboardStats();
        }

        private void btnManageRooms_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormRoomManagement());
        }

        private void btnBookings_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormBooking());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to log out of the system?",
                "Grand Palace Hotel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                this.Hide();
                FormLogin login = new FormLogin();
                login.FormClosed += (s, args) => this.Close();
                login.Show();
            }

        }

        
    }
}
