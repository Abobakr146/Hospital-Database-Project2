
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DBProject.Models;

namespace DBProject.Data
{
    public class PatientRepository
    {
        private SqlConnection conn;
        public PatientRepository()
        {
            conn = DatabaseHelper.GetConnection();
        }

        public DataTable GetAllPatients()
        {
            DataTable tbl_Patients = new DataTable();

            tbl_Patients.Columns.Add("PatientID");
            tbl_Patients.Columns.Add("FirstName");
            tbl_Patients.Columns.Add("LastName");
            tbl_Patients.Columns.Add("DOB");

            SqlCommand cmd = new SqlCommand(@"
        SELECT 
            p.PatientID,
            p.FirstName,
            p.LastName,
            p.DOB
        FROM Patient p
        ORDER BY p.LastName, p.FirstName", conn);

            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                DataRow row;
                while (reader.Read())
                {
                    row = tbl_Patients.NewRow();
                    row["PatientID"] = reader["PatientID"].ToString();
                    row["FirstName"] = reader["FirstName"].ToString();
                    row["LastName"] = reader["LastName"].ToString();
                    row["DOB"] = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]).ToShortDateString() : "";
                    tbl_Patients.Rows.Add(row);
                }
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }

            return tbl_Patients;
        }

        public DataTable GetAllPatientsAlongWithTheirPhoneNumbers()
        {
            DataTable tbl_Patients = new DataTable();

            tbl_Patients.Columns.Add("PatientID");
            tbl_Patients.Columns.Add("FirstName");
            tbl_Patients.Columns.Add("LastName");
            tbl_Patients.Columns.Add("DOB");
            tbl_Patients.Columns.Add("PhoneNumbers");


            SqlCommand cmd = new SqlCommand(@"
        SELECT 
            p.PatientID,
            p.FirstName,
            p.LastName,
            p.DOB,
            pp.Phone
        FROM Patient p
        LEFT JOIN Patient_Phone pp ON p.PatientID = pp.PatientID
        ORDER BY p.PatientID", conn);

            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                DataRow row = null;
                string currentPatientId = "";
                List<string> phones = new List<string>();

                while (reader.Read())
                {
                    string patientId = reader["PatientID"].ToString();

                    if (patientId != currentPatientId)
                    {
                        if (row != null)
                        {
                            row["PhoneNumbers"] = phones.Count > 0 ? string.Join(", ", phones) : "No Phone";
                        }

                        // Start new patient
                        currentPatientId = patientId;
                        phones = new List<string>();

                        row = tbl_Patients.NewRow();
                        row["PatientID"] = patientId;
                        row["FirstName"] = reader["FirstName"].ToString();
                        row["LastName"] = reader["LastName"].ToString();
                        row["DOB"] = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]).ToShortDateString() : "";

                        tbl_Patients.Rows.Add(row);
                    }

                    if (reader["Phone"] != DBNull.Value)
                    {
                        phones.Add(reader["Phone"].ToString());
                    }
                }

                if (row != null)
                {
                    row["PhoneNumbers"] = phones.Count > 0 ? string.Join(", ", phones) : "No Phone";
                }
            }
            finally
            {
                reader?.Close();
                conn.Close();
            }

            return tbl_Patients;
        }

        public bool InsertPatient(string patientId, string firstName, string lastName, DateTime? dob)
        {
            SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Patient (PatientID, FirstName, LastName, DOB) 
        VALUES (@PatientID, @FirstName, @LastName, @DOB)", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PatientID", patientId);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@DOB", (object)dob ?? DBNull.Value);

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

        public bool InsertPatientPhone(string patientId, string phone)
        {
            SqlCommand cmd = new SqlCommand(@"
        INSERT INTO Patient_Phone (PatientID, Phone) 
        VALUES (@PatientID, @Phone)", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PatientID", patientId);
            cmd.Parameters.AddWithValue("@Phone", phone);

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

        public bool UpdatePatient(string patientId, string firstName = null, string lastName = null, DateTime? dob = null)
        {
            string setClause = "";

            if (!string.IsNullOrEmpty(firstName))
                setClause += "FirstName = @FirstName, ";
            if (!string.IsNullOrEmpty(lastName))
                setClause += "LastName = @LastName, ";
            if (dob.HasValue)
                setClause += "DOB = @DOB, ";

            setClause = setClause.TrimEnd(',', ' ');

            SqlCommand cmd = new SqlCommand($@"
        UPDATE Patient 
        SET {setClause}
        WHERE PatientID = @PatientID", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PatientID", patientId);

            if (!string.IsNullOrEmpty(firstName))
                cmd.Parameters.AddWithValue("@FirstName", firstName);
            if (!string.IsNullOrEmpty(lastName))
                cmd.Parameters.AddWithValue("@LastName", lastName);
            if (dob.HasValue)
                cmd.Parameters.AddWithValue("@DOB", dob.Value);

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
        public bool UpdatePatientPhone(string patientId, string oldPhone, string newPhone)
        {
            SqlCommand cmd = new SqlCommand(@"
        UPDATE Patient_Phone 
        SET Phone = @NewPhone
        WHERE PatientID = @PatientID AND Phone = @OldPhone", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PatientID", patientId);
            cmd.Parameters.AddWithValue("@OldPhone", oldPhone);
            cmd.Parameters.AddWithValue("@NewPhone", newPhone);

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

        public bool DeletePatient(string patientId)
        {
            SqlCommand cmd = new SqlCommand(@"
        DELETE FROM Patient 
        WHERE PatientID = @PatientID", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PatientID", patientId);

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

        public bool DeletePatientPhone(string patientId, string phone)
        {
            SqlCommand cmd = new SqlCommand(@"
        DELETE FROM Patient_Phone 
        WHERE PatientID = @PatientID AND Phone = @Phone", conn);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PatientID", patientId);
            cmd.Parameters.AddWithValue("@Phone", phone);

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

        public void TestGetAllPatients()
        {
            DataTable patients = GetAllPatients();

            Console.WriteLine("=== All Patients ===");
            Console.WriteLine($"Total patients found: {patients.Rows.Count}\n");

            foreach (DataRow row in patients.Rows)
            {
                Console.WriteLine($"Patient ID: {row["PatientID"]}");
                Console.WriteLine($"Name: {row["FirstName"]} {row["LastName"]}");
                Console.WriteLine($"DOB: {row["DOB"]}");
                Console.WriteLine("-------------------");
            }
        }

        public void TestGetAllPatientsAlongWithTheirPhoneNumbers()
        {
            DataTable patients = GetAllPatientsAlongWithTheirPhoneNumbers();

            Console.WriteLine("=== All Patients ===\n");

            foreach (DataRow row in patients.Rows)
            {
                Console.WriteLine($"ID: {row["PatientID"]}");
                Console.WriteLine($"Name: {row["FirstName"]} {row["LastName"]}");
                Console.WriteLine($"DOB: {row["DOB"]}");
                Console.WriteLine($"Phones: {row["PhoneNumbers"]}");
                Console.WriteLine("-------------------");
            }
        }
    }
}
