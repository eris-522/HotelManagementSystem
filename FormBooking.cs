using Google.Protobuf.Compiler;
using MySql.Data.MySqlClient;
using Mysqlx;
using Mysqlx.Session;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace HotelManagementSystem
{
    public partial class FormBooking : Form
    {
        // Feature: Provides real-time visual error indicators for input fields
        private ErrorProvider errorProvider = new ErrorProvider();

        public FormBooking()
        {
            InitializeComponent();
        }

        // Feature: Initializes default UI state, restricts dates, and binds event handlers
        private void FormBooking_Load(object sender, EventArgs e)
        {
            LoadBookingsGrid();
            LoadAvailableRoomsComboBox();

            // Feature: Restricts the calendar selection to the current day and future days only
            dtpCheckIn.MinDate = DateTime.Today;
            dtpCheckOut.MinDate = DateTime.Today;

            dtpCheckIn.Value = DateTime.Today;
            dtpCheckOut.Value = DateTime.Today.AddDays(1);

            // Auto-populate guest details if ID exists
            txtGuestId.TextChanged += txtGuestId_TextChanged;

            // Initialize real-time validation and calculation listeners
            InitializeValidationEvents();

            // Trigger the initial cost calculation to populate txtTotalAmount right away
            CalculateCostEvent(null, null);
        }

        // Feature: Subscribes UI controls to validation and calculation events for immediate feedback
        private void InitializeValidationEvents()
        {
            txtGuestId.TextChanged += (s, e) => ValidateGuestId();
            txtGuestName.TextChanged += (s, e) => ValidateGuestName();
            txtGuestPhone.TextChanged += (s, e) => ValidateGuestPhone();

            // Feature: Dynamically recalculates total cost whenever dates or room selection change
            dtpCheckIn.ValueChanged += (s, e) =>
            {
                // Forward-thinking safeguard: Automatically push check-out date forward if check-in surpasses it
                if (dtpCheckOut.Value <= dtpCheckIn.Value)
                {
                    dtpCheckOut.Value = dtpCheckIn.Value.AddDays(1);
                }
                CalculateCostEvent(s, e);
            };

            dtpCheckOut.ValueChanged += CalculateCostEvent;
            cmbRooms.SelectedIndexChanged += CalculateCostEvent;
        }

        // Feature: Validates Guest ID format in real-time
        private bool ValidateGuestId()
        {
            string guestId = txtGuestId.Text.Trim();
            if (string.IsNullOrEmpty(guestId))
            {
                errorProvider.SetError(txtGuestId, "Guest ID is required.");
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(guestId, @"^ID-\d{4}$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                errorProvider.SetError(txtGuestId, "Strict format required: ID-0000 (e.g. ID-1234).");
                return false;
            }
            errorProvider.SetError(txtGuestId, "");
            return true;
        }

        // Feature: Validates Guest Name for invalid characters in real-time
        private bool ValidateGuestName()
        {
            string guestName = txtGuestName.Text.Trim();
            if (string.IsNullOrEmpty(guestName))
            {
                errorProvider.SetError(txtGuestName, "Guest Name is required.");
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(guestName, @"^[a-zA-Z\s]+$"))
            {
                errorProvider.SetError(txtGuestName, "Numbers and special characters are not allowed.");
                return false;
            }
            errorProvider.SetError(txtGuestName, "");
            return true;
        }

        // Feature: Validates Phone Number using existing Philippine format rules in real-time
        private bool ValidateGuestPhone()
        {
            string guestPhone = txtGuestPhone.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(guestPhone))
            {
                errorProvider.SetError(txtGuestPhone, "Guest Phone is required.");
                return false;
            }
            if (!IsValidPhilippinePhone(guestPhone))
            {
                errorProvider.SetError(txtGuestPhone, "Invalid Philippine phone number format.");
                return false;
            }
            errorProvider.SetError(txtGuestPhone, "");
            return true;
        }

        // Feature: Auto-populates guest details if an existing guest ID is entered.
        private void txtGuestId_TextChanged(object sender, EventArgs e)
        {
            string gId = txtGuestId.Text.Trim();
            if (string.IsNullOrEmpty(gId) || gId.Length < 7) return;

            string query = "SELECT full_name, phone FROM `guests` WHERE guest_id = @gId";
            using (MySqlConnection conn = DatabaseConnection.GetConnection())
            {
                if (conn == null) return;
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@gId", gId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtGuestName.Text = reader["full_name"].ToString();
                            txtGuestPhone.Text = reader["phone"].ToString();
                        }
                    }
                }
            }
        }

        // Feature: Fetches and binds current booking records to the DataGridView.
        private void LoadBookingsGrid()
        {
            string query = "SELECT b.booking_id AS 'Booking ID', b.room_no AS 'Room', g.full_name AS 'Guest Name', " +
                           "b.check_in_date AS 'Check In', b.check_out_date AS 'Check Out', b.total_amount AS 'Cost', " +
                           "b.status AS 'Status' FROM `bookings` b " +
                           "INNER JOIN `guests` g ON b.guest_id = g.guest_id " +
                           "ORDER BY b.booking_id DESC";

            DataTable dt = DatabaseConnection.ExecuteQuery(query);
            if (dt != null)
            {
                dgvBookings.DataSource = dt;
            }
        }

        // Feature: Queries the database for unoccupied rooms and populates the selection dropdown.
        private void LoadAvailableRoomsComboBox()
        {
            cmbRooms.Items.Clear();
            string query = "SELECT room_no, price_per_night FROM `rooms` WHERE status = 'Available' ORDER BY room_no ASC";
            DataTable dt = DatabaseConnection.ExecuteQuery(query);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cmbRooms.Items.Add($"{row["room_no"]} (${row["price_per_night"]}/night)");
                }
            }
            if (cmbRooms.Items.Count > 0) cmbRooms.SelectedIndex = 0;
        }

        // Feature: Parses the selected room's rate and dynamically calculates total cost based on the date span.
        private void CalculateCostEvent(object sender, EventArgs e)
        {
            try
            {
                if (cmbRooms.SelectedItem == null) return;

                string selected = cmbRooms.SelectedItem.ToString();
                // Extract price out of "101 ($50.00/night)"
                int start = selected.IndexOf('$') + 1;
                int end = selected.IndexOf('/');
                if (start > 0 && end > start)
                {
                    string pricePart = selected.Substring(start, end - start).Trim();
                    decimal rate = decimal.Parse(pricePart);

                    DateTime startD = dtpCheckIn.Value.Date;
                    DateTime endD = dtpCheckOut.Value.Date;
                    int nights = (endD - startD).Days;
                    if (nights <= 0) nights = 1; // Minimum baseline is 1 night stay charge

                    decimal grandTotal = rate * nights;
                    txtTotalAmount.Text = grandTotal.ToString("F2");
                }
            }
            catch { }
        }

        // Feature: Validates all inputs and commits a new reservation transaction to the database, updating room status.
        private void btnBook_Click(object sender, EventArgs e)
        {
            // Feature: Executes instantaneous validation checks before permitting a database transaction
            bool isGuestIdOk = ValidateGuestId();
            bool isGuestNameOk = ValidateGuestName();
            bool isGuestPhoneOk = ValidateGuestPhone();

            if (!isGuestIdOk || !isGuestNameOk || !isGuestPhoneOk || cmbRooms.SelectedItem == null)
            {
                MessageBox.Show("Input Validation Failed! Please correct the fields marked with red error icons before booking.", "Validation Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string guestId = txtGuestId.Text.Trim();
            string guestName = txtGuestName.Text.Trim();
            string guestPhone = FormatPhilippinePhone(txtGuestPhone.Text.Trim());

            // Extract room number
            string roomSel = cmbRooms.SelectedItem.ToString();
            string roomNo = roomSel.Split(' ')[0];

            DateTime checkIn = dtpCheckIn.Value;
            DateTime checkOut = dtpCheckOut.Value;

            // Final backend safeguards
            if (checkIn.Date < DateTime.Today)
            {
                MessageBox.Show("Invalid Check In Date: Check-in date cannot be in the past. It must be today or a future date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkOut.Date < DateTime.Today)
            {
                MessageBox.Show("Invalid Check Out Date: Check-out date cannot be in the past. It must be today or a future date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkOut.Date <= checkIn.Date)
            {
                MessageBox.Show("Check-out date must succeed Check-In date.", "Invalid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal totalAmount = decimal.Parse(txtTotalAmount.Text);

            // ==========================================
            // SQL Transactions: Insert Guest & Insert Booking and Update Room Status
            // ==========================================
            using (MySqlConnection conn = DatabaseConnection.GetConnection())
            {
                if (conn == null) return;
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insert Guest if record not exists (Verify Guest ID Uniqueness and prevent reuse under a different name)
                        string guestCheckQ = "SELECT full_name FROM `guests` WHERE guest_id = @gId";
                        using (MySqlCommand checkCmd = new MySqlCommand(guestCheckQ, conn, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@gId", guestId);
                            object dbNameObj = checkCmd.ExecuteScalar();
                            if (dbNameObj != null)
                            {
                                string existingName = dbNameObj.ToString();
                                if (!existingName.Equals(guestName, StringComparison.OrdinalIgnoreCase))
                                {
                                    MessageBox.Show($"MySQL Constraint Conflict: The Guest ID '{guestId}' is already registered to a different guest('{existingName}')." +
                                        $"To ensure database reference integrity(no key repetition or re - association), a Guest ID cannot be reused for a different person.Please generate a unique Guest ID or correct the name.", "MySQL Integrity Check Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    transaction.Rollback();
                                    return;
                                }
                            }
                            else
                            {
                                string gInsertQ = "INSERT INTO `guests` (guest_id, full_name, phone) VALUES (@gId, @name, @phone)";
                                using (MySqlCommand insCmd = new MySqlCommand(gInsertQ, conn, transaction))
                                {
                                    insCmd.Parameters.AddWithValue("@gId", guestId);
                                    insCmd.Parameters.AddWithValue("@name", guestName);
                                    insCmd.Parameters.AddWithValue("@phone", guestPhone);
                                    insCmd.ExecuteNonQuery();
                                }
                            }
                        }

                        // 2. Insert Booking record
                        string insertBooking = $"INSERT INTO `bookings` (room_no, guest_id, check_in_date, check_out_date, total_amount, status) " +
                                               "VALUES (@Room, @GId, @In, @Out, @Total, 'CheckedIn')";
                        using (MySqlCommand bkCmd = new MySqlCommand(insertBooking, conn, transaction))
                        {
                            bkCmd.Parameters.AddWithValue("@Room", roomNo);
                            bkCmd.Parameters.AddWithValue("@GId", guestId);
                            bkCmd.Parameters.AddWithValue("@In", checkIn.ToString("yyyy-MM-dd"));
                            bkCmd.Parameters.AddWithValue("@Out", checkOut.ToString("yyyy-MM-dd"));
                            bkCmd.Parameters.AddWithValue("@Total", totalAmount);
                            bkCmd.ExecuteNonQuery();
                        }

                        // 3. Mark Room status as 'Occupied'
                        string updateRoom = $"UPDATE `rooms` SET status = 'Occupied' WHERE room_no = @Room";
                        using (MySqlCommand rmCmd = new MySqlCommand(updateRoom, conn, transaction))
                        {
                            rmCmd.Parameters.AddWithValue("@Room", roomNo);
                            rmCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Reservation saved! Guest checked in successfully.", "Transaction Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadBookingsGrid();
                        LoadAvailableRoomsComboBox();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Transaction rolled back! Booking failed: " + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Feature: Processes final payment, manages early departure penalties/waivers, and updates status to 'CheckedOut'.
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow == null) return;

            DataGridViewRow row = dgvBookings.CurrentRow;
            string bookingId = row.Cells["Booking ID"].Value.ToString();
            string roomNo = row.Cells["Room"].Value.ToString();
            string status = row.Cells["Status"].Value.ToString();
            decimal originalCost = Convert.ToDecimal(row.Cells["Cost"].Value);
            DateTime checkIn = Convert.ToDateTime(row.Cells["Check In"].Value);
            DateTime checkOut = Convert.ToDateTime(row.Cells["Check Out"].Value);

            if (status.Equals("CheckedOut"))
            {
                MessageBox.Show("Selected ticket is already checked-out.", "Operations Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int originalNights = (checkOut.Date - checkIn.Date).Days;
            if (originalNights <= 0) originalNights = 1;

            decimal finalSettlement = originalCost;
            bool isEarlyDeparture = false;

            // Apply Early Departure Policy for stays longer than 1 day
            if (originalNights > 1)
            {
                DialogResult earlyDepartureResult = MessageBox.Show(
                    "Is this guest checking out earlier than the scheduled departure date?",
                    "Early Departure Check", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (earlyDepartureResult == DialogResult.Yes)
                {
                    isEarlyDeparture = true;

                    // Retrieve standard daily room rate
                    decimal dailyRate = originalCost / originalNights;

                    // Ask how many actual nights the guest stayed
                    int actualNights = 1;
                    string input = Microsoft.VisualBasic.Interaction.InputBox(
                        $"Enter number of actual nights stayed (1 to {originalNights - 1}):",
                        "Nights Stayed Simulation", "1");

                    if (int.TryParse(input, out int nightsVal) && nightsVal >= 1 && nightsVal < originalNights)
                    {
                        actualNights = nightsVal;
                    }

                    // Prompt about transition notice
                    DialogResult noticeResult = MessageBox.Show(
                        "Did the guest provide a 24 to 48-hour advanced notice for the early departure?",
                        "Notice Waiver Option", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (noticeResult == DialogResult.Yes)
                    {
                        // Given 24-48h notice: waive remaining nights
                        finalSettlement = actualNights * dailyRate;
                        MessageBox.Show($"24-48h notice verified! Remaining {originalNights - actualNights} nights are waived. Adjusted Stay Bill: ${finalSettlement:F2}", "Notice Waived", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Abrupt departure: stayed nights + 1 night penalty (not exceeding original total)
                        decimal earlyDepartureFee = dailyRate;
                        finalSettlement = Math.Min((actualNights * dailyRate) + earlyDepartureFee, originalCost);
                        MessageBox.Show($"Abrupt departure without notice!\nCharging {actualNights} nights stayed plus 1 night penalty fee of ${earlyDepartureFee:F2}. Adjusted Settlement Bill: ${finalSettlement:F2}",
                            "Early Departure Penalty Applied", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

            if (!isEarlyDeparture)
            {
                DialogResult confirm = MessageBox.Show($"Process final payment billing of ${finalSettlement:F2} for stay at Room {roomNo}?", "Checkout Billing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;
            }

            using (MySqlConnection conn = DatabaseConnection.GetConnection())
            {
                if (conn == null) return;
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Update booking status and final settlement amount
                        string upBooking = $"UPDATE `bookings` SET status = 'CheckedOut', total_amount = @FinalAmount WHERE booking_id = @bId";
                        using (MySqlCommand cmd1 = new MySqlCommand(upBooking, conn, transaction))
                        {
                            cmd1.Parameters.AddWithValue("@FinalAmount", finalSettlement);
                            cmd1.Parameters.AddWithValue("@bId", bookingId);
                            cmd1.ExecuteNonQuery();
                        }

                        // 2. Set Room back to 'Available'
                        string upRoom = $"UPDATE `rooms` SET status = 'Available' WHERE room_no = @room";
                        using (MySqlCommand cmd2 = new MySqlCommand(upRoom, conn, transaction))
                        {
                            cmd2.Parameters.AddWithValue("@room", roomNo);
                            cmd2.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show($"Payment settled successfully! Bill of ${finalSettlement:F2} paid. Guest checked out and Room {roomNo} is now available.", "Operations Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadBookingsGrid();
                        LoadAvailableRoomsComboBox();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Failure settling bill: " + ex.Message, "Exception Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Feature: Generates a formatted text receipt for the completed booking transaction.
        private void btnViewReceipt_Click(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow == null) return;

            DataGridViewRow row = dgvBookings.CurrentRow;
            string bookingId = row.Cells["Booking ID"].Value.ToString();
            string status = row.Cells["Status"].Value.ToString();

            if (!status.Equals("CheckedOut"))
            {
                MessageBox.Show("Receipt is only available for guests who have checked out.", "View Receipt Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string roomNo = row.Cells["Room"].Value.ToString();
            string guestName = row.Cells["Guest Name"].Value.ToString();
            string checkIn = row.Cells["Check In"].Value.ToString();
            string checkOut = row.Cells["Check Out"].Value.ToString();
            string cost = row.Cells["Cost"].Value.ToString();

            string receipt = $"=======================================\n" +
                             $"                RECEIPT        \n" +
                             $"=======================================\n" +
                             $"Receipt No:     RC-{bookingId.PadLeft(4, '0')}-{roomNo}\n" +
                             $"Guest Name:     {guestName}\n" +
                             $"Room Stayed:    Room {roomNo}\n" +
                             $"Check-In Date:  {checkIn}\n" +
                             $"Check-Out Date: {checkOut}\n" +
                             $"---------------------------------------\n" +
                             $"Total Stay Paid: ${cost}\n" +
                             $"Status:          PAID & CLEAR IN FULL\n" +
                             $"=======================================\n" +
                             $"       Thank you for your stay!         \n" +
                             $"=======================================\n";

            MessageBox.Show(receipt, "Official Checkout Billing Invoice Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Feature: Matches incoming phone numbers against verified Philippine mobile and landline standard regex patterns.
        private bool IsValidPhilippinePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;

            string cleaned = System.Text.RegularExpressions.Regex.Replace(phone, @"[^\d+]", "");
            if (phone.Trim().StartsWith("+63") || phone.Trim().StartsWith("63"))
            {
                string digits = System.Text.RegularExpressions.Regex.Replace(cleaned, @"[^\d]", "");
                if (System.Text.RegularExpressions.Regex.IsMatch(digits, @"^639\d{9}$")) return true;
                if (System.Text.RegularExpressions.Regex.IsMatch(digits, @"^632\d{8}$")) return true;
                if (System.Text.RegularExpressions.Regex.IsMatch(digits, @"^63[3-9]\d{8}$")) return true;
                return false;
            }

            string onlyDigits = System.Text.RegularExpressions.Regex.Replace(cleaned, @"[^\d]", "");
            if (onlyDigits.StartsWith("0"))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(onlyDigits, @"^09\d{9}$")) return true;
                if (System.Text.RegularExpressions.Regex.IsMatch(onlyDigits, @"^02\d{8}$")) return true;
                if (System.Text.RegularExpressions.Regex.IsMatch(onlyDigits, @"^0[3-9]\d{8,9}$")) return true;
            }
            return false;
        }

        // Feature: Formats raw input string into standardized spacing conventions depending on the region prefix.
        private string FormatPhilippinePhone(string phone)
        {
            string cleaned = phone.Trim();
            if (string.IsNullOrEmpty(cleaned)) return "";

            string digits = System.Text.RegularExpressions.Regex.Replace(cleaned, @"[^\d]", "");
            if (cleaned.StartsWith("+63") || cleaned.StartsWith("63"))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(digits, @"^639\d{9}$"))
                    return $"+63 9{digits.Substring(3, 2)} {digits.Substring(5, 3)} {digits.Substring(8, 4)}";
                if (System.Text.RegularExpressions.Regex.IsMatch(digits, @"^632\d{8}$"))
                    return $"+63 2 {digits.Substring(3, 4)} {digits.Substring(7, 4)}";
                if (System.Text.RegularExpressions.Regex.IsMatch(digits, @"^63[3-9]\d{8}$"))
                    return $"+63 {digits.Substring(2, 2)} {digits.Substring(4, 3)} {digits.Substring(7, 4)}";
            }

            if (digits.StartsWith("0"))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(digits, @"^09\d{9}$"))
                    return $"09{digits.Substring(2, 2)}-{digits.Substring(4, 3)}-{digits.Substring(7, 4)}";
                if (System.Text.RegularExpressions.Regex.IsMatch(digits, @"^02\d{8}$"))
                    return $"02-{digits.Substring(2, 4)}-{digits.Substring(6, 4)}";
                if (System.Text.RegularExpressions.Regex.IsMatch(digits, @"^0[3-9]\d{8,9}$"))
                {
                    int areaLen = digits.StartsWith("032") || digits.StartsWith("082") ? 3 : 4;
                    if (areaLen == 3)
                        return $"{digits.Substring(0, 3)}-{digits.Substring(3, 3)}-{digits.Substring(6)}";
                    return $"{digits.Substring(0, 4)}-{digits.Substring(4, 3)}-{digits.Substring(7)}";
                }
            }
            return cleaned;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMain frmMain = new FormMain(UserSession.Username, UserSession.Role);
            frmMain.FormClosed += (s, args) => this.Close();
            frmMain.Show();
        }

        private void btnManageRooms_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormRoomManagement frmRoom = new FormRoomManagement();
            frmRoom.FormClosed += (s, args) => this.Close();
            frmRoom.Show();
        }

        private void btnBookings_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to log out of the system?",
        "Grand Palace Hotel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                // Feature: Explicitly flushes session data on logout for security
                UserSession.Username = null;
                UserSession.Role = null;

                this.Hide();
                FormLogin login = new FormLogin();
                login.FormClosed += (s, args) => this.Close();
                login.Show();
            }
        }
    }
}