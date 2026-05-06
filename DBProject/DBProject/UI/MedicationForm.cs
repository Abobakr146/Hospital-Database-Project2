using System;
using System.Drawing;
using System.Windows.Forms;
using DBProject.Data;

namespace DBProject.UI
{
    public class MedicationForm : Form
    {
        private MedicationRepository repo = new MedicationRepository();
        public bool IsEditMode { get; set; }
        private short editMedCode;

        private Label lblHeader, lblName, lblDosage, lblUnit;
        private TextBox txtName, txtDosage, txtUnit;
        private Button btnSave, btnCancel;

        public MedicationForm(short medCode = 0, string medName = "", string dosage = "", string unit = "")
        {
            InitializeComponents();

            if (medCode != 0)
            {
                IsEditMode = true;
                editMedCode = medCode;
                txtName.Text = medName;
                txtDosage.Text = dosage;
                txtUnit.Text = unit;
                lblHeader.Text = "Edit Medication";
                btnSave.Text = "Update Medication";
            }
        }

        private void InitializeComponents()
        {
            this.ClientSize = new Size(434, 280);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Medication Form";

            lblHeader = new Label { Text = "Add Medication", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            lblName = new Label { Text = "Name:", Location = new Point(20, 70), AutoSize = true };
            txtName = new TextBox { Location = new Point(100, 67), Width = 200 };
            
            lblDosage = new Label { Text = "Dosage:", Location = new Point(20, 110), AutoSize = true };
            txtDosage = new TextBox { Location = new Point(100, 107), Width = 200 };

            lblUnit = new Label { Text = "Unit:", Location = new Point(20, 150), AutoSize = true };
            txtUnit = new TextBox { Location = new Point(100, 147), Width = 200 };

            btnSave = new Button { Text = "Add Medication", Location = new Point(60, 210), Size = new Size(140, 40), BackColor = Color.Teal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCancel = new Button { Text = "Cancel", Location = new Point(220, 210), Size = new Size(140, 40), BackColor = Color.IndianRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblHeader, lblName, txtName, lblDosage, txtDosage, lblUnit, txtUnit, btnSave, btnCancel });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtDosage.Text, out decimal dosage)) { MessageBox.Show("Invalid Dosage"); return; }
            if (IsEditMode)
                repo.UpdateMedication(editMedCode, txtName.Text, dosage, txtUnit.Text);
            else
                repo.InsertMedication(txtName.Text, dosage, txtUnit.Text);
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}