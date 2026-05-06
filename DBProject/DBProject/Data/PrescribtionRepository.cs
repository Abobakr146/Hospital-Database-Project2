using System;
using System.Data;
using System.Data.SqlClient;

namespace DBProject.Data
{
    public class PrescribtionRepository
    {
        private SqlConnection conn;
        public PrescribtionRepository()
        {
            conn = DatabaseHelper.GetConnection();
        }

        public DataTable GetAllPrescribtions()
        {
            DataTable tbl_Prescribtions = new DataTable();

            tbl_Prescribtions.Columns.Add("PatientID");
            tbl_Prescribtions.Columns.Add("DocID");
            tbl_Prescribtions.Columns.Add("MedCode");
            tbl_Prescribtions.Columns.Add("PatientName");
            tbl_Prescribtions.Columns.Add("MedicationName");
            tbl_Prescribtions.Columns.Add("Dosage");
            tbl_Prescribtions.Columns.Add("Unit");
            tbl_Prescribtions.Columns.Add("DoctorName");
            tbl_Prescribtions.Columns.Add("PrescribtionDate");

            SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    pr.PatientID,
                    pr.DocID,
                    pr.MedCode,
                    p.FirstName + ' ' + p.LastName AS PatientName,
                    m.MedName,
                    m.Dosage,
                    m.Unit,
                    d.FirstName + ' ' + d.LastName AS DoctorName,
                    pr.PrescribtionDate
                FROM Prescribtion pr
                INNER JOIN Patient p ON pr.PatientID = p.PatientID
                INNER JOIN Medication m ON pr.MedCode = m.MedCode
                INNER JOIN Doctor d ON pr.DocID = d.DoctorID
                ORDER BY pr.PrescribtionDate DESC", conn);

            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                DataRow row;
                while (reader.Read())
                {
                    row = tbl_Prescribtions.NewRow();
                    row["PatientID"] = reader["PatientID"].ToString();
                    row["DocID"] = reader["DocID"].ToString();
                    row["MedCode"] = reader["MedCode"].ToString();
                    row["PatientName"] = reader["PatientName"].ToString();
                    row["MedicationName"] = reader["MedName"].ToString();
                    row["Dosage"] = reader["Dosage"].ToString();
                    row["Unit"] = reader["Unit"].ToString();
                    row["DoctorName"] = reader["DoctorName"].ToString();
                    row["PrescribtionDate"] = reader["PrescribtionDate"] != DBNull.Value ? Convert.ToDateTime(reader["PrescribtionDate"]).ToString() : "";
                    tbl_Prescribtions.Rows.Add(row);
                }
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }

            return tbl_Prescribtions;
        }

        public bool InsertPrescribtion(string patientId, short medCode, string docId)
        {
            SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Prescribtion (PatientID, MedCode, DocID, PrescribtionDate) 
        VALUES (@PatientID, @MedCode, @DocID, @PrescribtionDate)", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PatientID", patientId);
            cmd.Parameters.AddWithValue("@MedCode", medCode);
            cmd.Parameters.AddWithValue("@DocID", docId);
            cmd.Parameters.AddWithValue("@PrescribtionDate", DateTime.Now);

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

        public bool DeletePrescribtion(string patientId, short medCode, string docId)
        {
            SqlCommand cmd = new SqlCommand(@"
        DELETE FROM Prescribtion 
        WHERE PatientID = @PatientID AND MedCode = @MedCode AND DocID = @DocID", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PatientID", patientId);
            cmd.Parameters.AddWithValue("@MedCode", medCode);
            cmd.Parameters.AddWithValue("@DocID", docId);

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

        public void TestGetAllPrescribtions()
        {
            DataTable prescribtions = GetAllPrescribtions();

            Console.WriteLine("=== All Prescribtions ===\n");
            Console.WriteLine($"Total prescribtions: {prescribtions.Rows.Count}\n");

            foreach (DataRow row in prescribtions.Rows)
            {
                Console.WriteLine($"Patient: {row["PatientName"]} | Medication: {row["MedicationName"]} ({row["Dosage"]} {row["Unit"]}) | Doctor: {row["DoctorName"]} | Date: {row["PrescribtionDate"]}");
            }
        }
    }
}