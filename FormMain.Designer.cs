namespace HotelManagementSystem
{
    partial class FormMain
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
            this.dashboard = new System.Windows.Forms.Label();
            this.pnlAvailable = new System.Windows.Forms.Panel();
            this.lblAvailableCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlOccupied = new System.Windows.Forms.Panel();
            this.lblOccupiedCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlMaintenance = new System.Windows.Forms.Panel();
            this.lblMaintenanceCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlGuests = new System.Windows.Forms.Panel();
            this.lblGuestCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnBookings = new System.Windows.Forms.Button();
            this.btnManageRooms = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.pnlAvailable.SuspendLayout();
            this.pnlOccupied.SuspendLayout();
            this.pnlMaintenance.SuspendLayout();
            this.pnlGuests.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // dashboard
            // 
            this.dashboard.AutoSize = true;
            this.dashboard.Font = new System.Drawing.Font("Loben Variable Trial Bold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dashboard.Location = new System.Drawing.Point(12, 44);
            this.dashboard.Name = "dashboard";
            this.dashboard.Size = new System.Drawing.Size(194, 39);
            this.dashboard.TabIndex = 0;
            this.dashboard.Text = "Dashboard";
            // 
            // pnlAvailable
            // 
            this.pnlAvailable.BackColor = System.Drawing.Color.Tan;
            this.pnlAvailable.Controls.Add(this.lblAvailableCount);
            this.pnlAvailable.Controls.Add(this.label1);
            this.pnlAvailable.Location = new System.Drawing.Point(19, 109);
            this.pnlAvailable.Name = "pnlAvailable";
            this.pnlAvailable.Size = new System.Drawing.Size(187, 151);
            this.pnlAvailable.TabIndex = 1;
            // 
            // lblAvailableCount
            // 
            this.lblAvailableCount.AutoSize = true;
            this.lblAvailableCount.Font = new System.Drawing.Font("Le Jour Serif Personal Use Only", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableCount.Location = new System.Drawing.Point(60, 45);
            this.lblAvailableCount.Name = "lblAvailableCount";
            this.lblAvailableCount.Size = new System.Drawing.Size(77, 95);
            this.lblAvailableCount.TabIndex = 6;
            this.lblAvailableCount.Text = "0";
            this.lblAvailableCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Loben Variable Trial Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "AVAILABLE ROOMS";
            // 
            // pnlOccupied
            // 
            this.pnlOccupied.BackColor = System.Drawing.Color.Tan;
            this.pnlOccupied.Controls.Add(this.lblOccupiedCount);
            this.pnlOccupied.Controls.Add(this.label4);
            this.pnlOccupied.Location = new System.Drawing.Point(215, 109);
            this.pnlOccupied.Name = "pnlOccupied";
            this.pnlOccupied.Size = new System.Drawing.Size(187, 151);
            this.pnlOccupied.TabIndex = 7;
            // 
            // lblOccupiedCount
            // 
            this.lblOccupiedCount.AutoSize = true;
            this.lblOccupiedCount.Font = new System.Drawing.Font("Le Jour Serif Personal Use Only", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOccupiedCount.Location = new System.Drawing.Point(60, 45);
            this.lblOccupiedCount.Name = "lblOccupiedCount";
            this.lblOccupiedCount.Size = new System.Drawing.Size(77, 95);
            this.lblOccupiedCount.TabIndex = 6;
            this.lblOccupiedCount.Text = "0";
            this.lblOccupiedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Loben Variable Trial Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(32, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "OCCUPIED STAY";
            // 
            // pnlMaintenance
            // 
            this.pnlMaintenance.BackColor = System.Drawing.Color.Tan;
            this.pnlMaintenance.Controls.Add(this.lblMaintenanceCount);
            this.pnlMaintenance.Controls.Add(this.label6);
            this.pnlMaintenance.Location = new System.Drawing.Point(411, 109);
            this.pnlMaintenance.Name = "pnlMaintenance";
            this.pnlMaintenance.Size = new System.Drawing.Size(187, 151);
            this.pnlMaintenance.TabIndex = 8;
            // 
            // lblMaintenanceCount
            // 
            this.lblMaintenanceCount.AutoSize = true;
            this.lblMaintenanceCount.Font = new System.Drawing.Font("Le Jour Serif Personal Use Only", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaintenanceCount.Location = new System.Drawing.Point(60, 45);
            this.lblMaintenanceCount.Name = "lblMaintenanceCount";
            this.lblMaintenanceCount.Size = new System.Drawing.Size(77, 95);
            this.lblMaintenanceCount.TabIndex = 6;
            this.lblMaintenanceCount.Text = "0";
            this.lblMaintenanceCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Loben Variable Trial Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 19);
            this.label6.TabIndex = 5;
            this.label6.Text = "UNDER MAINTENANCE";
            // 
            // pnlGuests
            // 
            this.pnlGuests.BackColor = System.Drawing.Color.Tan;
            this.pnlGuests.Controls.Add(this.lblGuestCount);
            this.pnlGuests.Controls.Add(this.label8);
            this.pnlGuests.Location = new System.Drawing.Point(607, 109);
            this.pnlGuests.Name = "pnlGuests";
            this.pnlGuests.Size = new System.Drawing.Size(187, 151);
            this.pnlGuests.TabIndex = 9;
            // 
            // lblGuestCount
            // 
            this.lblGuestCount.AutoSize = true;
            this.lblGuestCount.Font = new System.Drawing.Font("Le Jour Serif Personal Use Only", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuestCount.Location = new System.Drawing.Point(60, 45);
            this.lblGuestCount.Name = "lblGuestCount";
            this.lblGuestCount.Size = new System.Drawing.Size(77, 95);
            this.lblGuestCount.TabIndex = 6;
            this.lblGuestCount.Text = "0";
            this.lblGuestCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Loben Variable Trial Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(32, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 19);
            this.label8.TabIndex = 5;
            this.label8.Text = "TOTAL GUESTS";
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnlContent.Controls.Add(this.btnLogout);
            this.pnlContent.Controls.Add(this.btnBookings);
            this.pnlContent.Controls.Add(this.btnManageRooms);
            this.pnlContent.Controls.Add(this.btnDashboard);
            this.pnlContent.Font = new System.Drawing.Font("Loben Variable Trial Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlContent.Location = new System.Drawing.Point(350, 44);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(444, 39);
            this.pnlContent.TabIndex = 10;
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.ClientSize = new System.Drawing.Size(818, 370);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlGuests);
            this.Controls.Add(this.pnlMaintenance);
            this.Controls.Add(this.pnlOccupied);
            this.Controls.Add(this.pnlAvailable);
            this.Controls.Add(this.dashboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.pnlAvailable.ResumeLayout(false);
            this.pnlAvailable.PerformLayout();
            this.pnlOccupied.ResumeLayout(false);
            this.pnlOccupied.PerformLayout();
            this.pnlMaintenance.ResumeLayout(false);
            this.pnlMaintenance.PerformLayout();
            this.pnlGuests.ResumeLayout(false);
            this.pnlGuests.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label dashboard;
        private System.Windows.Forms.Panel pnlAvailable;
        private System.Windows.Forms.Label lblAvailableCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlOccupied;
        private System.Windows.Forms.Label lblOccupiedCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlMaintenance;
        private System.Windows.Forms.Label lblMaintenanceCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlGuests;
        private System.Windows.Forms.Label lblGuestCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnBookings;
        private System.Windows.Forms.Button btnManageRooms;
        private System.Windows.Forms.Button btnDashboard;
    }
}