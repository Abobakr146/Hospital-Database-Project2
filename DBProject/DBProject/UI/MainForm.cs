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
            // Apply modern font to the entire form
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

            // Modernize the DataGridView
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 150, 136);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 136);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.DefaultCellStyle.Padding = new Padding(5);
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.GridColor = Color.LightGray;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            
            // Add padding around the grid in the content panel
            dataGridView1.Dock = DockStyle.None;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.Location = new Point(20, 80);
            
            // Adjust the size so it doesn't overlap the bottom action panel
            dataGridView1.Size = new Size(panelContent.Width - 40, panelContent.Height - 160);

            // Make sure the action panel is sent to the front so it is not hidden
            panelActions.BringToFront();

            // Enhance sidebar
            panelSidebar.BackColor = Color.FromArgb(41, 53, 65); // Modern dark blue-grey

            // Enhance sidebar buttons
            foreach (Control control in panelSidebar.Controls)
            {
                if (control is Button btn)
                {
                    btn.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
                    btn.ForeColor = Color.WhiteSmoke;
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    btn.Padding = new Padding(20, 0, 0, 0);
                    btn.Cursor = Cursors.Hand;
                    
                    // Add modern hover effects
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 73, 94);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(31, 43, 52);
                }
            }
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Doctors";
            panelActions.Visible = true;
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
            panelActions.Visible = false;
            DepartmentRepository repo = new DepartmentRepository();
            dataGridView1.DataSource = repo.GetAllDepartments();
        }

        private void btnPrescribtions_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Prescriptions";
            panelActions.Visible = false;
            PrescribtionRepository repo = new PrescribtionRepository();
            dataGridView1.DataSource = repo.GetAllPrescribtions();
        }

        private void btnMedications_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Medications";
            panelActions.Visible = false;
            MedicationRepository repo = new MedicationRepository();
            dataGridView1.DataSource = repo.GetAllMedications();
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Appointments";
            panelActions.Visible = false;
            AppointmentRepository repo = new AppointmentRepository();
            dataGridView1.DataSource = repo.GetAllAppointments();
        }

        private void btnAddEntity_Click(object sender, EventArgs e)
        {
            if (lblTitle.Text == "Patients")
            {
                PatientForm form = new PatientForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadPatients();
                }
            }
            else if (lblTitle.Text == "Doctors")
            {
                DoctorForm form = new DoctorForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDoctors();
                }
            }
        }

        private void btnEditEntity_Click(object sender, EventArgs e)
        {
            if (lblTitle.Text == "Patients")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var row = dataGridView1.SelectedRows[0];
                    string id = row.Cells["PatientID"].Value.ToString();
                    string firstName = row.Cells["FirstName"].Value.ToString();
                    string lastName = row.Cells["LastName"].Value.ToString();
                    string dob = row.Cells["DOB"].Value.ToString();
                    string phone = row.Cells["PhoneNumbers"]?.Value?.ToString() ?? "";

                    PatientForm form = new PatientForm(id, firstName, lastName, dob, phone);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadPatients();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a patient to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (lblTitle.Text == "Doctors")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var row = dataGridView1.SelectedRows[0];
                    string id = row.Cells["DoctorID"].Value.ToString();
                    string firstName = row.Cells["FirstName"].Value.ToString();
                    string lastName = row.Cells["LastName"].Value.ToString();
                    string specialty = row.Cells["Specialty"].Value.ToString();
                    string deptId = row.Cells["DeptID"].Value.ToString();

                    DoctorForm form = new DoctorForm(id, firstName, lastName, specialty, deptId);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadDoctors();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a doctor to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnDeleteEntity_Click(object sender, EventArgs e)
        {
            if (lblTitle.Text == "Patients")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    string id = dataGridView1.SelectedRows[0].Cells["PatientID"].Value.ToString();
                    string name = dataGridView1.SelectedRows[0].Cells["FirstName"].Value.ToString() + " " + dataGridView1.SelectedRows[0].Cells["LastName"].Value.ToString();

                    var confirmResult = MessageBox.Show($"Are you sure you want to delete patient '{name}' (ID: {id})?",
                                           "Confirm Delete",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        try
                        {
                            PatientRepository repo = new PatientRepository();
                            bool success = repo.DeletePatient(id);
                            if (success)
                            {
                                MessageBox.Show("Patient deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadPatients();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete patient. Ensure there are no related records (e.g., appointments, prescriptions) blocking deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a patient to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (lblTitle.Text == "Doctors")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    string id = dataGridView1.SelectedRows[0].Cells["DoctorID"].Value.ToString();
                    string name = dataGridView1.SelectedRows[0].Cells["FirstName"].Value.ToString() + " " + dataGridView1.SelectedRows[0].Cells["LastName"].Value.ToString();

                    var confirmResult = MessageBox.Show($"Are you sure you want to delete doctor '{name}' (ID: {id})?",
                                           "Confirm Delete",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        try
                        {
                            DoctorRepository repo = new DoctorRepository();
                            bool success = repo.DeleteDoctor(id);
                            if (success)
                            {
                                MessageBox.Show("Doctor deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDoctors();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete doctor. Ensure there are no related records blocking deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a doctor to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}