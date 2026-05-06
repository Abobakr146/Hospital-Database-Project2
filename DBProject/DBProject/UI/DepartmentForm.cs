using System;
using System.Drawing;
using System.Windows.Forms;
using DBProject.Data;

namespace DBProject.UI
{
    public class DepartmentForm : Form
    {
        private DepartmentRepository repo = new DepartmentRepository();
        public bool IsEditMode { get; set; }
        private short editDeptId;

        private Label lblHeader, lblName, lblLocation, lblManager;
        private TextBox txtName, txtLocation, txtManager;
        private Button btnSave, btnCancel;

        public DepartmentForm(short deptId = 0, string deptName = "", string location = "", string managerDocId = "")
        {
            InitializeComponents();

            if (deptId != 0)
            {
                IsEditMode = true;
                editDeptId = deptId;
                txtName.Text = deptName;
                txtLocation.Text = location;
                txtManager.Text = managerDocId;
                lblHeader.Text = "Edit Department";
                btnSave.Text = "Update Department";
            }
        }

        private void InitializeComponents()
        {
            this.ClientSize = new Size(434, 280);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Department Form";

            lblHeader = new Label { Text = "Add Department", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            lblName = new Label { Text = "Name:", Location = new Point(20, 70), AutoSize = true };
            txtName = new TextBox { Location = new Point(100, 67), Width = 200 };
            
            lblLocation = new Label { Text = "Location:", Location = new Point(20, 110), AutoSize = true };
            txtLocation = new TextBox { Location = new Point(100, 107), Width = 200 };

            lblManager = new Label { Text = "Manager ID:", Location = new Point(20, 150), AutoSize = true };
            txtManager = new TextBox { Location = new Point(100, 147), Width = 200 };

            btnSave = new Button { Text = "Add Department", Location = new Point(60, 210), Size = new Size(140, 40), BackColor = Color.Teal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCancel = new Button { Text = "Cancel", Location = new Point(220, 210), Size = new Size(140, 40), BackColor = Color.IndianRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblHeader, lblName, txtName, lblLocation, txtLocation, lblManager, txtManager, btnSave, btnCancel });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string managerId = string.IsNullOrWhiteSpace(txtManager.Text) ? null : txtManager.Text;

                if (IsEditMode)
                    repo.UpdateDepartment(editDeptId, txtName.Text, txtLocation.Text, managerId);
                else
                    repo.InsertDepartment(txtName.Text, txtLocation.Text, managerId);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FOREIGN KEY") || ex.Message.Contains("ManagerDocID"))
                    MessageBox.Show("Invalid Manager ID. Please ensure the Manager Doctor ID exists or leave it blank if no manager is assigned.", "Database Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("An error occurred while saving the department:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}