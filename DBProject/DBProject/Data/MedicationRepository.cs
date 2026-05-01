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