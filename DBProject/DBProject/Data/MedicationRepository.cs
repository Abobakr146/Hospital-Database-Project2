using System;
using System.Data;
using System.Data.SqlClient;

namespace DBProject.Data
{
    public class MedicationRepository
    {
        private SqlConnection conn;
        public MedicationRepository()
        {
            conn = DatabaseHelper.GetConnection();
        }

        public DataTable GetAllMedications()
        {
            DataTable tbl_Medications = new DataTable();

            tbl_Medications.Columns.Add("MedCode");
            tbl_Medications.Columns.Add("MedName");
            tbl_Medications.Columns.Add("Dosage");
            tbl_Medications.Columns.Add("Unit");

            SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    MedCode,
                    MedName,
                    Dosage,
                    Unit
                FROM Medication
                ORDER BY MedName", conn);

            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                DataRow row;
                while (reader.Read())
                {
                    row = tbl_Medications.NewRow();
                    row["MedCode"] = reader["MedCode"].ToString();
                    row["MedName"] = reader["MedName"].ToString();
                    row["Dosage"] = reader["Dosage"].ToString();
                    row["Unit"] = reader["Unit"].ToString();
                    tbl_Medications.Rows.Add(row);
                }
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }

            return tbl_Medications;
        }

        public bool InsertMedication(string medName, decimal dosage, string unit)
        {
            SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Medication (MedName, Dosage, Unit) 
        VALUES (@MedName, @Dosage, @Unit)", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MedName", medName);
            cmd.Parameters.AddWithValue("@Dosage", dosage);
            cmd.Parameters.AddWithValue("@Unit", unit);

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

        public bool UpdateMedication(short medCode, string medName = null, decimal? dosage = null, string unit = null)
        {
            string setClause = "";

            if (!string.IsNullOrEmpty(medName))
                setClause += "MedName = @MedName, ";
            if (dosage.HasValue)
                setClause += "Dosage = @Dosage, ";
            if (!string.IsNullOrEmpty(unit))
                setClause += "Unit = @Unit, ";

            setClause = setClause.TrimEnd(',', ' ');

            SqlCommand cmd = new SqlCommand($@"
        UPDATE Medication 
        SET {setClause}
        WHERE MedCode = @MedCode", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MedCode", medCode);

            if (!string.IsNullOrEmpty(medName))
                cmd.Parameters.AddWithValue("@MedName", medName);
            if (dosage.HasValue)
                cmd.Parameters.AddWithValue("@Dosage", dosage.Value);
            if (!string.IsNullOrEmpty(unit))
                cmd.Parameters.AddWithValue("@Unit", unit);

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

        public bool DeleteMedication(short medCode)
        {
            SqlCommand cmd = new SqlCommand(@"
        DELETE FROM Medication 
        WHERE MedCode = @MedCode", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MedCode", medCode);

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

        public void TestGetAllMedications()
        {
            DataTable medications = GetAllMedications();

            Console.WriteLine("=== All Medications ===\n");
            Console.WriteLine($"Total medications: {medications.Rows.Count}\n");

            foreach (DataRow row in medications.Rows)
            {
                Console.WriteLine($"Code: {row["MedCode"]} | Name: {row["MedName"]} | Dosage: {row["Dosage"]} {row["Unit"]}");
            }
        }
    }
}