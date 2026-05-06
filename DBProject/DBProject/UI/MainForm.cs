using System;
using System.Drawing;
using System.Windows.Forms;
using DBProject.Data;

namespace DBProject.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ApplyModernStyles();
        }

        private void ApplyModernStyles()
        {
            // Vercel Light Theme Palette
            Color pureWhite = Color.FromArgb(255, 255, 255); // #FFFFFF
            Color vercelBlack = Color.FromArgb(23, 23, 23); // #171717 (Vercel Black)
            Color primaryText = vercelBlack;
            Color secondaryText = Color.FromArgb(102, 102, 102); // #666666
            Color mutedText = Color.FromArgb(128, 128, 128); // #808080
            Color borderColor = Color.FromArgb(235, 235, 235); // #EBEBEB
            Color surfaceTint = Color.FromArgb(250, 250, 250); // #FAFAFA
            Color destructiveRed = Color.FromArgb(255, 91, 79); // #FF5B4F (Ship Red)
            Color linkBlue = Color.FromArgb(0, 114, 245); // #0072F5

            // Apply palette to forms and panels
            this.BackColor = pureWhite;
            panelSidebar.BackColor = surfaceTint;
            panelContent.BackColor = pureWhite;
            panelActions.BackColor = pureWhite;
            panelHeader.BackColor = pureWhite;
            panelHeader.Padding = new Padding(20, 10, 20, 10);
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left; // prevent it from re-centering
            lblTitle.Location = new Point(20, 15); // left align title

            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblTitle.ForeColor = primaryText;

            // Modernize the DataGridView - Vercel Minimalist Light Style
            dataGridView1.BackgroundColor = pureWhite;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = pureWhite;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = mutedText;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dataGridView1.ColumnHeadersHeight = 45;
            dataGridView1.DefaultCellStyle.BackColor = pureWhite;
            dataGridView1.DefaultCellStyle.ForeColor = primaryText;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 235, 235); // distinct grey
            dataGridView1.DefaultCellStyle.SelectionForeColor = primaryText;
            dataGridView1.DefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            dataGridView1.RowTemplate.Height = 45; // Taller rows
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.GridColor = borderColor;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = pureWhite; // Completely flat
            
            // Add padding around the grid in the content panel
            dataGridView1.Dock = DockStyle.None;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.Location = new Point(20, 80);
            
            // Adjust the size so it doesn't overlap the bottom action panel
            dataGridView1.Size = new Size(panelContent.Width - 40, panelContent.Height - 160);

            panelActions.BringToFront();

            // Enhance sidebar buttons
            foreach (Control control in panelSidebar.Controls)
            {
                if (control is Button btn)
                {
                    btn.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                    btn.ForeColor = secondaryText;
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    btn.Padding = new Padding(20, 0, 0, 0);
                    btn.Cursor = Cursors.Hand;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = borderColor;
                    btn.FlatAppearance.MouseDownBackColor = pureWhite;
                }
            }

            // Vercel Action Buttons Styling (Light Mode)
            StyleActionButton(btnAddEntity, vercelBlack, pureWhite, 0, Color.Empty); // Primary CTA: Black bg, White text
            StyleActionButton(btnEditEntity, pureWhite, primaryText, 1, borderColor); // Secondary CTA: White bg, Black text, faint border
            StyleActionButton(btnDeleteEntity, pureWhite, destructiveRed, 1, borderColor); // Destructive: White bg, Red text, faint border
        }

        private void StyleActionButton(Button btn, Color bg, Color fg, int borderSize, Color borderCol)
        {
            if (btn == null) return;
            btn.BackColor = bg;
            btn.ForeColor = fg;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = borderSize;
            if (borderSize > 0) btn.FlatAppearance.BorderColor = borderCol;
            btn.Cursor = Cursors.Hand;
            btn.Size = new Size(120, 40);
            // Slight adjustments to location to accommodate slightly wider buttons
            if (btn.Name == "btnEditEntity") btn.Location = new Point(150, btn.Location.Y);
            if (btn.Name == "btnDeleteEntity") btn.Location = new Point(280, btn.Location.Y);
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Doctors";
            panelActions.Visible = true;
            btnEditEntity.Visible = true;
            btnDeleteEntity.Location = new Point(280, btnDeleteEntity.Location.Y);
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            DoctorRepository repo = new DoctorRepository();
            dataGridView1.DataSource = repo.GetAllDoctors();
            
            // Hide the actual DeptID column so the user only sees DepartmentName
            if (dataGridView1.Columns.Contains("DeptID"))
            {
                dataGridView1.Columns["DeptID"].Visible = false;
            }

            // Set up proper selection logic
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Patients";
            panelActions.Visible = true;
            btnEditEntity.Visible = true;
            btnDeleteEntity.Location = new Point(280, btnDeleteEntity.Location.Y);
            LoadPatients();
        }

        private void LoadPatients()
        {
            PatientRepository repo = new PatientRepository();
            // Switched to the method that joins the Patient_Phone table
            dataGridView1.DataSource = repo.GetAllPatientsAlongWithTheirPhoneNumbers();
            
            // Set up proper selection logic
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void btnDepartments_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Departments";
            panelActions.Visible = true;
            btnEditEntity.Visible = true;
            btnDeleteEntity.Location = new Point(280, btnDeleteEntity.Location.Y);
            LoadDepartments();
        }

        private void btnPrescribtions_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Prescriptions";
            panelActions.Visible = true;
            btnEditEntity.Visible = false;
            btnDeleteEntity.Location = new Point(150, btnDeleteEntity.Location.Y);
            LoadPrescribtions();
        }

        private void btnMedications_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Medications";
            panelActions.Visible = true;
            btnEditEntity.Visible = true;
            btnDeleteEntity.Location = new Point(280, btnDeleteEntity.Location.Y);
            LoadMedications();
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Appointments";
            panelActions.Visible = true;
            btnEditEntity.Visible = true;
            btnDeleteEntity.Location = new Point(280, btnDeleteEntity.Location.Y);
            LoadAppointments();
        }

        private void LoadDepartments()
        {
            DepartmentRepository repo = new DepartmentRepository();
            dataGridView1.DataSource = repo.GetAllDepartments();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void LoadPrescribtions()
        {
            PrescribtionRepository repo = new PrescribtionRepository();
            dataGridView1.DataSource = repo.GetAllPrescribtions();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            if (dataGridView1.Columns["PatientID"] != null) dataGridView1.Columns["PatientID"].Visible = false;
            if (dataGridView1.Columns["DocID"] != null) dataGridView1.Columns["DocID"].Visible = false;
            if (dataGridView1.Columns["MedCode"] != null) dataGridView1.Columns["MedCode"].Visible = false;
        }

        private void LoadMedications()
        {
            MedicationRepository repo = new MedicationRepository();
            dataGridView1.DataSource = repo.GetAllMedications();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        private void LoadAppointments()
        {
            AppointmentRepository repo = new AppointmentRepository();
            dataGridView1.DataSource = repo.GetAllAppointments();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            if (dataGridView1.Columns["PatientID"] != null) dataGridView1.Columns["PatientID"].Visible = false;
            if (dataGridView1.Columns["DoctorID"] != null) dataGridView1.Columns["DoctorID"].Visible = false;
        }

        private bool ShowVercelModal(Form modal)
        {
            Color pureWhite = Color.FromArgb(255, 255, 255);
            Color vercelBlack = Color.FromArgb(23, 23, 23);
            Color mutedText = Color.FromArgb(128, 128, 128);
            Color borderColor = Color.FromArgb(235, 235, 235);
            Color hoverColor = Color.FromArgb(250, 250, 250);

            modal.BackColor = pureWhite;
            modal.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            modal.FormBorderStyle = FormBorderStyle.FixedDialog;

            foreach (Control ctrl in modal.Controls)
            {
                if (ctrl is Label lbl)
                {
                    if (lbl.Font.Bold || lbl.Font.Size > 12)
                    {
                        lbl.ForeColor = vercelBlack;
                        lbl.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                    }
                    else
                    {
                        lbl.ForeColor = mutedText;
                    }
                }
                else if (ctrl is TextBox txt)
                {
                    txt.BackColor = pureWhite;
                    txt.ForeColor = vercelBlack;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Cursor = Cursors.Hand;
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                    if (btn.Text.ToLower().Contains("save") || btn.Text.ToLower().Contains("add") || btn.Text.ToLower().Contains("update"))
                    {
                         btn.BackColor = vercelBlack;
                         btn.ForeColor = pureWhite;
                         btn.FlatAppearance.BorderSize = 0;
                         btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(10, 10, 10);
                    }
                    else
                    {
                         btn.BackColor = pureWhite;
                         btn.ForeColor = vercelBlack;
                         btn.FlatAppearance.BorderSize = 1;
                         btn.FlatAppearance.BorderColor = borderColor;
                         btn.FlatAppearance.MouseOverBackColor = hoverColor;
                    }
                }
                else if (ctrl is DateTimePicker dtp)
                {
                    dtp.CalendarTitleBackColor = vercelBlack;
                }
            }

            return modal.ShowDialog() == DialogResult.OK;
        }

        private void btnAddEntity_Click(object sender, EventArgs e)
        {
            if (lblTitle.Text == "Patients")
            {
                if (ShowVercelModal(new PatientForm())) LoadPatients();
            }
            else if (lblTitle.Text == "Doctors")
            {
                if (ShowVercelModal(new DoctorForm())) LoadDoctors();
            }
            else if (lblTitle.Text == "Departments")
            {
                if (ShowVercelModal(new DepartmentForm())) LoadDepartments();
            }
            else if (lblTitle.Text == "Medications")
            {
                if (ShowVercelModal(new MedicationForm())) LoadMedications();
            }
            else if (lblTitle.Text == "Appointments")
            {
                if (ShowVercelModal(new AppointmentForm())) LoadAppointments();
            }
            else if (lblTitle.Text == "Prescriptions")
            {
                if (ShowVercelModal(new PrescribtionForm())) LoadPrescribtions();
            }
        }

        private void btnEditEntity_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show($"Please select a {lblTitle.Text.ToLower().TrimEnd('s')} to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dataGridView1.SelectedRows[0];

            if (lblTitle.Text == "Patients")
            {
                string id = row.Cells["PatientID"].Value.ToString();
                string firstName = row.Cells["FirstName"].Value.ToString();
                string lastName = row.Cells["LastName"].Value.ToString();
                string dob = row.Cells["DOB"].Value.ToString();
                string phone = row.Cells["PhoneNumbers"]?.Value?.ToString() ?? "";

                if (ShowVercelModal(new PatientForm(id, firstName, lastName, dob, phone))) LoadPatients();
            }
            else if (lblTitle.Text == "Doctors")
            {
                string id = row.Cells["DoctorID"].Value.ToString();
                string firstName = row.Cells["FirstName"].Value.ToString();
                string lastName = row.Cells["LastName"].Value.ToString();
                string specialty = row.Cells["Specialty"].Value.ToString();
                string deptId = row.Cells["DeptID"].Value.ToString();

                if (ShowVercelModal(new DoctorForm(id, firstName, lastName, specialty, deptId))) LoadDoctors();
            }
            else if (lblTitle.Text == "Departments")
            {
                short id = Convert.ToInt16(row.Cells["DeptID"].Value);
                string name = row.Cells["DeptName"].Value.ToString();
                string loc = row.Cells["Location"].Value.ToString();
                string mng = row.Cells["ManagerID"].Value.ToString();

                if (ShowVercelModal(new DepartmentForm(id, name, loc, mng))) LoadDepartments();
            }
            else if (lblTitle.Text == "Medications")
            {
                short code = Convert.ToInt16(row.Cells["MedCode"]?.Value ?? 0);
                string name = row.Cells["MedName"]?.Value?.ToString() ?? "";
                string dosage = row.Cells["Dosage"]?.Value?.ToString() ?? "";
                string unit = row.Cells["Unit"]?.Value?.ToString() ?? "";

                if (ShowVercelModal(new MedicationForm(code, name, dosage, unit))) LoadMedications();
            }
            else if (lblTitle.Text == "Appointments")
            {
                int id = Convert.ToInt32(row.Cells["ApptID"].Value);
                string date = row.Cells["ApptDate"].Value.ToString();
                string time = row.Cells["ApptTime"].Value.ToString();
                string status = row.Cells["Status"].Value.ToString();
                string patient = row.Cells["PatientID"].Value.ToString();
                string doctor = row.Cells["DoctorID"].Value.ToString();

                if (ShowVercelModal(new AppointmentForm(id, date, time, status, patient, doctor))) LoadAppointments();
            }
            else if (lblTitle.Text == "Prescriptions")
            {
                MessageBox.Show("Editing prescriptions directly is restricted by constraints. Please add or delete instead.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteEntity_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show($"Please select a {lblTitle.Text.ToLower().TrimEnd('s')} to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dataGridView1.SelectedRows[0];
            
            var confirmResult = MessageBox.Show($"Are you sure you want to delete this {lblTitle.Text.ToLower().TrimEnd('s')}?",
                                   "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    if (lblTitle.Text == "Patients")
                    {
                        if (new PatientRepository().DeletePatient(row.Cells["PatientID"].Value.ToString())) LoadPatients();
                    }
                    else if (lblTitle.Text == "Doctors")
                    {
                        if (new DoctorRepository().DeleteDoctor(row.Cells["DoctorID"].Value.ToString())) LoadDoctors();
                    }
                    else if (lblTitle.Text == "Departments")
                    {
                        if (new DepartmentRepository().DeleteDepartment(Convert.ToInt16(row.Cells["DeptID"].Value))) LoadDepartments();
                    }
                    else if (lblTitle.Text == "Medications")
                    {
                        if (new MedicationRepository().DeleteMedication(Convert.ToInt16(row.Cells["MedCode"]?.Value ?? 0))) LoadMedications();
                    }
                    else if (lblTitle.Text == "Appointments")
                    {
                        if (new AppointmentRepository().DeleteAppointment(Convert.ToInt32(row.Cells["ApptID"].Value))) LoadAppointments();
                    }
                    else if (lblTitle.Text == "Prescriptions")
                    {
                        PrescribtionRepository repo = new PrescribtionRepository();
                        string patientId = row.Cells["PatientID"].Value.ToString();
                        string docId = row.Cells["DocID"].Value.ToString();
                        short medCode = Convert.ToInt16(row.Cells["MedCode"].Value);
                        
                        if (repo.DeletePrescribtion(patientId, medCode, docId)) LoadPrescribtions();
                    }
                    
                    MessageBox.Show("Operation completed.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Action Failed. Reason: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}