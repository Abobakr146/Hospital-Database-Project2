using System;
using System.Data;
using System.Data.SqlClient;

namespace DBProject.Data
{
    public class DepartmentRepository
    {
        private SqlConnection conn;
        public DepartmentRepository()
        {
            conn = DatabaseHelper.GetConnection();
        }

        public DataTable GetAllDepartments()
        {
            DataTable tbl_Departments = new DataTable();

            tbl_Departments.Columns.Add("DeptID");
            tbl_Departments.Columns.Add("DeptName");
            tbl_Departments.Columns.Add("Location");
            tbl_Departments.Columns.Add("ManagerID");
            tbl_Departments.Columns.Add("ManagerName");

            SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    dep.DeptID,
                    dep.DeptName,
                    dep.Location,
                    dep.Manager_DocID,
                    d.FirstName + ' ' + d.LastName AS ManagerName
                FROM Department dep
                LEFT JOIN Doctor d ON dep.Manager_DocID = d.DoctorID
                ORDER BY dep.DeptName", conn);

            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                DataRow row;
                while (reader.Read())
                {
                    row = tbl_Departments.NewRow();
                    row["DeptID"] = reader["DeptID"].ToString();
                    row["DeptName"] = reader["DeptName"].ToString();
                    row["Location"] = reader["Location"] != DBNull.Value ? reader["Location"].ToString() : "";
                    row["ManagerID"] = reader["Manager_DocID"] != DBNull.Value ? reader["Manager_DocID"].ToString() : "No Manager";
                    row["ManagerName"] = reader["ManagerName"] != DBNull.Value ? reader["ManagerName"].ToString() : "No Manager";
                    tbl_Departments.Rows.Add(row);
                }
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }

            return tbl_Departments;
        }

        public DataTable GetDoctorCountByDepartment()
        {
            DataTable tbl_Count = new DataTable();

            tbl_Count.Columns.Add("DeptName");
            tbl_Count.Columns.Add("DoctorCount");

            SqlCommand cmd = new SqlCommand(@"
        SELECT 
            dep.DeptName,
            COUNT(d.DoctorID) AS DoctorCount
        FROM Department dep
        LEFT JOIN Doctor d ON dep.DeptID = d.DeptID
        GROUP BY dep.DeptName
        ORDER BY dep.DeptName", conn);

            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                DataRow row;
                while (reader.Read())
                {
                    row = tbl_Count.NewRow();
                    row["DeptName"] = reader["DeptName"].ToString();
                    row["DoctorCount"] = reader["DoctorCount"].ToString();
                    tbl_Count.Rows.Add(row);
                }
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }

            return tbl_Count;
        }

        public bool InsertDepartment(string deptName, string location, string managerDocId = null)
        {
            SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Department (DeptName, Location, Manager_DocID) 
        VALUES (@DeptName, @Location, @Manager_DocID)", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@DeptName", deptName);
            cmd.Parameters.AddWithValue("@Location", (object)location ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Manager_DocID", (object)managerDocId ?? DBNull.Value);

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

        public bool UpdateDepartment(short deptId, string deptName = null, string location = null, string managerDocId = null)
        {
            string setClause = "";

            if (!string.IsNullOrEmpty(deptName))
                setClause += "DeptName = @DeptName, ";
            if (location != null)
                setClause += "Location = @Location, ";
            if (managerDocId != null)
                setClause += "Manager_DocID = @Manager_DocID, ";

            setClause = setClause.TrimEnd(',', ' ');

            SqlCommand cmd = new SqlCommand($@"
        UPDATE Department 
        SET {setClause}
        WHERE DeptID = @DeptID", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@DeptID", deptId);

            if (!string.IsNullOrEmpty(deptName))
                cmd.Parameters.AddWithValue("@DeptName", deptName);
            if (location != null)
                cmd.Parameters.AddWithValue("@Location", location == "" ? DBNull.Value : (object)location);
            if (managerDocId != null)
                cmd.Parameters.AddWithValue("@Manager_DocID", managerDocId == "" ? DBNull.Value : (object)managerDocId);

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

        public bool DeleteDepartment(short deptId)
        {
            SqlCommand cmd = new SqlCommand(@"
        DELETE FROM Department 
        WHERE DeptID = @DeptID", conn);

            cmd.CommandType = CommandType.Text;
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

        public void TestGetDoctorCountByDepartment()
        {
            DataTable counts = GetDoctorCountByDepartment();

            Console.WriteLine("=== Doctors per Department ===\n");

            foreach (DataRow row in counts.Rows)
            {
                Console.WriteLine($"{row["DeptName"]}: {row["DoctorCount"]} doctors");
            }
        }

        public void TestGetAllDepartments()
        {
            DataTable departments = GetAllDepartments();

            Console.WriteLine("=== All Departments ===\n");
            Console.WriteLine($"Total departments: {departments.Rows.Count}\n");

            foreach (DataRow row in departments.Rows)
            {
                Console.WriteLine($"ID: {row["DeptID"]} | Name: {row["DeptName"]} | Location: {row["Location"]} | Manager: {row["ManagerName"]} (ID: {row["ManagerID"]})");
            }
        }
    }
}