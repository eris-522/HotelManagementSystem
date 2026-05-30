namespace HotelManagementSystem
{
    partial class FormBooking
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rooms = new System.Windows.Forms.Label();
            this.grpNewBooking = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.txtGuestName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnViewReceipt = new System.Windows.Forms.Button();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.btnBook = new System.Windows.Forms.Button();
            this.cmbRooms = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGuestPhone = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGuestId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTotalAmount = new System.Windows.Forms.Label();
            this.grpAllBookings = new System.Windows.Forms.GroupBox();
            this.dgvBookings = new System.Windows.Forms.DataGridView();
            this.grpQuickActions = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnBookings = new System.Windows.Forms.Button();
            this.btnManageRooms = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.grpNewBooking.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpAllBookings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).BeginInit();
            this.grpQuickActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // rooms
            // 
            this.rooms.AutoSize = true;
            this.rooms.Font = new System.Drawing.Font("Loben Variable Trial Bold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rooms.Location = new System.Drawing.Point(12, 44);
            this.rooms.Name = "rooms";
            this.rooms.Size = new System.Drawing.Size(147, 39);
            this.rooms.TabIndex = 3;
            this.rooms.Text = "Booking";
            // 
            // grpNewBooking
            // 
            this.grpNewBooking.BackColor = System.Drawing.Color.Tan;
            this.grpNewBooking.Controls.Add(this.label7);
            this.grpNewBooking.Controls.Add(this.dtpCheckOut);
            this.grpNewBooking.Controls.Add(this.label2);
            this.grpNewBooking.Controls.Add(this.dtpCheckIn);
            this.grpNewBooking.Controls.Add(this.txtGuestName);
            this.grpNewBooking.Controls.Add(this.label6);
            this.grpNewBooking.Controls.Add(this.btnViewReceipt);
            this.grpNewBooking.Controls.Add(this.btnCheckOut);
            this.grpNewBooking.Controls.Add(this.btnBook);
            this.grpNewBooking.Controls.Add(this.cmbRooms);
            this.grpNewBooking.Controls.Add(this.label5);
            this.grpNewBooking.Controls.Add(this.label4);
            this.grpNewBooking.Controls.Add(this.txtGuestPhone);
            this.grpNewBooking.Controls.Add(this.label3);
            this.grpNewBooking.Controls.Add(this.txtGuestId);
            this.grpNewBooking.Controls.Add(this.label1);
            this.grpNewBooking.Controls.Add(this.panel1);
            this.grpNewBooking.Font = new System.Drawing.Font("Loben Variable Trial Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpNewBooking.Location = new System.Drawing.Point(19, 104);
            this.grpNewBooking.Name = "grpNewBooking";
            this.grpNewBooking.Size = new System.Drawing.Size(775, 329);
            this.grpNewBooking.TabIndex = 5;
            this.grpNewBooking.TabStop = false;
            this.grpNewBooking.Text = "New Booking";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Loben Variable Trial Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 241);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 19);
            this.label7.TabIndex = 18;
            this.label7.Text = "Total Amount:";
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckOut.Location = new System.Drawing.Point(195, 186);
            this.dtpCheckOut.MinDate = new System.DateTime(2026, 5, 30, 0, 0, 0, 0);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(186, 27);
            this.dtpCheckOut.TabIndex = 17;
            this.dtpCheckOut.Value = new System.DateTime(2026, 5, 30, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 19);
            this.label2.TabIndex = 16;
            this.label2.Text = "Check Out";
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckIn.Location = new System.Drawing.Point(10, 186);
            this.dtpCheckIn.MinDate = new System.DateTime(2026, 5, 30, 0, 0, 0, 0);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(179, 27);
            this.dtpCheckIn.TabIndex = 15;
            this.dtpCheckIn.Value = new System.DateTime(2026, 5, 30, 0, 0, 0, 0);
            // 
            // txtGuestName
            // 
            this.txtGuestName.Location = new System.Drawing.Point(195, 62);
            this.txtGuestName.Name = "txtGuestName";
            this.txtGuestName.Size = new System.Drawing.Size(264, 27);
            this.txtGuestName.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(191, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "Guest Full Name";
            // 
            // btnViewReceipt
            // 
            this.btnViewReceipt.Font = new System.Drawing.Font("Loben Variable Trial Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewReceipt.Location = new System.Drawing.Point(582, 280);
            this.btnViewReceipt.Name = "btnViewReceipt";
            this.btnViewReceipt.Size = new System.Drawing.Size(85, 29);
            this.btnViewReceipt.TabIndex = 12;
            this.btnViewReceipt.Text = "&Receipt";
            this.btnViewReceipt.UseVisualStyleBackColor = true;
            this.btnViewReceipt.Click += new System.EventHandler(this.btnViewReceipt_Click);
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.Font = new System.Drawing.Font("Loben Variable Trial Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckOut.Location = new System.Drawing.Point(476, 280);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(91, 29);
            this.btnCheckOut.TabIndex = 11;
            this.btnCheckOut.Text = "&Checkout";
            this.btnCheckOut.UseVisualStyleBackColor = true;
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            // 
            // btnBook
            // 
            this.btnBook.Font = new System.Drawing.Font("Loben Variable Trial Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBook.Location = new System.Drawing.Point(10, 280);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(449, 29);
            this.btnBook.TabIndex = 10;
            this.btnBook.Text = "&Book";
            this.btnBook.UseVisualStyleBackColor = true;
            this.btnBook.Click += new System.EventHandler(this.btnBook_Click);
            // 
            // cmbRooms
            // 
            this.cmbRooms.FormattingEnabled = true;
            this.cmbRooms.Location = new System.Drawing.Point(195, 126);
            this.cmbRooms.Name = "cmbRooms";
            this.cmbRooms.Size = new System.Drawing.Size(264, 27);
            this.cmbRooms.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(191, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Select Room";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Check In";
            // 
            // txtGuestPhone
            // 
            this.txtGuestPhone.Location = new System.Drawing.Point(10, 127);
            this.txtGuestPhone.Name = "txtGuestPhone";
            this.txtGuestPhone.Size = new System.Drawing.Size(179, 27);
            this.txtGuestPhone.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Guest Phone Number";
            // 
            // txtGuestId
            // 
            this.txtGuestId.Location = new System.Drawing.Point(10, 62);
            this.txtGuestId.Name = "txtGuestId";
            this.txtGuestId.Size = new System.Drawing.Size(179, 27);
            this.txtGuestId.TabIndex = 1;
            this.txtGuestId.TextChanged += new System.EventHandler(this.txtGuestId_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Guest ID";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Moccasin;
            this.panel1.Controls.Add(this.txtTotalAmount);
            this.panel1.Location = new System.Drawing.Point(138, 229);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(321, 40);
            this.panel1.TabIndex = 20;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.AutoSize = true;
            this.txtTotalAmount.Font = new System.Drawing.Font("Loben Variable Trial Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Location = new System.Drawing.Point(3, 12);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(19, 19);
            this.txtTotalAmount.TabIndex = 21;
            this.txtTotalAmount.Text = "0";
            // 
            // grpAllBookings
            // 
            this.grpAllBookings.BackColor = System.Drawing.Color.Tan;
            this.grpAllBookings.Controls.Add(this.dgvBookings);
            this.grpAllBookings.Font = new System.Drawing.Font("Loben Variable Trial Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAllBookings.Location = new System.Drawing.Point(19, 453);
            this.grpAllBookings.Name = "grpAllBookings";
            this.grpAllBookings.Size = new System.Drawing.Size(775, 329);
            this.grpAllBookings.TabIndex = 6;
            this.grpAllBookings.TabStop = false;
            this.grpAllBookings.Text = "All Bookings";
            // 
            // dgvBookings
            // 
            this.dgvBookings.AllowUserToAddRows = false;
            this.dgvBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBookings.Location = new System.Drawing.Point(10, 26);
            this.dgvBookings.MultiSelect = false;
            this.dgvBookings.Name = "dgvBookings";
            this.dgvBookings.ReadOnly = true;
            this.dgvBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBookings.Size = new System.Drawing.Size(753, 297);
            this.dgvBookings.TabIndex = 0;
            // 
            // grpQuickActions
            // 
            this.grpQuickActions.BackColor = System.Drawing.Color.AntiqueWhite;
            this.grpQuickActions.Controls.Add(this.btnLogout);
            this.grpQuickActions.Controls.Add(this.btnBookings);
            this.grpQuickActions.Controls.Add(this.btnManageRooms);
            this.grpQuickActions.Controls.Add(this.btnDashboard);
            this.grpQuickActions.Font = new System.Drawing.Font("Loben Variable Trial Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuickActions.Location = new System.Drawing.Point(350, 44);
            this.grpQuickActions.Name = "grpQuickActions";
            this.grpQuickActions.Size = new System.Drawing.Size(444, 39);
            this.grpQuickActions.TabIndex = 11;
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Loben Variable Trial Light", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.Maroon;
            this.btnLogout.Location = new System.Drawing.Point(352, 0);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(86, 39);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnBookings
            // 
            this.btnBookings.Font = new System.Drawing.Font("Loben Variable Trial Light", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBookings.Location = new System.Drawing.Point(250, 0);
            this.btnBookings.Name = "btnBookings";
            this.btnBookings.Size = new System.Drawing.Size(96, 39);
            this.btnBookings.TabIndex = 2;
            this.btnBookings.Text = "Booking";
            this.btnBookings.UseVisualStyleBackColor = true;
            this.btnBookings.Click += new System.EventHandler(this.btnBookings_Click);
            // 
            // btnManageRooms
            // 
            this.btnManageRooms.Font = new System.Drawing.Font("Loben Variable Trial Light", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageRooms.Location = new System.Drawing.Point(154, 0);
            this.btnManageRooms.Name = "btnManageRooms";
            this.btnManageRooms.Size = new System.Drawing.Size(91, 39);
            this.btnManageRooms.TabIndex = 1;
            this.btnManageRooms.Text = "Rooms";
            this.btnManageRooms.UseVisualStyleBackColor = true;
            this.btnManageRooms.Click += new System.EventHandler(this.btnManageRooms_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Font = new System.Drawing.Font("Loben Variable Trial Light", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.Location = new System.Drawing.Point(43, 0);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(107, 39);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // FormBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.ClientSize = new System.Drawing.Size(817, 798);
            this.Controls.Add(this.grpQuickActions);
            this.Controls.Add(this.grpAllBookings);
            this.Controls.Add(this.grpNewBooking);
            this.Controls.Add(this.rooms);
            this.Name = "FormBooking";
            this.Text = "Booking";
            this.Load += new System.EventHandler(this.FormBooking_Load);
            this.grpNewBooking.ResumeLayout(false);
            this.grpNewBooking.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpAllBookings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).EndInit();
            this.grpQuickActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label rooms;
        private System.Windows.Forms.GroupBox grpNewBooking;
        private System.Windows.Forms.Button btnViewReceipt;
        private System.Windows.Forms.Button btnCheckOut;
        private System.Windows.Forms.Button btnBook;
        private System.Windows.Forms.ComboBox cmbRooms;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGuestPhone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGuestId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGuestName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.GroupBox grpAllBookings;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label txtTotalAmount;
        private System.Windows.Forms.DataGridView dgvBookings;
        private System.Windows.Forms.Panel grpQuickActions;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnBookings;
        private System.Windows.Forms.Button btnManageRooms;
        private System.Windows.Forms.Button btnDashboard;
    }
}

