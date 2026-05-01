using System;
using System.Drawing;
using System.Windows.Forms;
using DBProject.Data;

namespace DBProject.UI
{
    public partial class PatientForm : Form
    {
        private PatientRepository repo;
        private string patientIdToEdit;
        public bool IsEditMode { get; set; }

        public PatientForm(string patientId = "", string firstName = "", string lastName = "", string dob = "", string phones = "")
        {
            InitializeComponent();
            
            repo = new PatientRepository();
            
            // Setup form based on mode
            if (!string.IsNullOrEmpty(patientId))
            {
                IsEditMode = true;
                patientIdToEdit = patientId;
                txtPatientId.Text = patientId;
                txtPatientId.Enabled = false; // Cannot change ID in edit mode
                txtFirstName.Text = firstName;
                txtLastName.Text = lastName;
                
                if (txtPhone != null) 
                    txtPhone.Text = phones; // Show the comma-separated phones
                
                if (DateTime.TryParse(dob, out DateTime dobDate))
                {
                    dtpDOB.Value = dobDate;
                }
                
                lblHeader.Text = "Edit Patient";
                btnSave.Text = "Update Patient";
            }
            else
            {
                IsEditMode = false;
                
                // Enable Patient ID field for manual entry
                txtPatientId.Text = "";
                txtPatientId.Enabled = true;

                if (txtPhone != null)
                    txtPhone.Text = "";

                lblHeader.Text = "Add New Patient";
                btnSave.Text = "Add Patient";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientId.Text) || string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please fill out Patient ID, First Name, and Last Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (IsEditMode)
                {
                    bool success = repo.UpdatePatient(txtPatientId.Text, txtFirstName.Text, txtLastName.Text, dtpDOB.Value);
                    if (success)
                    {
                        // Handling phones is tricky since it's a list. 
                        // If they enter a new comma separated list, we can clear old and insert new.
                        repo.DeletePatientPhone(txtPatientId.Text, ""); // Needs logic in repository to delete ALL phones for patient. 
                        // For simplicity on update, we just insert any new ones they typed, assuming your DB handles duplicates gracefully,
                        // OR if you add `DeleteAllPatientPhones` to your repo you can wipe and re-insert.
                        // Let's do a simple insert for the updated phone string.
                        string[] phones = txtPhone.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach(var phone in phones)
                        {
                            try { repo.InsertPatientPhone(txtPatientId.Text, phone.Trim()); } catch { /* Ignore duplicate PK errors */ }
                        }

                        MessageBox.Show("Patient updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update patient. Maybe the record was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool success = repo.InsertPatient(txtPatientId.Text, txtFirstName.Text, txtLastName.Text, dtpDOB.Value);
                    if (success)
                    {
                        // Add Phones
                        string[] phones = txtPhone.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var phone in phones)
                        {
                            repo.InsertPatientPhone(txtPatientId.Text, phone.Trim());
                        }

                        MessageBox.Show("Patient added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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