using System;
using System.Data;
using System.Data.SqlClient;

namespace DBProject.Data
{
    public class AppointmentRepository
    {
        private SqlConnection conn;
        public AppointmentRepository()
        {
            conn = DatabaseHelper.GetConnection();
        }

        public DataTable GetAllAppointments()
        {
            DataTable tbl_Appointments = new DataTable();

            tbl_Appointments.Columns.Add("ApptID");
            tbl_Appointments.Columns.Add("ApptDate");
            tbl_Appointments.Columns.Add("ApptTime");
            tbl_Appointments.Columns.Add("Status");
            tbl_Appointments.Columns.Add("PatientName");
            tbl_Appointments.Columns.Add("DoctorName");

            SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    a.ApptID,
                    a.ApptDate,
                    a.ApptTime,
                    a.Status,
                    p.FirstName + ' ' + p.LastName AS PatientName,
                    d.FirstName + ' ' + d.LastName AS DoctorName
                FROM Appointment a
                INNER JOIN Patient p ON a.PatientID = p.PatientID
                INNER JOIN Doctor d ON a.DoctorID = d.DoctorID
                ORDER BY a.ApptDate , a.ApptTime", conn);

            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                DataRow row;
                while (reader.Read())
                {
                    row = tbl_Appointments.NewRow();
                    row["ApptID"] = reader["ApptID"].ToString();
                    row["ApptDate"] = reader["ApptDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApptDate"]).ToShortDateString() : "";
                    row["ApptTime"] = reader["ApptTime"].ToString();
                    row["Status"] = reader["Status"].ToString();
                    row["PatientName"] = reader["PatientName"].ToString();
                    row["DoctorName"] = reader["DoctorName"].ToString();
                    tbl_Appointments.Rows.Add(row);
                }
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }

            return tbl_Appointments;
        }

        public DataTable GetAppointmentsByDoctor(string doctorId)
        {
            DataTable tbl_Appointments = new DataTable();

            tbl_Appointments.Columns.Add("ApptID");
            tbl_Appointments.Columns.Add("ApptDate");
            tbl_Appointments.Columns.Add("ApptTime");
            tbl_Appointments.Columns.Add("Status");
            tbl_Appointments.Columns.Add("PatientName");
            tbl_Appointments.Columns.Add("DoctorName");

            SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    a.ApptID,
                    a.ApptDate,
                    a.ApptTime,
                    a.Status,
                    p.FirstName + ' ' + p.LastName AS PatientName,
                    d.FirstName + ' ' + d.LastName AS DoctorName
                FROM Appointment a
                INNER JOIN Patient p ON a.PatientID = p.PatientID
                INNER JOIN Doctor d ON a.DoctorID = d.DoctorID
                WHERE a.DoctorID = @DoctorID
                ORDER BY a.ApptDate , a.ApptTime", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@DoctorID", doctorId);

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                DataRow row;
                while (reader.Read())
                {
                    row = tbl_Appointments.NewRow();
                    row["ApptID"] = reader["ApptID"].ToString();
                    row["ApptDate"] = reader["ApptDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApptDate"]).ToShortDateString() : "";
                    row["ApptTime"] = reader["ApptTime"].ToString();
                    row["Status"] = reader["Status"].ToString();
                    row["PatientName"] = reader["PatientName"].ToString();
                    row["DoctorName"] = reader["DoctorName"].ToString();
                    tbl_Appointments.Rows.Add(row);
                }
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }

            return tbl_Appointments;
        }

        public bool InsertAppointment(DateTime apptDate, TimeSpan apptTime, string status, string patientId, string doctorId)
        {
            SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Appointment (ApptDate, ApptTime, Status, PatientID, DoctorID) 
        VALUES (@ApptDate, @ApptTime, @Status, @PatientID, @DoctorID)", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ApptDate", apptDate);
            cmd.Parameters.AddWithValue("@ApptTime", apptTime);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@PatientID", patientId);
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

        public bool UpdateAppointment(int apptId, DateTime? apptDate = null, TimeSpan? apptTime = null, string status = null, string patientId = null, string doctorId = null)
        {
            string setClause = "";

            if (apptDate.HasValue)
                setClause += "ApptDate = @ApptDate, ";
            if (apptTime.HasValue)
                setClause += "ApptTime = @ApptTime, ";
            if (!string.IsNullOrEmpty(status))
                setClause += "Status = @Status, ";
            if (!string.IsNullOrEmpty(patientId))
                setClause += "PatientID = @PatientID, ";
            if (!string.IsNullOrEmpty(doctorId))
                setClause += "DoctorID = @DoctorID, ";

            setClause = setClause.TrimEnd(',', ' ');

            SqlCommand cmd = new SqlCommand($@"
        UPDATE Appointment 
        SET {setClause}
        WHERE ApptID = @ApptID", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ApptID", apptId);

            if (apptDate.HasValue)
                cmd.Parameters.AddWithValue("@ApptDate", apptDate.Value);
            if (apptTime.HasValue)
                cmd.Parameters.AddWithValue("@ApptTime", apptTime.Value);
            if (!string.IsNullOrEmpty(status))
                cmd.Parameters.AddWithValue("@Status", status);
            if (!string.IsNullOrEmpty(patientId))
                cmd.Parameters.AddWithValue("@PatientID", patientId);
            if (!string.IsNullOrEmpty(doctorId))
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

        public bool DeleteAppointment(int apptId)
        {
            SqlCommand cmd = new SqlCommand(@"
        DELETE FROM Appointment 
        WHERE ApptID = @ApptID", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ApptID", apptId);

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

        public void TestGetAllAppointments()
        {
            DataTable appointments = GetAllAppointments();

            Console.WriteLine("=== All Appointments ===\n");
            Console.WriteLine($"Total appointments: {appointments.Rows.Count}\n");

            foreach (DataRow row in appointments.Rows)
            {
                Console.WriteLine($"ID: {row["ApptID"]} | Date: {row["ApptDate"]} | Time: {row["ApptTime"]} | Status: {row["Status"]} | Patient: {row["PatientName"]} | Doctor: {row["DoctorName"]}");
            }
        }

        public void TestGetAppointmentsByDoctor(string doctorId)
        {
            DataTable appointments = GetAppointmentsByDoctor(doctorId);

            Console.WriteLine($"=== Appointments for Doctor {doctorId} ===\n");
            Console.WriteLine($"Total appointments: {appointments.Rows.Count}\n");

            foreach (DataRow row in appointments.Rows)
            {
                Console.WriteLine($"ID: {row["ApptID"]} | Date: {row["ApptDate"]} | Time: {row["ApptTime"]} | Status: {row["Status"]} | Patient: {row["PatientName"]} | Doctor: {row["DoctorName"]}");
            }
        }
    }
}