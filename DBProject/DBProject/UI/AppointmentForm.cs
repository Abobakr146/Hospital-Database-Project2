using System;
using System.Drawing;
using System.Windows.Forms;
using DBProject.Data;

namespace DBProject.UI
{
    public class AppointmentForm : Form
    {
        private AppointmentRepository repo = new AppointmentRepository();
        public bool IsEditMode { get; set; }
        private int editApptId;

        private Label lblHeader, lblDate, lblTime, lblStatus, lblPatient, lblDoctor;
        private DateTimePicker dtpDate;
        private TextBox txtTime, txtStatus, txtPatient, txtDoctor;
        private Button btnSave, btnCancel;

        public AppointmentForm(int apptId = 0, string date = "", string time = "", string status = "", string patient = "", string doctor = "")
        {
            InitializeComponents();

            if (apptId != 0)
            {
                IsEditMode = true;
                editApptId = apptId;
                if (DateTime.TryParse(date, out DateTime parsedDate)) dtpDate.Value = parsedDate;
                txtTime.Text = time;
                txtStatus.Text = status;
                txtPatient.Text = patient;
                txtDoctor.Text = doctor;
                lblHeader.Text = "Edit Appointment";
                btnSave.Text = "Update Appointment";
            }
        }

        private void InitializeComponents()
        {
            this.ClientSize = new Size(434, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Appointment Form";

            lblHeader = new Label { Text = "Add Appointment", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            
            lblDate = new Label { Text = "Date:", Location = new Point(20, 70), AutoSize = true };
            dtpDate = new DateTimePicker { Location = new Point(100, 67), Width = 200, Format = DateTimePickerFormat.Short };
            
            lblTime = new Label { Text = "Time:", Location = new Point(20, 110), AutoSize = true };
            txtTime = new TextBox { Location = new Point(100, 107), Width = 200, Text = "00:00:00" };

            lblStatus = new Label { Text = "Status:", Location = new Point(20, 150), AutoSize = true };
            txtStatus = new TextBox { Location = new Point(100, 147), Width = 200, Text = "Scheduled" };

            lblPatient = new Label { Text = "Patient ID:", Location = new Point(20, 190), AutoSize = true };
            txtPatient = new TextBox { Location = new Point(100, 187), Width = 200 };

            lblDoctor = new Label { Text = "Doctor ID:", Location = new Point(20, 230), AutoSize = true };
            txtDoctor = new TextBox { Location = new Point(100, 227), Width = 200 };

            btnSave = new Button { Text = "Add Appointment", Location = new Point(60, 285), Size = new Size(140, 40), BackColor = Color.Teal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCancel = new Button { Text = "Cancel", Location = new Point(220, 285), Size = new Size(140, 40), BackColor = Color.IndianRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblHeader, lblDate, dtpDate, lblTime, txtTime, lblStatus, txtStatus, lblPatient, txtPatient, lblDoctor, txtDoctor, btnSave, btnCancel });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParse(txtTime.Text, out TimeSpan time)) { MessageBox.Show("Invalid Time. Use format HH:MM:SS"); return; }
            
            try
            {
                if (IsEditMode)
                    repo.UpdateAppointment(editApptId, dtpDate.Value, time, txtStatus.Text, txtPatient.Text, txtDoctor.Text);
                else
                    repo.InsertAppointment(dtpDate.Value, time, txtStatus.Text, txtPatient.Text, txtDoctor.Text);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 547) // Foreign Key Violation
                {
                    MessageBox.Show("The specified Patient ID or Doctor ID does not exist in the database.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}