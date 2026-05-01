using System;
using System.Data;
using System.Data.SqlClient;

namespace DBProject.Data
{
    public class DoctorRepository
    {
        private SqlConnection conn;
        public DoctorRepository()
        {
            conn = DatabaseHelper.GetConnection();
        }

        public DataTable GetAllDoctors()
        {
            DataTable tbl_Doctors = new DataTable();

            tbl_Doctors.Columns.Add("DoctorID");
            tbl_Doctors.Columns.Add("FirstName");
            tbl_Doctors.Columns.Add("LastName");
            tbl_Doctors.Columns.Add("Specialty");
            tbl_Doctors.Columns.Add("DepartmentName");

            SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    d.DoctorID,
                    d.FirstName,
                    d.LastName,
                    d.Specialty,
                    dep.DeptName
                FROM Doctor d
                INNER JOIN Department dep ON d.DeptID = dep.DeptID
                ORDER BY d.LastName, d.FirstName", conn);

            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                DataRow row;
                while (reader.Read())
                {
                    row = tbl_Doctors.NewRow();
                    row["DoctorID"] = reader["DoctorID"].ToString();
                    row["FirstName"] = reader["FirstName"].ToString();
                    row["LastName"] = reader["LastName"].ToString();
                    row["Specialty"] = reader["Specialty"].ToString();
                    row["DepartmentName"] = reader["DeptName"].ToString();
                    tbl_Doctors.Rows.Add(row);
                }
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }

            return tbl_Doctors;
        }

        public void TestGetAllDoctors()
        {
            DataTable doctors = GetAllDoctors();

            Console.WriteLine("=== All Doctors ===\n");
            Console.WriteLine($"Total doctors: {doctors.Rows.Count}\n");

            foreach (DataRow row in doctors.Rows)
            {
                Console.WriteLine($"ID: {row["DoctorID"]} | Name: {row["FirstName"]} {row["LastName"]} | Specialty: {row["Specialty"]} | Department: {row["DepartmentName"]}");
            }
        }
    }
}