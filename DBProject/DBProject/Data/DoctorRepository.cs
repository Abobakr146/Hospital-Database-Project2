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

        public bool InsertDoctor(string doctorId, string firstName, string lastName, string specialty, short deptId)
        {
            SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Doctor (DoctorID, FirstName, LastName, Specialty, DeptID) 
        VALUES (@DoctorID, @FirstName, @LastName, @Specialty, @DeptID)", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@DoctorID", doctorId);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Specialty", specialty);
            cmd.Parameters.AddWithValue("@DeptID", deptId);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool UpdateDoctor(string doctorId, string firstName = null, string lastName = null, string specialty = null, short? deptId = null)
        {
            string setClause = "";

            if (!string.IsNullOrEmpty(firstName))
                setClause += "FirstName = @FirstName, ";
            if (!string.IsNullOrEmpty(lastName))
                setClause += "LastName = @LastName, ";
            if (!string.IsNullOrEmpty(specialty))
                setClause += "Specialty = @Specialty, ";
            if (deptId.HasValue)
                setClause += "DeptID = @DeptID, ";

            setClause = setClause.TrimEnd(',', ' ');

            SqlCommand cmd = new SqlCommand($@"
        UPDATE Doctor 
        SET {setClause}
        WHERE DoctorID = @DoctorID", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@DoctorID", doctorId);

            if (!string.IsNullOrEmpty(firstName))
                cmd.Parameters.AddWithValue("@FirstName", firstName);
            if (!string.IsNullOrEmpty(lastName))
                cmd.Parameters.AddWithValue("@LastName", lastName);
            if (!string.IsNullOrEmpty(specialty))
                cmd.Parameters.AddWithValue("@Specialty", specialty);
            if (deptId.HasValue)
                cmd.Parameters.AddWithValue("@DeptID", deptId.Value);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool DeleteDoctor(string doctorId)
        {
            SqlCommand cmd = new SqlCommand(@"
        DELETE FROM Doctor 
        WHERE DoctorID = @DoctorID", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@DoctorID", doctorId);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally
            {
                conn.Close();
            }
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