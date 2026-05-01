namespace DBProject.UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnAppointments = new System.Windows.Forms.Button();
            this.btnMedications = new System.Windows.Forms.Button();
            this.btnPrescribtions = new System.Windows.Forms.Button();
            this.btnDepartments = new System.Windows.Forms.Button();
            this.btnPatients = new System.Windows.Forms.Button();
            this.btnDoctors = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnAddEntity = new System.Windows.Forms.Button();
            this.btnEditEntity = new System.Windows.Forms.Button();
            this.btnDeleteEntity = new System.Windows.Forms.Button();
            this.panelSidebar.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panelSidebar.Controls.Add(this.btnAppointments);
            this.panelSidebar.Controls.Add(this.btnMedications);
            this.panelSidebar.Controls.Add(this.btnPrescribtions);
            this.panelSidebar.Controls.Add(this.btnDepartments);
            this.panelSidebar.Controls.Add(this.btnPatients);
            this.panelSidebar.Controls.Add(this.btnDoctors);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(200, 561);
            this.panelSidebar.TabIndex = 0;
            // 
            // btnAppointments
            // 
            this.btnAppointments.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAppointments.FlatAppearance.BorderSize = 0;
            this.btnAppointments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppointments.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnAppointments.Location = new System.Drawing.Point(0, 300);
            this.btnAppointments.Name = "btnAppointments";
            this.btnAppointments.Size = new System.Drawing.Size(200, 60);
            this.btnAppointments.TabIndex = 5;
            this.btnAppointments.Text = "Appointments";
            this.btnAppointments.UseVisualStyleBackColor = true;
            this.btnAppointments.Click += new System.EventHandler(this.btnAppointments_Click);
            // 
            // btnMedications
            // 
            this.btnMedications.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMedications.FlatAppearance.BorderSize = 0;
            this.btnMedications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMedications.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnMedications.Location = new System.Drawing.Point(0, 240);
            this.btnMedications.Name = "btnMedications";
            this.btnMedications.Size = new System.Drawing.Size(200, 60);
            this.btnMedications.TabIndex = 4;
            this.btnMedications.Text = "Medications";
            this.btnMedications.UseVisualStyleBackColor = true;
            this.btnMedications.Click += new System.EventHandler(this.btnMedications_Click);
            // 
            // btnPrescribtions
            // 
            this.btnPrescribtions.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrescribtions.FlatAppearance.BorderSize = 0;
            this.btnPrescribtions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrescribtions.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnPrescribtions.Location = new System.Drawing.Point(0, 180);
            this.btnPrescribtions.Name = "btnPrescribtions";
            this.btnPrescribtions.Size = new System.Drawing.Size(200, 60);
            this.btnPrescribtions.TabIndex = 3;
            this.btnPrescribtions.Text = "Prescriptions";
            this.btnPrescribtions.UseVisualStyleBackColor = true;
            this.btnPrescribtions.Click += new System.EventHandler(this.btnPrescribtions_Click);
            // 
            // btnDepartments
            // 
            this.btnDepartments.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDepartments.FlatAppearance.BorderSize = 0;
            this.btnDepartments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDepartments.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDepartments.Location = new System.Drawing.Point(0, 120);
            this.btnDepartments.Name = "btnDepartments";
            this.btnDepartments.Size = new System.Drawing.Size(200, 60);
            this.btnDepartments.TabIndex = 2;
            this.btnDepartments.Text = "Departments";
            this.btnDepartments.UseVisualStyleBackColor = true;
            this.btnDepartments.Click += new System.EventHandler(this.btnDepartments_Click);
            // 
            // btnPatients
            // 
            this.btnPatients.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPatients.FlatAppearance.BorderSize = 0;
            this.btnPatients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatients.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnPatients.Location = new System.Drawing.Point(0, 60);
            this.btnPatients.Name = "btnPatients";
            this.btnPatients.Size = new System.Drawing.Size(200, 60);
            this.btnPatients.TabIndex = 1;
            this.btnPatients.Text = "Patients";
            this.btnPatients.UseVisualStyleBackColor = true;
            this.btnPatients.Click += new System.EventHandler(this.btnPatients_Click);
            // 
            // btnDoctors
            // 
            this.btnDoctors.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDoctors.FlatAppearance.BorderSize = 0;
            this.btnDoctors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoctors.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDoctors.Location = new System.Drawing.Point(0, 0);
            this.btnDoctors.Name = "btnDoctors";
            this.btnDoctors.Size = new System.Drawing.Size(200, 60);
            this.btnDoctors.TabIndex = 0;
            this.btnDoctors.Text = "Doctors";
            this.btnDoctors.UseVisualStyleBackColor = true;
            this.btnDoctors.Click += new System.EventHandler(this.btnDoctors_Click);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.dataGridView1);
            this.panelContent.Controls.Add(this.panelActions);
            this.panelContent.Controls.Add(this.panelHeader);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(200, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(684, 561);
            this.panelContent.TabIndex = 1;
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.Controls.Add(this.btnAddEntity);
            this.panelActions.Controls.Add(this.btnEditEntity);
            this.panelActions.Controls.Add(this.btnDeleteEntity);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 501);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(684, 60);
            this.panelActions.TabIndex = 2;
            this.panelActions.Visible = false; // Hidden by default, show conditionally
            // 
            // btnAddEntity
            // 
            this.btnAddEntity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnAddEntity.FlatAppearance.BorderSize = 0;
            this.btnAddEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddEntity.ForeColor = System.Drawing.Color.White;
            this.btnAddEntity.Location = new System.Drawing.Point(20, 10);
            this.btnAddEntity.Name = "btnAddEntity";
            this.btnAddEntity.Size = new System.Drawing.Size(100, 40);
            this.btnAddEntity.TabIndex = 0;
            this.btnAddEntity.Text = "Add";
            this.btnAddEntity.UseVisualStyleBackColor = false;
            this.btnAddEntity.Click += new System.EventHandler(this.btnAddEntity_Click);
            // 
            // btnEditEntity
            // 
            this.btnEditEntity.BackColor = System.Drawing.Color.SteelBlue;
            this.btnEditEntity.FlatAppearance.BorderSize = 0;
            this.btnEditEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditEntity.ForeColor = System.Drawing.Color.White;
            this.btnEditEntity.Location = new System.Drawing.Point(130, 10);
            this.btnEditEntity.Name = "btnEditEntity";
            this.btnEditEntity.Size = new System.Drawing.Size(100, 40);
            this.btnEditEntity.TabIndex = 1;
            this.btnEditEntity.Text = "Edit";
            this.btnEditEntity.UseVisualStyleBackColor = false;
            this.btnEditEntity.Click += new System.EventHandler(this.btnEditEntity_Click);
            // 
            // btnDeleteEntity
            // 
            this.btnDeleteEntity.BackColor = System.Drawing.Color.IndianRed;
            this.btnDeleteEntity.FlatAppearance.BorderSize = 0;
            this.btnDeleteEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteEntity.ForeColor = System.Drawing.Color.White;
            this.btnDeleteEntity.Location = new System.Drawing.Point(240, 10);
            this.btnDeleteEntity.Name = "btnDeleteEntity";
            this.btnDeleteEntity.Size = new System.Drawing.Size(100, 40);
            this.btnDeleteEntity.TabIndex = 2;
            this.btnDeleteEntity.Text = "Delete";
            this.btnDeleteEntity.UseVisualStyleBackColor = false;
            this.btnDeleteEntity.Click += new System.EventHandler(this.btnDeleteEntity_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(684, 501);
            this.dataGridView1.TabIndex = 1;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(684, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(280, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(130, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Welcome";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSidebar);
            this.Name = "MainForm";
            this.Text = "Hospital Management System";
            this.panelSidebar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Button btnDoctors;
        private System.Windows.Forms.Button btnAppointments;
        private System.Windows.Forms.Button btnMedications;
        private System.Windows.Forms.Button btnPrescribtions;
        private System.Windows.Forms.Button btnDepartments;
        private System.Windows.Forms.Button btnPatients;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnAddEntity;
        private System.Windows.Forms.Button btnEditEntity;
        private System.Windows.Forms.Button btnDeleteEntity;
    }
}