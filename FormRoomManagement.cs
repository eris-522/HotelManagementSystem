using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Mysqlx;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace HotelManagementSystem
{
    public partial class FormRoomManagement : Form
    {
        private Form activeForm = null;
        private ErrorProvider errorProvider = new ErrorProvider();

        // Initializes the form components and attaches event handlers for real-time validation
        public FormRoomManagement()
        {
            InitializeComponent();
            InitializeValidationEvents();
        }

        // Subscribes to text change events to trigger validation as the user types
        private void InitializeValidationEvents()
        {
            txtFloor.TextChanged += (s, e) => ValidateFloor();
            txtRoomNo.TextChanged += (s, e) => ValidateRoomNo();
            txtPrice.TextChanged += (s, e) => ValidatePrice();
        }

        // Validates that the floor input contains only numbers and is within the 1-10 range
        private bool ValidateFloor()
        {
            string val = txtFloor.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                errorProvider.SetError(txtFloor, "Floor number is required. Letters and special characters are not allowed.");
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, @"^\d+$"))
            {
                errorProvider.SetError(txtFloor, "Invalid Floor: Numbers only. Letters and special characters are not allowed.");
                return false;
            }
            int floor = int.Parse(val);
            if (floor < 1 || floor > 10)
            {
                errorProvider.SetError(txtFloor, "Invalid Floor: Floor number cannot exceed 10. Valid floors: 1-10.");
                return false;
            }
            errorProvider.SetError(txtFloor, "");
            return true;
        }

        // Validates that the room number contains only digits and matches the required floor prefix
        private bool ValidateRoomNo()
        {
            string roomNo = txtRoomNo.Text.Trim();
            if (string.IsNullOrEmpty(roomNo))
            {
                errorProvider.SetError(txtRoomNo, "Room number is required.");
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(roomNo, @"^\d+$"))
            {
                errorProvider.SetError(txtRoomNo, "Invalid Room Number: Numbers only. Letters and special characters are not allowed.");
                return false;
            }

            // Check if prefix matches the floor
            string floorVal = txtFloor.Text.Trim();
            if (System.Text.RegularExpressions.Regex.IsMatch(floorVal, @"^\d+$"))
            {
                int floor = int.Parse(floorVal);
                if (floor >= 1 && floor <= 10 && !roomNo.StartsWith(floorVal))
                {
                    errorProvider.SetError(txtRoomNo, $"Invalid format: Since Room starts depend on the floor, Room number must start with Floor {floorVal} (e.g., {floorVal}01, {floorVal}02).");
                    return false;
                }
            }

            errorProvider.SetError(txtRoomNo, "");
            return true;
        }

        // Validates the price input to ensure it is a valid numeric structure (allows decimals)
        private bool ValidatePrice()
        {
            string val = txtPrice.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                errorProvider.SetError(txtPrice, "Price is required.");
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, @"^\d+(\.\d+)?$"))
            {
                errorProvider.SetError(txtPrice, "Invalid Price: Numeric pricing structures only (no letters or special characters).");
                return false;
            }
            errorProvider.SetError(txtPrice, "");
            return true;
        }

        // Populates the combo boxes with predefined valid selections for room types and statuses
        private void InitializeComboBoxes()
        {
            cmbRoomType.Items.Clear();
            cmbStatus.Items.Clear();

            cmbRoomType.Items.AddRange(new string[] { "Single", "Double", "Deluxe", "Suite" });
            cmbStatus.Items.AddRange(new string[] { "Available", "Maintenance", "Occupied" });
        }

        // Triggers upon form load to initialize dropdowns, load the data grid, and reset input fields
        private void FormRoomManagement_Load(object sender, EventArgs e)
        {
            InitializeComboBoxes();
            LoadRoomsGrid();
            ClearInputControls();
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

        // Fetches all registered rooms from the database and binds them to the data grid view
        private void LoadRoomsGrid()
        {
            string query = "SELECT room_no AS 'Room No', room_type AS 'Type', price_per_night AS 'Price/Night', status AS 'Status', floor AS 'Floor' FROM `rooms` ORDER BY room_no ASC";
            DataTable dt = DatabaseConnection.ExecuteQuery(query);
            if (dt != null)
            {
                dgvRooms.DataSource = dt;
            }
        }

        // Resets all input fields and UI states to prepare for a new room entry
        private void ClearInputControls()
        {
            txtRoomNo.Clear();

            // Check added to prevent crashing if clear is called before init
            if (cmbRoomType.Items.Count > 0) cmbRoomType.SelectedIndex = 0;

            txtPrice.Clear();

            if (cmbStatus.Items.Count > 0) cmbStatus.SelectedIndex = 0;

            txtFloor.Text = "1";
            txtRoomNo.Focus();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnAdd.Enabled = true;
            txtRoomNo.ReadOnly = false;
            errorProvider.Clear(); // Clear all error indicators
        }

        // Handles the insertion of a new room record into the database after validating all inputs
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Run all validations instantly
            bool isFloorOk = ValidateFloor();
            bool isRoomNoOk = ValidateRoomNo();
            bool isPriceOk = ValidatePrice();

            if (!isFloorOk || !isRoomNoOk || !isPriceOk)
            {
                MessageBox.Show("Input Validation Failed! Please correct the marked fields with error indicators before inserting standard rows.", "Validation Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int floor = int.Parse(txtFloor.Text.Trim());
            string roomNo = txtRoomNo.Text.Trim();
            decimal price = decimal.Parse(txtPrice.Text.Trim());

            string type = cmbRoomType.SelectedItem?.ToString();
            string status = cmbStatus.SelectedItem?.ToString();

            // Check duplicate Room ID first
            object count = DatabaseConnection.ExecuteScalar("SELECT COUNT(*) FROM `rooms` WHERE room_no = @num",
                new MySqlParameter[] { new MySqlParameter("@num", roomNo) });

            if (count != null && Convert.ToInt32(count) > 0)
            {
                MessageBox.Show("Room number already exists. Please choose another.", "Duplicate Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string insertQuery = "INSERT INTO `rooms` (room_no, room_type, price_per_night, status, floor) VALUES (@No, @Type, @Price, @Status, @Floor)";
            MySqlParameter[] parms = {
                new MySqlParameter("@No", roomNo),
                new MySqlParameter("@Type", type),
                new MySqlParameter("@Price", price),
                new MySqlParameter("@Status", status),
                new MySqlParameter("@Floor", floor)
            };

            int res = DatabaseConnection.ExecuteNonQuery(insertQuery, parms);
            if (res > 0)
            {
                MessageBox.Show("Room added successfully!", "Rooms Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRoomsGrid();
                ClearInputControls();
            }

        }

        // Processes modifications to an existing room's details, preventing updates if the room is occupied
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string roomNo = txtRoomNo.Text.Trim();
            if (string.IsNullOrEmpty(roomNo)) return;

            // Check if older version of room was occupied
            object oldStatusObj = DatabaseConnection.ExecuteScalar("SELECT status FROM `rooms` WHERE room_no = @No",
                new MySqlParameter[] { new MySqlParameter("@No", roomNo) });
            if (oldStatusObj != null && oldStatusObj.ToString() == "Occupied")
            {
                MessageBox.Show("This room is currently occupied and cannot be edited/updated.", "Security Safeguard", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Run all validations instantly
            bool isFloorOk = ValidateFloor();
            bool isRoomNoOk = ValidateRoomNo();
            bool isPriceOk = ValidatePrice();

            if (!isFloorOk || !isRoomNoOk || !isPriceOk)
            {
                MessageBox.Show("Input Validation Failed! Please correct the marked fields with error indicators before updating room record.", "Validation Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int floor = int.Parse(txtFloor.Text.Trim());
            decimal price = decimal.Parse(txtPrice.Text.Trim());

            string type = cmbRoomType.SelectedItem?.ToString();
            string status = cmbStatus.SelectedItem?.ToString();

            string updateQuery = "UPDATE `rooms` SET room_type = @Type, price_per_night = @Price, status = @Status, floor = @Floor WHERE room_no = @No";
            MySqlParameter[] parms = {
                new MySqlParameter("@Type", type),
                new MySqlParameter("@Price", price),
                new MySqlParameter("@Status", status),
                new MySqlParameter("@Floor", floor),
                new MySqlParameter("@No", roomNo)
            };

            int res = DatabaseConnection.ExecuteNonQuery(updateQuery, parms);
            if (res > 0)
            {
                MessageBox.Show("Room detail updated successfully!", "Rooms Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRoomsGrid();
                ClearInputControls();
            }

        }

        // Handles the removal of a room from the database, blocking deletion if the room is occupied
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string roomNo = txtRoomNo.Text.Trim();
            if (string.IsNullOrEmpty(roomNo)) return;

            // Check if older version of room is occupied
            object oldStatusObj = DatabaseConnection.ExecuteScalar("SELECT status FROM `rooms` WHERE room_no = @No",
                new MySqlParameter[] { new MySqlParameter("@No", roomNo) });
            if (oldStatusObj != null && oldStatusObj.ToString() == "Occupied")
            {
                MessageBox.Show("This room is currently occupied and cannot be deleted.", "Delete Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Are you sure you want to delete Room {roomNo}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            string deleteQuery = "DELETE FROM `rooms` WHERE room_no = @No";
            int res = DatabaseConnection.ExecuteNonQuery(deleteQuery, new MySqlParameter[] { new MySqlParameter("@No", roomNo) });
            if (res > 0)
            {
                MessageBox.Show("Room deleted from registry.", "Rooms Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRoomsGrid();
                ClearInputControls();
            }

        }

        private void dgvRooms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRooms.Rows[e.RowIndex];

                // Checks the database status directly from the grid data
                string status = row.Cells["Status"].Value.ToString();

                // Prevents modification or deletion of rooms currently in use
                if (status == "Occupied")
                {
                    MessageBox.Show("This room is currently occupied and cannot be edited or deleted.", "Room Occupied Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearInputControls();
                    return;
                }

                // Populates the form controls with the data from the selected row
                txtRoomNo.Text = row.Cells["Room No"].Value.ToString();
                cmbRoomType.SelectedItem = row.Cells["Type"].Value.ToString();
                txtPrice.Text = row.Cells["Price/Night"].Value.ToString();
                cmbStatus.SelectedItem = row.Cells["Status"].Value.ToString();
                txtFloor.Text = row.Cells["Floor"].Value.ToString();

                // Locks the Room No field because primary keys should never be altered during an update
                txtRoomNo.ReadOnly = true;

                // Disables adding a new record to prevent duplicate key crashes, and enables modification buttons
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ClearInputControls();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormMain());
        }

        private void btnManageRooms_Click(object sender, EventArgs e)
        {

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