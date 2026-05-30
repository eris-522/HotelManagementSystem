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
        private ErrorProvider errorProvider = new ErrorProvider();
        public FormRoomManagement()
        {
            InitializeComponent();
            InitializeValidationEvents();
        }

        private void InitializeValidationEvents()
        {
            txtFloor.TextChanged += (s, e) => ValidateFloor();
            txtRoomNo.TextChanged += (s, e) => ValidateRoomNo();
            txtPrice.TextChanged += (s, e) => ValidatePrice();
        }

        private bool ValidateFloor()
        {
            string val = txtFloor.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                errorProvider.SetError(txtFloor, "Floor number is required. Letters and special characters are not allowed.");
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, @"^d+$"))
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

        private bool ValidateRoomNo()
        {
            string roomNo = txtRoomNo.Text.Trim();
            if (string.IsNullOrEmpty(roomNo))
            {
                errorProvider.SetError(txtRoomNo, "Room number is required.");
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(roomNo, @"^d+$"))
            {
                errorProvider.SetError(txtRoomNo, "Invalid Room Number: Numbers only. Letters and special characters are not allowed.");
                return false;
            }

            // Check if prefix matches the floor
            string floorVal = txtFloor.Text.Trim();
            if (System.Text.RegularExpressions.Regex.IsMatch(floorVal, @"^d+$"))
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

        private bool ValidatePrice()
        {
            string val = txtPrice.Text.Trim();
            if (string.IsNullOrEmpty(val))
            {
                errorProvider.SetError(txtPrice, "Price is required.");
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, @"^d+(.d+)?$"))
            {
                errorProvider.SetError(txtPrice, "Invalid Price: Numeric pricing structures only (no letters or special characters).");
                return false;
            }
            errorProvider.SetError(txtPrice, "");
            return true;
        }


        private void FormRoomManagement_Load(object sender, EventArgs e)
        {
            LoadRoomsGrid();
            ClearInputControls();
        }

        private void LoadRoomsGrid()
        {
            string query = "SELECT room_no AS 'Room No', room_type AS 'Type', price_per_night AS 'Price/Night', status AS 'Status', floor AS 'Floor' FROM `rooms` ORDER BY room_no ASC";
            DataTable dt = DatabaseConnection.ExecuteQuery(query);
            if (dt != null)
            {
                dgvRooms.DataSource = dt;
            }
        }

        private void ClearInputControls()
        {
            txtRoomNo.Clear();
            cmbRoomType.SelectedIndex = 0;
            txtPrice.Clear();
            cmbStatus.SelectedIndex = 0;
            txtFloor.Text = "1";
            txtRoomNo.Focus();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnAdd.Enabled = true;
            txtRoomNo.ReadOnly = false;
            errorProvider.Clear(); // Clear all error indicators
        }


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

        private void dgvRooms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRooms.Rows[e.RowIndex];
                string status = row.Cells["Status"].Value.ToString();
                if (status == "Occupied")
                {
                    MessageBox.Show("This room is currently occupied and cannot be edited or deleted.", "Room Occupied Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearInputControls();
                    return;
                }

                txtRoomNo.Text = row.Cells["Room No"].Value.ToString();
                cmbRoomType.SelectedItem = row.Cells["Type"].Value.ToString();
                txtPrice.Text = row.Cells["Price/Night"].Value.ToString();
                cmbStatus.SelectedItem = row.Cells["Status"].Value.ToString();
                txtFloor.Text = row.Cells["Floor"].Value.ToString();

                txtRoomNo.ReadOnly = true; // Key shouldn't be altered on updates
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputControls();
        }
    }
}
