using System;
using System.Drawing;
using System.Windows.Forms;
using DBProject.Data;

namespace DBProject.UI
{
    public class PrescribtionForm : Form
    {
        private PrescribtionRepository repo = new PrescribtionRepository();

        private Label lblHeader, lblPatientId, lblMedCode, lblDoctorId;
        private TextBox txtPatientId, txtMedCode, txtDoctorId;
        private Button btnSave, btnCancel;

        public PrescribtionForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.ClientSize = new Size(434, 280);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Prescribtion Form";

            lblHeader = new Label { Text = "Add Prescribtion", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            
            lblPatientId = new Label { Text = "Patient ID:", Location = new Point(20, 70), AutoSize = true };
            txtPatientId = new TextBox { Location = new Point(120, 67), Width = 180 };
            
            lblMedCode = new Label { Text = "Med Code:", Location = new Point(20, 110), AutoSize = true };
            txtMedCode = new TextBox { Location = new Point(120, 107), Width = 180 };

            lblDoctorId = new Label { Text = "Doctor ID:", Location = new Point(20, 150), AutoSize = true };
            txtDoctorId = new TextBox { Location = new Point(120, 147), Width = 180 };

            btnSave = new Button { Text = "Add Prescription", Location = new Point(60, 210), Size = new Size(140, 40), BackColor = Color.Teal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCancel = new Button { Text = "Cancel", Location = new Point(220, 210), Size = new Size(140, 40), BackColor = Color.IndianRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblHeader, lblPatientId, txtPatientId, lblMedCode, txtMedCode, lblDoctorId, txtDoctorId, btnSave, btnCancel });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!short.TryParse(txtMedCode.Text, out short medCode)) { MessageBox.Show("Invalid Med Code"); return; }
            repo.InsertPrescribtion(txtPatientId.Text, medCode, txtDoctorId.Text);
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}