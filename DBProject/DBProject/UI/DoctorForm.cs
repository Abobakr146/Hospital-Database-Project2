using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DBProject.Data;

namespace DBProject.UI
{
    public partial class DoctorForm : Form
    {
        private DoctorRepository repo;
        private DepartmentRepository deptRepo;
        public bool IsEditMode { get; set; }

        public DoctorForm(string doctorId = "", string firstName = "", string lastName = "", string specialty = "", string deptId = "")
        {
            InitializeComponent();
            repo = new DoctorRepository();
            deptRepo = new DepartmentRepository();
            
            // Load Departments into ComboBox
            LoadDepartments();
            
            // Setup form based on mode
            if (!string.IsNullOrEmpty(doctorId))
            {
                IsEditMode = true;
                txtDoctorId.Text = doctorId;
                txtDoctorId.Enabled = false; // Cannot change ID in edit mode
                txtFirstName.Text = firstName;
                txtLastName.Text = lastName;
                txtSpecialty.Text = specialty;
                
                // Select proper department
                if (short.TryParse(deptId, out short parsedDeptId))
                {
                    cmbDepartment.SelectedValue = parsedDeptId;
                }
                
                lblHeader.Text = "Edit Doctor";
                btnSave.Text = "Update Doctor";
            }
            else
            {
                IsEditMode = false;
                // Enable Doctor ID field for manual entry
                txtDoctorId.Text = "";
                txtDoctorId.Enabled = true;

                lblHeader.Text = "Add New Doctor";
                btnSave.Text = "Add Doctor";
            }
        }

        private void LoadDepartments()
        {
            try
            {
                DataTable departments = deptRepo.GetAllDepartments();
                
                // Since DepartmentRepository returns DeptID as string in DataTable, we need to convert or bind directly.
                // We'll just bind directly and let ValueMember map to DeptID string, which we parse later.
                cmbDepartment.DataSource = departments;
                cmbDepartment.DisplayMember = "DeptName";
                cmbDepartment.ValueMember = "DeptID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading departments: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDoctorId.Text) || 
                string.IsNullOrWhiteSpace(txtFirstName.Text) || 
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtSpecialty.Text) ||
                cmbDepartment.SelectedValue == null)
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!short.TryParse(cmbDepartment.SelectedValue.ToString(), out short parsedDeptId))
            {
                MessageBox.Show("Invalid Department selection.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (IsEditMode)
                {
                    bool success = repo.UpdateDoctor(txtDoctorId.Text, txtFirstName.Text, txtLastName.Text, txtSpecialty.Text, parsedDeptId);
                    if (success)
                    {
                        MessageBox.Show("Doctor updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update doctor. Might not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool success = repo.InsertDoctor(txtDoctorId.Text, txtFirstName.Text, txtLastName.Text, txtSpecialty.Text, parsedDeptId);
                    if (success)
                    {
                        MessageBox.Show("Doctor added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add doctor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}